
using LinQMax.Contract;
using LinQMax.Dtos;
using LinQMax.Models;

namespace LinQMax.Queries;
public class CustomerQueries
{
    private readonly IDatabaseService _databaseService;

    public CustomerQueries(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public List<Customer> GetCustomersFromLondon()
    {
        using var context = _databaseService.GetContext();
        return context.Customers
            .Where(c => c.City == "London")
            .ToList();
    }

    public List<Customer> GetCustomersFromLondonWithOrder()
    {
        using var context = _databaseService.GetContext();
        return context.Customers
            .Where(c => c.City == "London")
            .OrderBy(c => c.FirstName)
            .ToList();
    }

    public List<Customer> GetCustomersFromLondonWithOrderAndTake()
    {
        using var context = _databaseService.GetContext();
        return context.Customers
            .Where(c => c.City == "London")
            .OrderBy(c => c.FirstName)
            .Take(5)
            .ToList();
    }

    public List<CustomerDto> GetCustomersFromLondonWithOrderAndTakeSelect()
    {
        using var context = _databaseService.GetContext();
        return context.Customers
            .Where(c => c.City == "London")
            .OrderBy(c => c.FirstName)
            .Select(c => new CustomerDto { FirstName = c.FirstName, City = c.City })
            .Take(5)
            .ToList();
    }
}