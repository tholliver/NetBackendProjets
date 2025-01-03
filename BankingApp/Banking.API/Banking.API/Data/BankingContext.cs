using Banking.API.Domain.Accounts;
using Banking.API.Domain.Customers;
using Banking.API.Domain.Transactions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banking.API.Data;
public class BankingContext : IdentityDbContext<Customer>
{
    public BankingContext(DbContextOptions<BankingContext> options)
    : base(options)
    {

    }
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().HasMany(e => e.Accounts)
                                .WithOne(e => e.Customer)
                                .HasForeignKey(e => e.CustomerId)
                                .IsRequired();

        modelBuilder.Entity<Account>().HasMany(e => e.Transactions)
                                .WithOne(e => e.Account)
                                .HasForeignKey(e => e.AccountId)
                                .IsRequired();

        // Seeding initial Data
        // var customers = SeedData.GetCustomers();
        // var accounts = SeedData.GetAccounts();

        // // Removing navigation properties to avoid circular reference issues
        // foreach (var customer in customers)
        // {
        //     customer.Accounts = null;
        // }

        // foreach (var account in accounts)
        // {
        //     account.Customer = null;
        // }

        // modelBuilder.Entity<Customer>().HasData(customers);
        // modelBuilder.Entity<Account>().HasData(accounts);
    }
}
