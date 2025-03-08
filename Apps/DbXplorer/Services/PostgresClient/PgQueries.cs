using System;

namespace DbXplorer.Services.PostgresClient;

public class PgQueries
{
    public async Task<List<Dictionary<string, object>>> GetTableDataAsync(string conn, string dbType,
        string tableName, int rowCount)
    {
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        return rows;
    }
}
