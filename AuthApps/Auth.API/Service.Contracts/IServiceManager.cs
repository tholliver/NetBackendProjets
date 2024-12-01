using Service.Contracts;

namespace Contracts;

public interface IServiceManager
{
    IEmployeeService EmployeeService { get; }
    IAuthenticationService AuthenticationService { get; }
}
