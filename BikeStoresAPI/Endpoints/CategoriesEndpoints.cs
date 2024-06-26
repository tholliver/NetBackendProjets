using Microsoft.EntityFrameworkCore;
using BikeStoresAPI.Data;
using BikeStoresAPI.Dtos.Brands;
using BikeStoresAPI.Entities;

namespace BikeStoresAPI.Endpoints
{
    public static class CategoriesEndpoints
    {
        const string GetCategoriesEndpoints = "GetCategory";

        public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("categories").WithParameterValidation();

            group.MapGet("/", async (BikeStoreContext _dbContext) =>
            {
                var categories = await _dbContext.Categories.ToListAsync();
                return Results.Ok(categories);
            });

            group.MapGet("/{id}", async (int id, BikeStoreContext _dbContext) =>
            {
                var categoryFound = await _dbContext.Categories.FirstAsync(c => c.Id == id);

                return categoryFound is null ? Results.NotFound() : Results.Ok(categoryFound);
            });

            group.MapPost("/", async (CreateBrandDto category, BikeStoreContext _dbcontext) =>
            {
                var newCategory = new Category()
                {
                    Name = category.Name
                };
                var brandCreated = await _dbcontext.Categories.AddAsync(newCategory);
                await _dbcontext.SaveChangesAsync();

                return Results.CreatedAtRoute(GetCategoriesEndpoints, new { id = newCategory.Id }, newCategory);
            });

            return group;
        }
    }
}