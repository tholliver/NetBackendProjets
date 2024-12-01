// using System.Collections.Generic;
// using Banking.API.Entities;

// namespace Banking.API.Data;

// public static class SeedData
// {
//     public static List<Customer> GetCustomers()
//     {
//         return new List<Customer>
//             {
//                 new Customer
//                 {
//                     CustomerId = "1",
//                     FirstName = "John",
//                     LastName = "Doe",
//                     Email = "john.doe@example.com",
//                     Phone = "123-456-7890",
//                     Password = "password123",
//                     Accounts = new List<Account>
//                     {
//                         new Account
//                         {
//                             AccountId = 1,
//                             AccountNumber = "123456789",
//                             Type = "Checking",
//                             Description = "John's checking account",
//                             Currency = "USD",
//                             InterestRate = 0.01m,
//                             MinimumBalance = 1000.00m,
//                             AllowedTransactions = 100,
//                             EarlyWithdrawalPenalty = false,
//                             CustomerId = "1"
//                         },
//                         new Account
//                         {
//                             AccountId = 2,
//                             AccountNumber = "987654321",
//                             Type = "Savings",
//                             Description = "John's savings account",
//                             Currency = "USD",
//                             InterestRate = 0.05m,
//                             MinimumBalance = 500.00m,
//                             AllowedTransactions = 10,
//                             EarlyWithdrawalPenalty = true,
//                             CustomerId = "1"
//                         }
//                     }
//                 },
//                 new Customer
//                 {
//                     CustomerId = "2",
//                     FirstName = "Jane",
//                     LastName = "Smith",
//                     Email = "jane.smith@example.com",
//                     Phone = "098-765-4321",
//                     Password = "password456",
//                     Accounts = new List<Account>
//                     {
//                         new Account
//                         {
//                             AccountId = 3,
//                             AccountNumber = "111222333",
//                             Type = "Checking",
//                             Description = "Jane's checking account",
//                             Currency = "USD",
//                             InterestRate = 0.02m,
//                             MinimumBalance = 2000.00m,
//                             AllowedTransactions = 50,
//                             EarlyWithdrawalPenalty = false,
//                             CustomerId = "2"
//                         }
//                     }
//                 }
//             };
//     }

//     public static List<Account> GetAccounts()
//     {
//         var accounts = new List<Account>();

//         foreach (var customer in GetCustomers())
//         {
//             foreach (var account in customer.Accounts)
//             {
//                 accounts.Add(account);
//             }
//         }

//         return accounts;
//     }

// }
