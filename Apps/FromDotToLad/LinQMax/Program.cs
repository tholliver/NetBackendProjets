using System.Diagnostics;
using LinQMax.Config;
using LinQMax.Queries;
using LinQMax.Services;

// var query = context.Customers
//     .OrderBy(c => c.FirstName)
//     .Select(c => new { c.FirstName, c.City })
//     .Take(5)
//     .ToList();

// Console.WriteLine($"Customers from London: {query}");
// foreach (var customer in query)
// {
//     Console.WriteLine($"{customer.FirstName} from {customer.City}");
// }


public partial class Program
{
    public static async Task Main()
    {
        var configuration = new ConfigReader();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var dbService = new DatabaseService(connectionString);

        var brandQueries = new BrandQueries(dbService);

        // Initialize the Stopwatch

        // Measure runtime of GetTotalProductsByBrand
        var brandsv2 = await brandQueries.GetTotalProductsByBrandV2();

        // Measure runtime of GetTotalProductsByBrandV2
        var brands = await brandQueries.GetTotalProductsByBrand();

        // foreach (var brand in brands)
        // {
        //     Console.WriteLine($"{brand.Name} has {brand.TotalProducts} products");
        // }
    }
}