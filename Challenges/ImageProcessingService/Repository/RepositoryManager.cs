using System;
using Service.Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<IImageRepository> _imageRepository =
                                new(() => new ImageRepository(repositoryContext));
    private readonly RepositoryContext _repositoryContext = repositoryContext;

    public IImageRepository Images => _imageRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
