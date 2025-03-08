using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Contracts;

public class UserRepository(RepositoryContext repositoryContext) :
        RepositoryBase<User>(repositoryContext), IUserRepository
{
    public async Task<User?> GetUserByUserName(string userName, bool trackChanges)
    {
        return await FindByCondition(c => c.UserName.Equals(userName), trackChanges)
                                                              .SingleOrDefaultAsync();
    }
}