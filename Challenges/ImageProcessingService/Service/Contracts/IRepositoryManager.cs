namespace Service.Contracts;

public interface IRepositoryManager
{
    IUserRepository Users { get; }
    IImageRepository Images { get; }
    Task SaveAsync();
}
