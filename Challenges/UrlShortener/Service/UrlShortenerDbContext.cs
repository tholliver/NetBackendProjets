using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;

public class UrlShortenerDbContext(
    DbContextOptions<UrlShortenerDbContext> options) : DbContext(options)
{
    public DbSet<UrlShorted> UrlShorteds { get; set; }
}