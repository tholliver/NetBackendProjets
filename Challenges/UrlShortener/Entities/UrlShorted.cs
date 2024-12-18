namespace UrlShortener.Entities;
public class UrlShorted
{
    public int Id { get; set; }
    public string? ShortUrl { get; set; }
    public string? OriginalUrl { get; set; }
}