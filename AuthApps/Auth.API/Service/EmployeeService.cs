using Contracts;
using Entities.Models;

namespace Service;

internal sealed class EmployeeService(IRepositoryManager repository, ILoggerManager logger) : IEmployeeService
{
    private readonly IRepositoryManager _repositoryManager = repository;
    private readonly ILoggerManager _logger = logger;
}
