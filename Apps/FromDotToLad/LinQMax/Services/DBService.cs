
using LinQMax.Contract;
using LinQMax.Data;

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
        return await Task.FromResult(true);
    }


}