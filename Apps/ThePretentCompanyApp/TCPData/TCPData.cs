namespace TCPData;
public static class Data
{
    public static List<Employee> GetEmployees()
    {
        return new List<Employee> {
                new Employee { Id = 1, FirstName = "Alice", LastName = "Johnson", AnnualSalary = 75000, IsManager = true, DepartmentId = 101 },
                new Employee { Id = 2, FirstName = "Bob", LastName = "Smith", AnnualSalary = 55000, IsManager = false, DepartmentId = 102 },
                new Employee { Id = 3, FirstName = "Carol", LastName = "Williams", AnnualSalary = 82000, IsManager = true, DepartmentId = 103 },
                new Employee { Id = 4, FirstName = "David", LastName = "Brown", AnnualSalary = 48000, IsManager = false, DepartmentId = 101 },
                new Employee { Id = 5, FirstName = "Eve", LastName = "Davis", AnnualSalary = 60000, IsManager = false, DepartmentId = 102 }
            };
    }

    public static List<Department> GetDepartments()
    {
        return new List<Department>
            {
                new Department { Id = 101, LongName = "Human Resources", ShortName = "HR" },
                new Department { Id = 102, LongName = "Information Technology", ShortName = "IT" },
                new Department { Id = 103, LongName = "Marketing and Sales", ShortName = "M&S" },
                new Department { Id = 104, LongName = "Finance and Accounting", ShortName = "Finance" },
                new Department { Id = 105, LongName = "Customer Support", ShortName = "Support" }
            };
    }
}