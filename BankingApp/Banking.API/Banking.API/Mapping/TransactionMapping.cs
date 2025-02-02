using Banking.API.Domain.Transactions;
using Banking.API.Dtos;

namespace Banking.API.Mapping;
public static class TransactionMapping
{
    public static Transaction ToEntity(this CreateTransactionDto newTransaction)
    {
        return new Transaction()
        {
            // TransactionId = newTransaction.TransactionId,
            SourceAccountNumber = newTransaction.SourceAccountNumber,
            TargetAccountNumber = newTransaction.TargetAccountNumber,
            Status = newTransaction.Status,
            Amount = newTransaction.Amount,
            CreatedDate = newTransaction.CreatedDate,
            ProcessedDate = newTransaction.ProcessedDate,
            Description = newTransaction.Description,
            TransactionType = newTransaction.TransactionType,
            AccountId = newTransaction.AccountId
        };
    }
}