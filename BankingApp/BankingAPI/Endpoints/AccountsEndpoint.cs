using BankingAPI.Data;
using BankingAPI.Dtos;
using BankingAPI.Entities;
using BankingAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Endpoints;

static class AccountsEndpoint
{

    public static RouteGroupBuilder MapAccountEndpoint(this WebApplication app)
    {
        const string GetAccountEndpoint = "GetAccount";

        RouteGroupBuilder AccountRoutes = app.MapGroup("accounts");

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