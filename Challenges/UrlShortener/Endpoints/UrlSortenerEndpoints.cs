using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;

namespace UrlShortener.Endpoints;

public static class UrlSortenerEndpoints
{
    public static void MapUrlSortenerEndpoints(this WebApplication app)
    {
        app.MapGet("/uri", GetShortUrl);
        app.MapGet("/uri/{id:int}", GetShortUrlById);
        app.MapPost("/uri", ShortUrl);
        app.MapDelete("/uri{id:int}", DeleteShortUrl);
    }

    public static async Task<IResult> GetShortUrlById(int id,
                                                       UrlShortenerDbContext dbContext)
    {
        var uriResult = await dbContext.UrlShorteds.FindAsync(id);
        return uriResult != null ? TypedResults.Ok(uriResult) : TypedResults.NotFound();
    }

    public async static Task<IResult> GetShortUrl(UrlShortenerDbContext dbContext, HttpContext httpContext)
    {
        var clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
        Console.WriteLine(clientIp);
        var allUrls = await dbContext.UrlShorteds.ToListAsync();
        return TypedResults.Ok(allUrls);
    }

    public async static Task<IResult> ShortUrl(string originalURI,
                                               UrlShortenerDbContext dbContext)
    {
        var key = GenerateKey();
        var URLShort = new UrlShorted() { OriginalUrl = originalURI, ShortUrl = key };
        var uriAdded = await dbContext.UrlShorteds.AddAsync(URLShort);
        await dbContext.SaveChangesAsync();
        return TypedResults.Created($"/{URLShort.Id}");
    }

    public async static Task<IResult> DeleteShortUrl(int id, UrlShortenerDbContext dbContext)
    {
        var url = await dbContext.UrlShorteds.FindAsync(id);
        if (url == null)
        {
            return TypedResults.NotFound(new { Message = "URL not found" });
        }

        dbContext.UrlShorteds.Remove(url);
        await dbContext.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    // Generates the UNIQUE key for the short URI
    private static string GenerateKey()
    {
        return Guid.NewGuid().ToString().Substring(0, 6);
    }
}