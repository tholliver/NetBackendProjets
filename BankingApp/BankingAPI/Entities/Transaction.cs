namespace BankingAPI.Entities;

public class Transaction
{
    public int TransactionId { get; set; }
    public string SourceAccountNumber { get; set; }  // New property for the source account
    public string TargetAccountNumber { get; set; }
    public StatusEnum Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }  // New property for when the transaction was created
    public DateTime ProcessedDate { get; set; }
    public string Description { get; set; }
    public TransactionTypeEnum TransactionType { get; set; }  // New property for transaction type
    public int AccountId { get; set; }
    public Account Account { get; set; }
}

public enum StatusEnum
{
    Pending,
    Completed,
    Failed,
    Cancelled
}

public enum TransactionTypeEnum
{
    Deposit,
    Withdrawal,
    Transfer,
    Payment
}
