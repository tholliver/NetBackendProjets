using Microsoft.AspNetCore.Identity;

namespace BankingAPI.Entities;

public class Customer : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Account> Accounts { get; set; }

    // public Customer() { }
}

