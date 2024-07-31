using BankingAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Data;
public class BankingContext(DbContextOptions<BankingContext> options)
: DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().HasMany(e => e.Accounts)
                                .WithOne(e => e.Customer)
                                .HasForeignKey(e => e.CustomerId)
                                .IsRequired();

        modelBuilder.Entity<Account>().HasMany(e => e.Transactions)
                                .WithOne(e => e.Account)
                                .HasForeignKey(e => e.AccountId)
                                .IsRequired();
    }
}
