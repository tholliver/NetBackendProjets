namespace Service.Contracts;

public interface IRepositoryManager
{
    // IUser User { get; }
    IImageRepository Images { get; }
    Task SaveAsync();
}
