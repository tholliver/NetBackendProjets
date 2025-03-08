using System.Data;
using System.Data.Common;
using DbXplorer.Models;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

namespace DbXplorer.Services;

public interface IDbService
{
    List<DbConnectionInfo> GetAllConnOptions();
    DbConnectionInfo? GetDbById(int id);
    void AddDbConn(DbConnectionInfo newDatabase);
    Task<List<string>> GetSchemasAsync(string connectionString, string dbType);
    Task<List<DbSchema>> FetchSchemasAndObjectsAsync(string connectionString);
    Task<List<string>> GetTableNamesAsync(string connectionString, string dbType);
    List<Dictionary<string, object>> ExecuteQuery(string connectionString, string dbType, string query);
    bool TestConnection(string connectionString, string dbType);
}

public class DbService : IDbService
{
    private readonly DbXplorerContext _context;

    public DbService(DbXplorerContext context)
    {
        _context = context;
    }

    public List<DbConnectionInfo> GetAllConnOptions() => _context.DbCredentials.ToList();

    public DbConnectionInfo? GetDbById(int id) => _context.DbCredentials.FirstOrDefault(d => d.Id == id);

    public void AddDbConn(DbConnectionInfo newDatabase)
    {
        _context.DbCredentials.Add(newDatabase);
        _context.SaveChanges();
    }

    public bool TestConnection(string connectionString, string dbType)
    {
        try
        {
            using var connection = CreateConnection(connectionString, dbType);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1;";
            var result = command.ExecuteScalar();
            Console.WriteLine($"Test query result: {result}");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
            return false;
        }
    }

    public async Task<List<string>> GetSchemasAsync(string connectionString, string dbType)
    {
        var schemas = new List<string>();

        if (dbType != "PostgreSQL")
            throw new NotSupportedException("Schema fetching only supported for PostgreSQL currently.");

        const string query = @"
            SELECT schema_name
            FROM information_schema.schemata
            WHERE schema_name NOT LIKE 'pg_%' AND schema_name != 'information_schema'";

        await using var connection = CreateConnection(connectionString, dbType);
        await connection.OpenAsync();

        schemas.AddRange(await ExecuteStringListQueryAsync(connection, query));

        return schemas;
    }

    public async Task<List<DbSchema>> FetchSchemasAndObjectsAsync(string connectionString)
    {
        var schemas = new List<DbSchema>();

        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var schemaNames = await GetSchemasAsync(connectionString, "PostgreSQL");

        foreach (var schemaName in schemaNames)
        {
            var schema = new DbSchema { Name = schemaName };

            schema.Tables = await FetchNamesAsync(connection, schemaName, "BASE TABLE");
            schema.Views = await FetchNamesAsync(connection, schemaName, "VIEW");
            schema.Functions = await FetchFunctionsAsync(connection, schemaName);
            schema.Sequences = await FetchSequencesAsync(connection, schemaName);

            schemas.Add(schema);
        }

        return schemas;
    }

    public async Task<List<string>> GetTableNamesAsync(string connectionString, string dbType)
    {
        await using var connection = CreateConnection(connectionString, dbType);
        await connection.OpenAsync();

        string query = dbType switch
        {
            "mysql" => "SHOW TABLES",
            "PostgreSQL" => "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'",
            "sqlserver" => "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'",
            _ => throw new NotSupportedException($"Database type '{dbType}' is not supported.")
        };

        return await ExecuteStringListQueryAsync(connection, query);
    }

    public List<Dictionary<string, object>> ExecuteQuery(string connectionString, string dbType, string query)
    {
        using var connection = CreateConnection(connectionString, dbType);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = command.ExecuteReader();
        return ReadResults(reader);
    }

    private DbConnection CreateConnection(string connectionString, string dbType)
    {
        return dbType.ToLower() switch
        {
            "mysql" => new MySqlConnection(connectionString),
            "postgresql" => new NpgsqlConnection(connectionString),
            "sqlserver" => new SqlConnection(connectionString),
            _ => throw new NotSupportedException($"Unsupported database type '{dbType}'")
        };
    }

    private static List<Dictionary<string, object>> ReadResults(IDataReader reader)
    {
        var results = new List<Dictionary<string, object>>();

        while (reader.Read())
        {
            var row = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.GetValue(i);

            results.Add(row);
        }

        return results;
    }

    private async Task<List<string>> ExecuteStringListQueryAsync(DbConnection connection, string query)
    {
        var results = new List<string>();

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(reader.GetString(0));
        }

        return results;
    }

    private async Task<List<string>> FetchNamesAsync(NpgsqlConnection connection, string schema, string type)
    {
        const string query = @"
            SELECT table_name
            FROM information_schema.tables
            WHERE table_schema = @schema AND table_type = @type";

        return await ExecuteParamQueryAsync(connection, query, ("@schema", schema), ("@type", type));
    }

    private async Task<List<string>> FetchFunctionsAsync(NpgsqlConnection connection, string schema)
    {
        const string query = @"
            SELECT routine_name
            FROM information_schema.routines
            WHERE routine_schema = @schema";

        return await ExecuteParamQueryAsync(connection, query, ("@schema", schema));
    }

    private async Task<List<string>> FetchSequencesAsync(NpgsqlConnection connection, string schema)
    {
        const string query = @"
            SELECT sequence_name
            FROM information_schema.sequences
            WHERE sequence_schema = @schema";

        return await ExecuteParamQueryAsync(connection, query, ("@schema", schema));
    }

    private async Task<List<string>> ExecuteParamQueryAsync(NpgsqlConnection connection, string query, params (string Name, object Value)[] parameters)
    {
        var results = new List<string>();

        await using var command = new NpgsqlCommand(query, connection);
        foreach (var (name, value) in parameters)
        {
            command.Parameters.AddWithValue(name, value);
        }

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(reader.GetString(0));
        }

        return results;
    }
}

