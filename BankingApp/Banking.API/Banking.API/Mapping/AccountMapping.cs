using Banking.API.Domain.Accounts;
using Banking.API.Dtos;

namespace Banking.API.Mapping;
public static class AccountMapping
{
    public static Account ToEntity(this CreateAccountDto account)
    {
        return new Account()
        {
            Type = account.Type,
            Description = account.Description,
            AccountNumber = account.AccountNumber,
            Currency = account.Currency,
            InterestRate = account.InterestRate,
            MinimumBalance = account.MinimumBalance,
            AllowedTransactions = account.AllowedTransactions,
            EarlyWithdrawalPenalty = account.EarlyWithdrawalPenalty
        };
    }

    public static Account ToEntity(this UpdateAccountDto account, int id)
    {
        return new Account()
        {
            AccountId = id,
            Type = account.Type,
            Description = account.Description,
            AccountNumber = account.AccountNumber,
            Currency = account.Currency,
            InterestRate = account.InterestRate,
            MinimumBalance = account.MinimumBalance,
            AllowedTransactions = account.AllowedTransactions,
            EarlyWithdrawalPenalty = account.EarlyWithdrawalPenalty
        };
    }
}