using BankingAPI.Entities;

namespace BankingAPI.Dtos;
public interface ICustomerDto
{
    string FirstName { get; init; }
    string LastName { get; init; }
    string PhoneNumber { get; init; }
    ICollection<Account> Accounts { get; init; }

}

public record class CreateCustomerDto
 : ICustomerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required ICollection<Account> Accounts { get; init; }
}

public record class UpdateCustomerDto
 : ICustomerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required ICollection<Account> Accounts { get; init; }
}

public record class CustomerInfoResponse : ICustomerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public ICollection<Account> Accounts { get; init; }
}