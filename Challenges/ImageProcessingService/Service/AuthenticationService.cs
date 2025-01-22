using System.Security.Claims;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects;
using Service.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Shared.Mapping;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Entities.ConfigurationModels;
using Microsoft.Extensions.Options;

namespace Service;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IOptions<JwtConfiguration> _configuration;
    private readonly JwtConfiguration _jwtConfiguration;
    private User? _user;

    public AuthenticationService(
      UserManager<User> userManager,
      RoleManager<IdentityRole> roleManager,
      IOptions<JwtConfiguration> configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _jwtConfiguration = _configuration.Value;
    }


    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        var refreshToken = GenerateRefreshToken();
        _user.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(_user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(accessToken, refreshToken);
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var existingRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        var invalidRoles = userForRegistration.Roles
                            .Where(r => !existingRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
                            .ToList();

        if (invalidRoles?.Any() == true)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidRoles",
                Description = $"Invalid roles: {string.Join(", ", invalidRoles)}"
            });
        }

        var user = userForRegistration.ToUser();
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        if (!result.Succeeded && userForRegistration.Roles?.Any() == true)
        {
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return IdentityResult.Failed(roleResult.Errors.ToArray());
                }
            }
        }

        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);

        var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
        if (!result)
        {
            Console.WriteLine("Authentication failed. Wrong user name or password");
            // _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
        }
        // Console.WriteLine("*** Authentication failed. Wrong username or password.");
        return result;
    }

    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);
        if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
                             user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();

        _user = user;
        return await CreateToken(populateExp: false);
    }


    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = _jwtConfiguration.Secret;

        if (string.IsNullOrEmpty(secretKey))
            throw new InvalidOperationException("JWT Secret key is missing in configuration.");

        byte[] keyBytes;
        try
        {
            keyBytes = Convert.FromBase64String(secretKey);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("JWT Secret key is not a valid Base64 string.", ex);
        }

        Console.WriteLine($"Decoded Key Length: {keyBytes.Length} bytes");

        if (keyBytes.Length != 32)
            throw new InvalidOperationException("JWT Secret key must decode to 32 bytes (256 bits).");

        var secret = new SymmetricSecurityKey(keyBytes);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, _user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
                                                  List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(issuer: _jwtConfiguration.ValidIssuer,
                                                audience: _jwtConfiguration.ValidAudience,
                                                claims: claims,
                                                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                                                signingCredentials: signingCredentials);
        return tokenOptions;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                                    Convert.FromBase64String(_jwtConfiguration.Secret)),
            ValidateLifetime = true,
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token,
                                                   tokenValidationParameters,
                                                   out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }


    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
