using BankingAPI.Entities;

namespace BankingAPI.Dtos;

public interface ITransactionDto
{
    public int TransactionId { get; init; }
    public string SourceAccountNumber { get; init; }  // New property for the source account
    public string TargetAccountNumber { get; init; }
    public StatusEnum Status { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedDate { get; init; }  // New property for when the transaction was created
    public DateTime ProcessedDate { get; init; }
    public string Description { get; init; }
    public TransactionTypeEnum TransactionType { get; init; }  // New property for transaction type
    public int AccountId { get; init; }
}

public record class CreateTransactionDto : ITransactionDto
{
    public int TransactionId { get; init; }
    public string SourceAccountNumber { get; init; }  // New property for the source account
    public string TargetAccountNumber { get; init; }
    public StatusEnum Status { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedDate { get; init; }  // New property for when the transaction was created
    public DateTime ProcessedDate { get; init; }
    public string Description { get; init; }
    public TransactionTypeEnum TransactionType { get; init; }  // New property for transaction type
    public int AccountId { get; init; }
}