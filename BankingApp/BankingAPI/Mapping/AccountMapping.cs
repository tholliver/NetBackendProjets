using BankingAPI.Dtos;
using BankingAPI.Entities;

namespace BankingAPI.Mapping;
public static class AccountMapping
{
    public static Account ToEntity(this CreateAccountDto account)
    {
        return new Account()
        {
            Type = account.Type,
            Description = account.Description,
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
            Currency = account.Currency,
            InterestRate = account.InterestRate,
            MinimumBalance = account.MinimumBalance,
            AllowedTransactions = account.AllowedTransactions,
            EarlyWithdrawalPenalty = account.EarlyWithdrawalPenalty
        };
    }
}