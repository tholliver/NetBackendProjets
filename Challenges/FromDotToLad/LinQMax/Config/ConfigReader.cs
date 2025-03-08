using System.Text.Json;

namespace LinQMax.Config;

public class ConfigReader
{
    private readonly string _configPath;
    private readonly JsonDocument _config;

    public ConfigReader(string configPath = "appsettings.Dev.json")
    {
        _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);
        var jsonString = File.ReadAllText(_configPath);
        _config = JsonDocument.Parse(jsonString);
    }
    public string GetConnectionString(string name)
    {
        return _config.RootElement
                    .GetProperty("ConnectionStrings")
                    .GetProperty(name)
                    .GetString() ?? throw new InvalidOperationException($"Connection string {name} not found");
    }
}
