namespace BankingAPI.Entities;

public class Account
{
    public int AccountId { get; set; }
    public string AccountNumber { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public decimal InterestRate { get; set; }
    public decimal MinimumBalance { get; set; }
    public int AllowedTransactions { get; set; }
    public bool EarlyWithdrawalPenalty { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}