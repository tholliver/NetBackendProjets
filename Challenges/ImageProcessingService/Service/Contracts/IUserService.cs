using Entities.Models;

namespace Service.Contracts;

public interface IUserService
{
    Task<User> GetUserByUserName(string userName, bool trackChanges);
}
