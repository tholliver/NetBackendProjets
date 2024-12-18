using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Config;

public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = "Administator",
                NormalizedName = "ADMINISTATOR"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
            },
            new IdentityRole
            {
                Name = "Developer",
                NormalizedName = "DEVELOPER",
            }
        );
    }
}
