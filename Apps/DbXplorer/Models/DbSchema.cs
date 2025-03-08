namespace DbXplorer.Models;
public class DbSchema
{
    public string Name { get; set; } = string.Empty;
    public List<string> Tables { get; set; } = new();
    public List<string> Views { get; set; } = new();
    public List<string> Functions { get; set; } = new();
    public List<string> Sequences { get; set; } = new();
}
