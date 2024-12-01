using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public class ServiceManager(
    IRepositoryManager repositoryManager,
    ILoggerManager logger,
    IMapper mapper,
    UserManager<User> userManager,
    IConfiguration configuration) : IServiceManager
{
    private readonly Lazy<IEmployeeService> _employeeService = new(() => new EmployeeService(repositoryManager, logger));
    private readonly Lazy<IAuthenticationService> _authenticationService = new(
        () => new AuthenticationService(logger, mapper, userManager, configuration));

    public IEmployeeService EmployeeService => _employeeService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
