using System;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public class ServiceManager(IRepositoryManager repositoryManager,
                            UserManager<User> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IConfiguration configuration) : IServiceManager
{
    private readonly Lazy<IImageService> _imageService = new Lazy<IImageService>(
        () => new ImageService(repositoryManager)
    );
    private readonly Lazy<IAuthenticationService> _authenticationService =
     new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, roleManager, configuration));

    public IImageService ImageService => _imageService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
