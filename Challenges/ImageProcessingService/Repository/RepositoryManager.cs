using System;
using Service.Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<IImageRepository> _imageRepository =
                                new(() => new ImageRepository(repositoryContext));

    private readonly Lazy<IUserRepository> _userRepository =
                        new(() => new UserRepository(repositoryContext));
    private readonly RepositoryContext _repositoryContext = repositoryContext;

    public IImageRepository Images => _imageRepository.Value;
    public IUserRepository Users => _userRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
