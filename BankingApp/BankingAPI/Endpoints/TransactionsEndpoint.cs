using BankingAPI.Data;
using BankingAPI.Dtos;
using BankingAPI.Mapping;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Endpoints;

static class TransactionsEndpoint
{
    const string GetTransactionById = "GetTransactionById";
    public static RouteGroupBuilder MapTransactionEndpoint(this WebApplication app)
    {
        RouteGroupBuilder TransactionsRoutes = app.MapGroup("transactions");

        TransactionsRoutes.MapGet("/{id}", async (int id, BankingContext _context) =>
        {
            var transactionFound = await _context.Transactions
                                    .FirstOrDefaultAsync(e => e.TransactionId == id);
            return transactionFound is null ? Results.NotFound() : Results.Ok(transactionFound);
        }).WithName(GetTransactionById);

        TransactionsRoutes.MapPost("/", async (CreateTransactionDto newTransaction, BankingContext _context) =>
        {
            var transaction = _context.Database.BeginTransactionAsync();
            // Check the existing account number [Source] {once gotten -> Check valance}
            var sourceAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == newTransaction.AccountId);

            if (sourceAccount is null)
            {
                return Results.NotFound(new { message = "No Source Account Found" });
            }

            var targetAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == newTransaction.TargetAccountNumber);

            if (targetAccount is null)
            {
                return Results.NotFound(new { message = "No Target Account Found" });
            }
            // Check the existing account number [target]

            if (sourceAccount.MinimumBalance >= newTransaction.Amount)
            {
                // Check the origin Account has ORIGIN_AMOUNT > AMOUNT_T
                await _context.Accounts.Where(a => a.AccountId == sourceAccount.AccountId)
                .ExecuteUpdateAsync(st => st.SetProperty(a => a.MinimumBalance, sourceAccount.MinimumBalance - newTransaction.Amount));
                // If NroChanges > 0 ----> Save changes 
                await _context.Transactions.AddAsync(newTransaction.ToEntity());
                await _context.SaveChangesAsync();
                return Results.CreatedAtRoute(GetTransactionById, new { id = newTransaction.TransactionId }, newTransaction);
            }
            else return Results.BadRequest(new { message = "No enought Founds" });


        });

        return TransactionsRoutes;
    }
}