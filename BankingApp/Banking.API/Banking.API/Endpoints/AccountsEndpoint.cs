using Banking.API.Domain.Accounts;
using Banking.API.Data;
using Banking.API.Dtos;
using Banking.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Banking.API.Endpoints;

static class AccountsEndpoint
{

    public static RouteGroupBuilder MapAccountEndpoint(this WebApplication app)
    {
        const string GetAccountEndpoint = "GetAccount";

        RouteGroupBuilder AccountRoutes = app.MapGroup("accounts");

        // Has to be all accounts for one CUSTOMER

        AccountRoutes.MapGet("/", async (int id, BankingContext _context) =>
        {
            var foundAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);
            return foundAccount is null ? Results.NotFound() : Results.Ok(foundAccount);
        }).WithName(GetAccountEndpoint);

        // await dbContext.Games
        //              .Include(game => game.Genre)
        //              .Select(game => game.ToGameSummaryDto())
        //              .AsNoTracking()
        //              .ToListAsync());
        AccountRoutes.MapGet("/{id}", async (string id, BankingContext _context) =>
        {
            // var customerFound = await _context.Customers.FindAsync(a => a.Id == id);
            // Handle if has 0 accounts 
            var foundAccounts = await _context.Accounts.Where(c => c.CustomerId == id)
                                            .Include(t => t.Transactions).ToListAsync();

            return foundAccounts is null ? Results.NotFound() : Results.Ok(foundAccounts);
        });

        // Specific customer account
        AccountRoutes.MapGet("/{customerId}/{accountId}", async (string customerId, int accountId, BankingContext _context) =>
       {
           // var customerFound = await _context.Customers.FindAsync(a => a.Id == id);
           // Handle if has 0 accounts 
           var foundAccounts = await _context.Accounts
                                    .Where(c => c.CustomerId == customerId && c.AccountId == accountId)
                                    .Include(t => t.Transactions).ToListAsync();

           return foundAccounts is null ? Results.NotFound() : Results.Ok(foundAccounts);
       });

        AccountRoutes.MapPost("/{id}", async (string id, CreateAccountDto newAccount, BankingContext _context) =>
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            var customerFound = await _context.Customers.FirstOrDefaultAsync(a => a.Id == id);

            Account account = newAccount.ToEntity();
            // Here handle if Customer does not exists
            try
            {
                customerFound.Accounts.Add(account);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            return Results.CreatedAtRoute(GetAccountEndpoint, new { id = account.AccountId }, newAccount);
        });

        return AccountRoutes;
    }
}