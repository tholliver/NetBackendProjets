using LinQueries;
using TCPData;
namespace LinQueries;

public class EmployeeQueries(List<Employee> employees, List<Department> departments) : IEmployeeQueries
{
    List<Employee> employees = employees;
    List<Department> departments = departments;

    public void DeleteEmployee(int id)
    {

    }

    public IEnumerable<Employee> GetEmployeeById(int id)
    {
        return employees.Where(emp => emp.Id == id);
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return employees;
    }


    public IEnumerable<Employee> SearchEmployeeByName(string name)
    {
        return employees.Where(emp => emp.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Employee> SearchEmployeeByLastName(string lastname)
    {
        return employees.Where(emp => emp.LastName.Contains(lastname, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Employee> GetEmployeesWithSalaryGreatherThan(decimal minimalAmount)
    {
        return from emp in employees
               where emp.AnnualSalary > minimalAmount
               select new Employee()
               {
                   Id = emp.Id,
                   FirstName = emp.FirstName,
                   LastName = emp.LastName,
                   AnnualSalary = emp.AnnualSalary,
                   IsManager = emp.IsManager,
                   DepartmentId = emp.DepartmentId
               };
    }

    public IEnumerable<EmployeeDepartment> FilterEmployeeByDepartment(string department)
    {
        var query = from emp in employees
                    join dept in departments
                    on emp.DepartmentId equals dept.Id
                    where dept.LongName.Contains(department, StringComparison.OrdinalIgnoreCase)
                    select new EmployeeDepartment
                    {
                        FullName = emp.FirstName + " " + emp.LastName,
                        DepartmentName = dept.LongName
                    };

        return query;
    }

    public IEnumerable<Employee> GetEmployeesById()
    {
        return from emp in employees
               select new Employee
               {
                   FirstName = emp.FirstName,
                   LastName = emp.LastName
               };
    }
}

public class EmployeeDepartment
{
    public required string FullName { set; get; }
    public required string DepartmentName { get; set; }
}