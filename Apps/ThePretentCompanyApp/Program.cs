using LinQueries;
using TCPData;
using TCPExtensions;

namespace ThePretentCompanyApp;

class Program
{
    static void Main(string[] args)
    {
        List<Employee> employeeList = Data.GetEmployees();
        List<Department> departmentList = Data.GetDepartments();

        // By extension
        var filteredEmployees = employeeList.Filter(emp =>
            emp.IsManager == false
        );


        var employeeQs = new EmployeeQueries(employeeList, departmentList);
        // Console.WriteLine("All employees");
        // employeeQs.GetAllEmployees().Print();
        // Console.WriteLine("Employee ID");
        // employeeQs.GetEmployeeById(2).Print();
        // employeeQs.SearchEmployeeByName("car").Print();
        // employeeQs.SearchEmployeeByLastName("will").Print();
        employeeQs.FilterEmployeeByDepartment("human").Print();
        employeeQs.GetEmployeesWithSalaryGreatherThan(76000.0M).Print();

        employeeQs.ToString();
    }
}
