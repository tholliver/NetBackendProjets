using Banking.API.Domain.Customers;
using Banking.API.Domain.Transactions;

namespace Banking.API.Domain.Accounts;

public class Account
{
    public int AccountId { get; set; }
    public required string AccountNumber { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
    public required string Currency { get; set; }
    public decimal InterestRate { get; set; }
    public decimal MinimumBalance { get; set; }
    public int AllowedTransactions { get; set; }
    public bool EarlyWithdrawalPenalty { get; set; }
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}