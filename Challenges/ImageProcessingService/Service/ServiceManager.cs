using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service;

public class ServiceManager(IRepositoryManager repositoryManager,
                            UserManager<User> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IOptions<JwtConfiguration> configuration) : IServiceManager
{
    private readonly Lazy<IImageService> _imageService = new Lazy<IImageService>(
        () => new ImageService(repositoryManager)
    );

    private readonly Lazy<IUserService> _userService = new Lazy<IUserService>(
() => new UserService(repositoryManager)
    );
    private readonly Lazy<IAuthenticationService> _authenticationService =
     new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, roleManager, configuration));

    public IImageService ImageService => _imageService.Value;
    public IUserService UserService => _userService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
