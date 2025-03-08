
using LinQMax.Contract;
using LinQMax.Data;
using Microsoft.EntityFrameworkCore;

namespace LinQMax.Services;

public class DatabaseService(string connectionString) : IDatabaseService
{
    private readonly string _connectionString = connectionString;

    public LinQMaxContext GetContext()
    {
        return new LinQMaxContext(_connectionString);
    }

    public async Task<bool> TestConnection()
    {
        using var context = new LinQMaxContext(_connectionString);
        var result = context.Database.ExecuteSqlRawAsync("SELECT 1");
        Console.WriteLine(result);
        return await Task.FromResult(true);
    }


}