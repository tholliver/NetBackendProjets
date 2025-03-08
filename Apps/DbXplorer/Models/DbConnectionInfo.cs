namespace DbXplorer.Models;

public class DbConnectionInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string DbType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
