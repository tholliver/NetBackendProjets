using Banking.API.Dtos;

namespace Banking.API.Domain.Transactions;

public static class TransactionDtoExtension
{
    public static TransactionResponse ToTransactionResponse(this Transaction transaction)
    {
        return new TransactionResponse
        {
            SourceAccountNumber = transaction.SourceAccountNumber,
            TargetAccountNumber = transaction.TargetAccountNumber,
            Status = transaction.Status,
            Amount = transaction.Amount,
            CreatedDate = transaction.CreatedDate,
            ProcessedDate = transaction.ProcessedDate,
            Description = transaction.Description,
            TransactionType = transaction.TransactionType,
            AccountId = transaction.AccountId
        };
    }
}