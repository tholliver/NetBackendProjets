using Entities.Models;

namespace Service.Contracts;

public interface IUserRepository
{
    Task<User?> GetUserByUserName(string userName, bool trackChanges);

}
