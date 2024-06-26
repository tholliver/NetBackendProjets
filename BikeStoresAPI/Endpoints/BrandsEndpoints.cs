using BikeStoresAPI.Data;
using BikeStoresAPI.Dtos.Brands;
using BikeStoresAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Endpoints
{
    public static class BrandsEndpoints
    {
        const string GetBrandsEndpoint = "GetBrand";

        public static RouteGroupBuilder MapBrandsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("brands").WithParameterValidation();

            // Get all brands
            group.MapGet("/", async (BikeStoreContext _dbcontext) =>
            {
                var brands = await _dbcontext.Brands.ToListAsync();
                return Results.Ok(brands);
            });

            // Get one brand by id
            group.MapGet("/{id}", async (int id, BikeStoreContext _dbcontext) =>
            {
                var brandFound = await _dbcontext.Brands.Include(e => e.Bikes)
               .FirstAsync(b => b.Id == id);

                return brandFound is null ? Results.NotFound() : Results.Ok(brandFound);
            })
            .WithName(GetBrandsEndpoint);

            // Post brand 
            group.MapPost("/", async (CreateBrandDto newBrand, BikeStoreContext _dbcontext) =>
            {
                var brand = new Brand()
                {
                    Name = newBrand.Name
                };
                var brandCreated = await _dbcontext.Brands.AddAsync(brand);
                await _dbcontext.SaveChangesAsync();

                return Results.CreatedAtRoute(GetBrandsEndpoint, new { id = brand.Id }, brand);
            });

            group.MapPut("/{id}", async (int id, UpdateBrandDto brand, BikeStoreContext _dbcontext) =>
            {
                var updatedToBrand = await _dbcontext.Brands.FindAsync(id);

                if (updatedToBrand == null)
                {
                    // Maybe create the Bike instead of this response
                    // With CreateOrUpdate()
                    return Results.NotFound();
                }

                await _dbcontext.Brands.Where(b => b.Id == updatedToBrand.Id)
                                    .ExecuteUpdateAsync(setter => setter.SetProperty(b => b.Name, brand.Name));
                // return Results.CreatedAtRoute(GetBikesEndpoint, new { id = index }, Repo.Bikes[index]);
                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, BikeStoreContext _dbcontext) =>
            {
                var brandToDelete = await _dbcontext.Brands.FindAsync(id);
                if (brandToDelete == null)
                {
                    return Results.NotFound();
                }

                _dbcontext.Brands.Remove(brandToDelete);
                await _dbcontext.SaveChangesAsync();

                return Results.NoContent();
            });

            return group;
        }
    }

}