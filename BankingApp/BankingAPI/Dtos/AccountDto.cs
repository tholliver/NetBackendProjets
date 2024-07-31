namespace BankingAPI.Dtos;

public interface IAccountDto
{
    public string Type { get; init; }
    public string AccountNumber { get; init; }
    public string Description { get; init; }
    public string Currency { get; init; }
    public decimal InterestRate { get; init; }
    public decimal MinimumBalance { get; init; }
    public int AllowedTransactions { get; init; }
    public bool EarlyWithdrawalPenalty { get; init; }
    // public int CustomerId { get; init; }
}


public record class CreateAccountDto : IAccountDto
{
    public string AccountNumber { get; init; }
    public string Type { get; init; }
    public string Description { get; init; }
    public string Currency { get; init; }
    public decimal InterestRate { get; init; }
    public decimal MinimumBalance { get; init; }
    public int AllowedTransactions { get; init; }
    public bool EarlyWithdrawalPenalty { get; init; }
    // public int CustomerId { get; init; }
}

public record class UpdateAccountDto : IAccountDto
{
    public string AccountNumber { get; init; }
    public string Type { get; init; }
    public string Description { get; init; }
    public string Currency { get; init; }
    public decimal InterestRate { get; init; }
    public decimal MinimumBalance { get; init; }
    public int AllowedTransactions { get; init; }
    public bool EarlyWithdrawalPenalty { get; init; }
    // public int CustomerId { get; init; }
}