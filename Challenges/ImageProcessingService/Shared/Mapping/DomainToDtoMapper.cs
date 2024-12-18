using Entities.Models;
using Shared.DataTransferObjects;

namespace Shared.Mapping;

public static class DomainToDtoMapper
{
    public static User ToUser(this UserForRegistrationDto user)
    {
        return new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            PasswordHash = user.Password, // Simulate password hashing
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
        };
    }
}
