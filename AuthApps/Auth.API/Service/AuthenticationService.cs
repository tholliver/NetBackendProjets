using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;
using Contracts;

namespace Service;

internal sealed class AuthenticationService(
    ILoggerManager logger,
    IMapper mapper,
    UserManager<User> userManager,
    IConfiguration configuration) : IAuthenticationService
{
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;
    private User _user;

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);

        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
        // Console.WriteLine("*** Authentication failed. Wrong username or password.");

        return result;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = _configuration["Secret"];

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
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
        return tokenOptions;
    }
}
