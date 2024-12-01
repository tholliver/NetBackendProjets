using System.Diagnostics;
using Banking.API.Data;
using Banking.API.Domain.Customers;
using Banking.API.Dtos;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banking.API.Endpoints;
public static class CustomersEnpoint
{
    const string GetCustomerEndpoint = "GetCustomer";
    public static RouteGroupBuilder MapCustomerEndpoint(this WebApplication app)
    {
        // var timeProvider = app.ServiceProvider.GetRequiredService<TimeProvider>();
        // var bearerTokenOptions = app.ServiceProvider.GetRequiredService<IOptionsMonitor<BearerTokenOptions>>();

        RouteGroupBuilder group = app.MapGroup("customers"); //WithParameterValidation();

        group.MapGet("/", GetCustomersAsync);
        group.MapGet("/{id}", GetCustomerById).WithName(GetCustomerEndpoint);
        group.MapPost("/", RegisterCustomerAsync);
        group.MapPost("/login", LoginAsync);

        return group;
    }

    public static async Task<Results<NotFound, Ok<CustomerInfoResponse>>> GetCustomerById(string id, BankingContext context)
    {
        Console.WriteLine($"Here on the {id}");
        var customerFound = await context.Customers
        .Where(b => b.Id == id).FirstOrDefaultAsync();

        return customerFound is null ? TypedResults.NotFound() : TypedResults.Ok(customerFound.ToCustomerResponse());
    }

    public static async Task<IResult> GetCustomersAsync(BankingContext _context)
    {
        var customers = await _context.Customers.ToListAsync();
        return Results.Ok(customers);
    }

    public static async Task<IResult> RegisterCustomerAsync(
        UserManager<Customer> userManager,
        CreateCustomerDto customer)
    {
        // Validations
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
        // var transaction = await _context.Database.BeginTransactionAsync();
        var creationResult = await userManager.CreateAsync(newCustomer, newCustomer.PasswordHash);
        if (!creationResult.Succeeded)
        {
            return Results.BadRequest(new { message = "Failed creating Customer", error = creationResult.Errors });
        }

        return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = newCustomer.Id }, newCustomer);
    }

    public static async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
       LoginAsync(LoginDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] SignInManager<Customer> signInManager)
    {
        var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        var isPersistent = (useCookies == true) && (useSessionCookies != true);
        signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var result = await signInManager.PasswordSignInAsync(userName: login.Email,
                                                            password: login.Password,
                                                            isPersistent: false,
                                                            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
        }
        return TypedResults.Empty;
    }


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
}

public class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

// group.MapPost("", async Task<Results<Ok<AccessTokenResponse>, UnauthorizedHttpResult, SignInHttpResult, ChallengeHttpResult>>
// ([FromBody] RefreshRequest refreshRequest, [FromServices] IServiceProvider sp) =>
// {
//     var signInManager = sp.GetRequiredService<SignInManager<TUser>>();
//     var refreshTokenProtector = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
//     var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

//     // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
//     if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
//         timeProvider.GetUtcNow() >= expiresUtc ||
//         await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not TUser user)

//     {
//         return TypedResults.Challenge();
//     }

//     var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
//     return TypedResults.SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
// });