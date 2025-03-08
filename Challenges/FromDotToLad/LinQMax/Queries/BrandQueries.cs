using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinQMax.Contract;
using LinQMax.Models;
using MethodTimer;
using Microsoft.EntityFrameworkCore;

namespace LinQMax.Queries;

public class BrandQueries
{
    private readonly IDatabaseService _databaseService;

    public BrandQueries(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public List<Brand> GetBrands()
    {
        using var context = _databaseService.GetContext();
        return context.Brands
            .ToList();
    }

    public List<Brand> GetBrandsWithOrder()
    {
        using var context = _databaseService.GetContext();
        return context.Brands
            .OrderBy(b => b.Name)
            .ToList();
    }

    public List<Brand> GetBrandsWithOrderAndTake()
    {
        using var context = _databaseService.GetContext();
        return context.Brands
            .OrderBy(b => b.Name)
            .Take(5)
            .ToList();
    }

    public List<BrandDto> GetBrandsWithOrderAndTakeSelect()
    {
        using var context = _databaseService.GetContext();
        return context.Brands
            .OrderBy(b => b.Name)
            .Select(b => new BrandDto { Name = b.Name, Id = b.Id })
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Get the total number of products for each brand
    /// </summary>
    /// <returns>
    /// A list of <see cref="BrandProductCount"/> objects
    /// </returns>

    // public async Task<List<BrandProductCount>> GetTotalProductsByBrand()
    // {
    //     using var context = _databaseService.GetContext();

    //     return await context.Brands
    //         .Select(b => new BrandProductCount { Name = b.Name, TotalProducts = b.Products.Count() })
    //         .OrderByDescending(b => b.TotalProducts)
    //         .ToListAsync();
    // }

    public async Task<List<BrandProductCount>> GetTotalProductsByBrand()
    {
        using var context = _databaseService.GetContext();

        return await context.Brands
                    .Select(b => new BrandProductCount
                    {
                        Name = b.Name,
                        TotalProducts = context.Products.Count(p => p.BrandId == b.Id) // Efficient COUNT query
                    })
                    .OrderByDescending(b => b.TotalProducts)
                    .ToListAsync();
    }

    [Time]
    public async Task<List<BrandProductCount>> GetTotalProductsByBrandV2()
    {
        using var context = _databaseService.GetContext();

        return await context.Products
                    .GroupBy(p => p.BrandId)
                    .Join(context.Brands,
                        g => g.Key,
                        b => b.Id,
                        (g, b) => new BrandProductCount
                        {
                            Name = b.Name,
                            TotalProducts = g.Count()
                        })
                    .OrderByDescending(p => p.TotalProducts)
                    .ToListAsync();
    }
}

public record BrandProductCount
{
    public string? Name { get; init; }
    public int TotalProducts { get; init; }
}

public record BrandDto
{
    public int Id { get; init; }

    public required string Name { get; init; }
}