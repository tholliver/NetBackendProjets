using Banking.API.Data;
using Banking.API.Domain.Transactions;
using Banking.API.Dtos;
using Banking.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Banking.API.Endpoints;

static class TransactionsEndpoint
{
    const string GetTransactionByIdEndpoint = "GetTransactionById";
    public static RouteGroupBuilder MapTransactionEndpoint(this WebApplication app)
    {
        RouteGroupBuilder TransactionsRoutes = app.MapGroup("transactions");

        TransactionsRoutes.MapGet("/{id}", GetTransactionById).WithName(GetTransactionByIdEndpoint);
        TransactionsRoutes.MapPost("/", CreateTransaction);

        return TransactionsRoutes;
    }

    public static async Task<IResult> GetTransactionById(int id, BankingContext _context)
    {
        var transactionFound = await _context.Transactions
                                .FirstOrDefaultAsync(e => e.TransactionId == id);
        return transactionFound is null ? Results.NotFound() : Results.Ok(transactionFound);
    }

    public static async Task<IResult> CreateTransaction(CreateTransactionDto newTransaction, BankingContext _context)
    {
        // var transaction = _context.Database.BeginTransactionAsync();
        var sourceAccount = await _context.Accounts.FirstOrDefaultAsync(
            a => a.AccountNumber == newTransaction.SourceAccountNumber);

        if (sourceAccount is null)
        {
            return Results.NotFound(new { message = "No Source Account Found" });
        }

        var targetAccount = await _context.Accounts.FirstOrDefaultAsync(
            a => a.AccountNumber == newTransaction.TargetAccountNumber);

        if (targetAccount is null)
        {
            return Results.NotFound(new { message = "No Target Account Found" });
        }

        // Check the origin Account has ORIGIN_AMOUNT > AMOUNT_SPECIFIED
        if (sourceAccount.MinimumBalance >= newTransaction.Amount)
        {

            await _context.Accounts.Where(a => a.AccountId == sourceAccount.AccountId)
            .ExecuteUpdateAsync(st => st.SetProperty(
                   a => a.MinimumBalance, sourceAccount.MinimumBalance - newTransaction.Amount));
            var transactionCreated = await _context.Transactions.AddAsync(newTransaction.ToEntity());

            await _context.SaveChangesAsync();
            return Results.CreatedAtRoute(GetTransactionByIdEndpoint, new { id = transactionCreated.Entity.TransactionId }, newTransaction);
        }
        else return Results.BadRequest(new { message = "No enought Founds" });
    }

}