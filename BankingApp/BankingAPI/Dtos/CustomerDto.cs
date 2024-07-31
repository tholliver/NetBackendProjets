using BankingAPI.Entities;

namespace BankingAPI.Dtos;
public interface ICustomerDto
{
    string FirstName { get; init; }
    string LastName { get; init; }
    string Email { get; init; }
    string Phone { get; init; }
    string Password { get; init; }
}

public record class CreateCustomerDto
 : ICustomerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Password { get; init; }
    public required ICollection<Account> Accounts { get; init; }
}

public record class UpdateCustomerDto
 : ICustomerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Password { get; init; }
}