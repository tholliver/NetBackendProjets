using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using Banking.API.Domain.Customers;
using Banking.API.Data;
using Banking.API.Dtos;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Banking.API.Endpoints;
public static class IdentityApiCustomerEndpoint
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();

    public static IEndpointConventionBuilder MapIdentityCustomerApi(this IEndpointRouteBuilder endpoints)
    {
        var timeProvider = endpoints.ServiceProvider.GetRequiredService<TimeProvider>();
        var bearerTokenOptions = endpoints.ServiceProvider.GetRequiredService<IOptionsMonitor<BearerTokenOptions>>();

        var routeGroup = endpoints.MapGroup("");
        routeGroup.MapPost("/register", RegisterCustomer);
        routeGroup.MapPost("/login", LoginCustomer);
        routeGroup.MapGet("/info", InfoCustomer);
        routeGroup.MapPost("/refresh", async Task<Results<Ok<AccessTokenResponse>, UnauthorizedHttpResult, SignInHttpResult, ChallengeHttpResult>>
          ([FromBody] RefreshRequest refreshRequest, [FromServices] IServiceProvider sp) =>
          {
              var signInManager = sp.GetRequiredService<SignInManager<Customer>>();
              var refreshTokenProtector = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
              var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

              // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
              if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
                  timeProvider.GetUtcNow() >= expiresUtc ||
                  await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not Customer user)

              {
                  return TypedResults.Challenge();
              }

              var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
              return TypedResults.SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
          });

        return routeGroup;


        // routeGroup.MapPost("/info", async Task<Results<Ok<InfoResponse>, ValidationProblem, NotFound>>
        // (ClaimsPrincipal claimsPrincipal, [FromBody] InfoRequest infoRequest, HttpContext context, [FromServices] IServiceProvider sp) =>
        // {
        //     var userManager = sp.GetRequiredService<UserManager<Customer>>();
        //     if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
        //     {
        //         return TypedResults.NotFound();
        //     }

        //     if (!string.IsNullOrEmpty(infoRequest.NewEmail) && !_emailAddressAttribute.IsValid(infoRequest.NewEmail))
        //     {
        //         return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(infoRequest.NewEmail)));
        //     }

        //     if (!string.IsNullOrEmpty(infoRequest.NewPassword))
        //     {
        //         if (string.IsNullOrEmpty(infoRequest.OldPassword))
        //         {
        //             return CreateValidationProblem("OldPasswordRequired",
        //                 "The old password is required to set a new password. If the old password is forgotten, use /resetPassword.");
        //         }

        //         var changePasswordResult = await userManager.ChangePasswordAsync(user, infoRequest.OldPassword, infoRequest.NewPassword);
        //         if (!changePasswordResult.Succeeded)
        //         {
        //             return CreateValidationProblem(changePasswordResult);
        //         }
        //     }

        //     if (!string.IsNullOrEmpty(infoRequest.NewEmail))
        //     {
        //         var email = await userManager.GetEmailAsync(user);

        //         // if (email != infoRequest.NewEmail)
        //         // {
        //         //     await SendConfirmationEmailAsync(user, userManager, context, infoRequest.NewEmail, isChange: true);
        //         // }
        //     }

        //     return TypedResults.Ok(await CreateInfoResponseAsync(user, userManager));
        // });


    }

    public static async Task<Results<Ok, ValidationProblem>>
        RegisterCustomer(UserManager<Customer> userManager, CreateCustomerDto customer)
    {

        var email = customer.Email;
        var username = customer.Username;

        var newCustomer = new Customer()
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            UserName = customer.Username,
            PhoneNumber = customer.PhoneNumber,
            Accounts = customer.Accounts,
            PasswordHash = customer.Password
        };
        // await userStore.SetUserNameAsync(newCustomer, username, CancellationToken.None);
        // await emailStore.SetEmailAsync(newCustomer, email, CancellationToken.None);
        // Handle duplications

        var result = await userManager.CreateAsync(newCustomer, customer.Password);

        if (!result.Succeeded)
        {
            return CreateValidationProblem(result);
            // return Results.BadRequest(new { message = "Failed creating Customer", error = result.Errors });
        }
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok<CustomerInfoResponse>, ValidationProblem, NotFound>>
           InfoCustomer(ClaimsPrincipal claimsPrincipal, [FromServices] IServiceProvider sp)
    {
        var userManager = sp.GetRequiredService<UserManager<Customer>>();
        if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
        {
            return TypedResults.NotFound();
        }

        string email = claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
        // var response = await userManager.GetUserAsync(claimsPrincipal);
        var _context = sp.GetService<BankingContext>();
        var customer = _context.Customers.Select(cl => new CustomerInfoResponse
        {
            CustomerId = cl.Id,
            FirstName = cl.FirstName,
            LastName = cl.LastName,
            Email = cl.Email,
            PhoneNumber = cl.PhoneNumber
        }).FirstOrDefault(c => c.Email == email);

        return TypedResults.Ok(CreateCustomerInfoResponse(customer));
    }


    public static async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
            LoginCustomer(LoginDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider sp)
    {
        var signInManager = sp.GetRequiredService<SignInManager<Customer>>();

        var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        var isPersistent = (useCookies == true) && (useSessionCookies != true);
        signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);

        // if (result.RequiresTwoFactor)
        // {
        //     if (!string.IsNullOrEmpty(login.TwoFactorCode))
        //     {
        //         result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
        //     }
        //     else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
        //     {
        //         result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
        //     }
        // }

        if (!result.Succeeded)
        {
            return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
        }

        // The signInManager already produced the needed response in the form of a cookie or bearer token.
        return TypedResults.Empty;
    }

    // public static 

    private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
    TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { errorCode, [errorDescription] }
    });

    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        // We expect a single error code and description in the normal case.
        // This could be golfed with GroupBy and ToDictionary, but perf! :P
        Debug.Assert(!result.Succeeded);
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }

    private static CustomerInfoResponse CreateCustomerInfoResponse(CustomerInfoResponse user)
    {
        // var customerResponse = await userManager.FindByEmailAsync(email: user.Email);
        return new CustomerInfoResponse
        {
            CustomerId = user.CustomerId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Accounts = user.Accounts
            // Email = await userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email."),
            // IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user),
        };
    }

    private static async Task<InfoResponse> CreateInfoResponseAsync<TUser>(TUser user, UserManager<TUser> userManager)
       where TUser : class
    {
        // var customerResponse = await userManager.GetUserAsync(user);
        return new()
        {
            Email = await userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email."),
            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user),
        };
    }
}

