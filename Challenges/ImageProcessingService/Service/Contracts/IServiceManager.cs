namespace Service.Contracts;

public interface IServiceManager
{
    IImageService ImageService { get; }
    IAuthenticationService AuthenticationService { get; }
}
