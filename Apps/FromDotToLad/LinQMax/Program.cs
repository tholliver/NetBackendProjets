using LinQMax.Config;
using LinQMax.Data;
using LinQMax.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigReader();
var connectionString = configuration.GetConnectionString("DefaultConnection");
using var dbService = new DatabaseService(connectionString);

var context = dbService.GetContext();

var query = context.Customers
    .OrderBy(c => c.FirstName)
    .Select(c => new { c.FirstName, c.City }).Take(5);

Console.WriteLine($"Customers from London: {query.ToList().Count()}");
foreach (var customer in query)
{
    Console.WriteLine($"{customer.FirstName} from {customer.City}");
}





