using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
           new User
           {
               Id = "b1a12345-6789-4def-bcfa-abcdef123456",
               FirstName = "John",
               LastName = "Doe",
               UserName = "johndoe",
               NormalizedUserName = "JOHNDOE",
               Email = "john@example.com",
               NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
               EmailConfirmed = true,
               PasswordHash = "AQAAAAEAACcQAAAAEBxU0K5pivQ5GkN5hHwIzHdQ3lqphKnCHJcDDFXOSFQ==", // Mock hash
               SecurityStamp = "e12345f6-abcd-4ef7-8910-abcdef987654",
               ConcurrencyStamp = Guid.NewGuid().ToString(),
               PhoneNumber = "1234567890",
               PhoneNumberConfirmed = true,
               TwoFactorEnabled = false,
               LockoutEnd = null,
               LockoutEnabled = true,
               AccessFailedCount = 0
           },
            new User
            {
                Id = "a1b23456-7890-4abc-bcfa-abcdef654321",
                FirstName = "Jane",
                LastName = "Smith",
                UserName = "janesmith",
                NormalizedUserName = "JANESMITH",
                Email = "jane@example.com",
                NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEBU7gR5sivV9FkN6jHqJkOdZ3qphyMoCJYdDCFZPTGQ==", // Mock hash
                SecurityStamp = "d23456e7-abcd-5fg8-8920-abcdef543210",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "0987654321",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.UtcNow.AddDays(7),
                LockoutEnabled = true,
                AccessFailedCount = 1
            }
        );
    }
}
