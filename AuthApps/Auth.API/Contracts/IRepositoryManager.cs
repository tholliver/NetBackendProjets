using System;

namespace Contracts;

public interface IRepositoryManager
{
    IEmployeeRepository Employee { get; }
    Task SaveAsync();
}
