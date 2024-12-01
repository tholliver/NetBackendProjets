using BankingAPI.Data;
using BankingAPI.Dtos;
using BankingAPI.Entities;
using BankingAPI.Mapping;
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
            // var transaction = _context.Database.BeginTransactionAsync();
            var sourceAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == newTransaction.SourceAccountNumber);
            // Check the existing account number [source]

            if (sourceAccount is null)
            {
                return Results.NotFound(new { message = "No Source Account Found" });
            }

            var targetAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == newTransaction.TargetAccountNumber);

            // Check the existing account number [target]
            if (targetAccount is null)
            {
                return Results.NotFound(new { message = "No Target Account Found" });
            }

            // Check the origin Account has ORIGIN_AMOUNT > AMOUNT_SPECIFIED
            if (sourceAccount.MinimumBalance >= newTransaction.Amount)
            {

                await _context.Accounts.Where(a => a.AccountId == sourceAccount.AccountId)
                .ExecuteUpdateAsync(st => st.SetProperty(a => a.MinimumBalance, sourceAccount.MinimumBalance - newTransaction.Amount));
                //Here we set add the transaction to the source account

                var transactionCreated = await _context.Transactions.AddAsync(newTransaction.ToEntity());

                // If transaction success change Status 
                await _context.SaveChangesAsync();
                return Results.CreatedAtRoute(GetTransactionById, new { id = transactionCreated.Entity.TransactionId }, newTransaction);
            }
            else return Results.BadRequest(new { message = "No enought Founds" });
        });

        return TransactionsRoutes;
    }
}