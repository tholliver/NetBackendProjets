namespace Banking.API.Domain.Customers;

public record CustomerRecord(
    string Id,
    string FirstName,
    string LastName,
    string? UserName,
    string? NormalizedUserName,
    string? Email,
    string? NormalizedEmail,
    bool EmailConfirmed,
    string? SecurityStamp,
    string? ConcurrencyStamp,
    string? PhoneNumber,
    bool PhoneNumberConfirmed,
    bool TwoFactorEnabled,
    DateTimeOffset? LockoutEnd,
    bool LockoutEnabled,
    int AccessFailedCount
);