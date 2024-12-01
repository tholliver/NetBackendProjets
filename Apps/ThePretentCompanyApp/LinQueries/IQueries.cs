using TCPData;

namespace LinQueries;
public interface IEmployeeQueries
{
    IEnumerable<Employee> GetAllEmployees();
    IEnumerable<Employee> GetEmployeeById(int id);
    IEnumerable<Employee> SearchEmployeeByName(string name);

    void DeleteEmployee(int id);
}

public interface IDepartmentQueries
{
    IEnumerable<Department> GetDepartaments();
    IEnumerable<Department> GetByDepartment();
}