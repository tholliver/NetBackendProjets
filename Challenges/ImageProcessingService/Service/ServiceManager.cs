using System;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
    private readonly Lazy<IAuthenticationService> _authenticationService =
     new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, roleManager, configuration));

    public IImageService ImageService => _imageService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
