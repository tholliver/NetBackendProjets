namespace Service.Contracts;

public interface IServiceManager
{
    IImageService ImageService { get; }
    IUserService UserService { get; }
    IAuthenticationService AuthenticationService { get; }
}
