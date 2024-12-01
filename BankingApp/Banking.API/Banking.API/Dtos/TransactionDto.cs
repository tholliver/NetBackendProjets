using Banking.API.Domain.Transactions;

namespace Banking.API.Dtos;

public interface ITransactionDto
{
    // public int TransactionId { get; init; }
    public string SourceAccountNumber { get; init; }
    public string TargetAccountNumber { get; init; }
    public StatusEnum Status { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime ProcessedDate { get; init; }
    public string Description { get; init; }
    public TransactionTypeEnum TransactionType { get; init; }
    public int AccountId { get; init; }
}

public record class CreateTransactionDto : ITransactionDto
{
    public required string SourceAccountNumber { get; init; }
    public required string TargetAccountNumber { get; init; }
    public StatusEnum Status { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime ProcessedDate { get; init; }
    public string Description { get; init; }
    public TransactionTypeEnum TransactionType { get; init; }
    public int AccountId { get; init; }
}

public record class TransactionResponse : ITransactionDto
{
    public required string SourceAccountNumber { get; init; }
    public required string TargetAccountNumber { get; init; }
    public StatusEnum Status { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime ProcessedDate { get; init; }
    public string Description { get; init; }
    public TransactionTypeEnum TransactionType { get; init; }
    public int AccountId { get; init; }
}