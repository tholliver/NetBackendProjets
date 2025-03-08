using Entities.Models;
using Service.Contracts;

namespace Service;

internal sealed class UserService(IRepositoryManager repository) : IUserService
{
    public Task<User> GetUserByUserName(string userName, bool trackChanges)
    {
        return repository.Users.GetUserByUserName(userName, trackChanges);
    }
}