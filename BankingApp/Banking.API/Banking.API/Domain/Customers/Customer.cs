using Banking.API.Domain.Accounts;
using Microsoft.AspNetCore.Identity;

namespace Banking.API.Domain.Customers;

public class Customer : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<Account>? Accounts { get; set; }

}


