
using LinQMax.Contract;
using LinQMax.Data;

namespace LinQMax.Services;

public class DatabaseService(string connectionString) : IDatabaseService
{
    private readonly string _connectionString = connectionString;
    private LinQMaxContext _context;

    public LinQMaxContext GetContext()
    {
        _context ??= new LinQMaxContext(_connectionString);
        return _context;
    }

    public async Task<bool> TestConnection()
    {
        return true;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}