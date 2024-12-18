namespace Entities.Models;

public class Image
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string UserId { get; set; }
    public User User { get; set; } = null!;
}