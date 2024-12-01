using System;
using Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext _repositoryContext) : IRepositoryManager
{
    private readonly Lazy<IEmployeeRepository> _employeeRepository = new Lazy<IEmployeeRepository>();
    public IEmployeeRepository Employee => _employeeRepository.Value;
    public void Save() => _repositoryContext.SaveChanges();

    public Task SaveAsync()
    {
        return null;
    }
}
