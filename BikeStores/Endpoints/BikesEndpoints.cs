namespace BikeStores.Endpoints;
using BikeStores.Data;
using BikeStores.Dtos;
using BikeStores.Dtos.Bikes;
using BikeStores.Entities;
using Microsoft.EntityFrameworkCore;

public static class BikesEndpoints
{
    const string GetBikesEndpoint = "GetBike";

    public static RouteGroupBuilder MapBikesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("bikes").WithParameterValidation();

        // Get Bikes
        // group.MapGet("/", () => Repo.Bikes);
        group.MapGet("/", async (BikeStoreContext context) =>
        {
            var bikes = await context.Bikes.ToListAsync();
            return Results.Ok(bikes);
        });


        group.MapGet("/{id}", async (int id, BikeStoreContext context) =>
        {
            var bikeDto = await context.Bikes
            .Include(b => b.Brand)
            .Where(b => b.Id == id).ToListAsync();


            Console.WriteLine("Here the obj: ", bikeDto.Take(2));

            return bikeDto is null ? Results.NotFound() : Results.Ok(bikeDto.Take(2));
        })
        .WithName(GetBikesEndpoint);

        group.MapPost("/", async (CreateBikeDto bike, BikeStoreContext _dbcontext) =>
        {
            var newBike = new Bike()
            {
                Name = bike.Name,
                ModelYear = bike.ModelYear,
                Price = bike.Price,
                BrandId = bike.BrandId,
                CategoryId = bike.CategoryId
            };

            await _dbcontext.Bikes.AddAsync(newBike);
            await _dbcontext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetBikesEndpoint, new { id = newBike.Id }, newBike);
        });

        group.MapPut("/{id}", async (int id, UpdateBikeDto bike, BikeStoreContext _dbcontext) =>
        {
            var bikeToUpdate = await _dbcontext.Bikes.FindAsync(id);

            if (bikeToUpdate == null)
            {
                // Maybe create the Bike instead of this response
                // With CreateOrUpdate()
                return Results.NotFound();
            }

            await _dbcontext.Bikes.Where(b => b.Id == id).ExecuteUpdateAsync(setter => setter
                                                                .SetProperty(b => b.ModelYear, bike.ModelYear)
                                                                .SetProperty(b => b.Name, bike.Name)
                                                                .SetProperty(b => b.ModelYear, bike.ModelYear)
                                                                .SetProperty(b => b.Price, bike.Price));

            // return Results.CreatedAtRoute(GetBikesEndpoint, new { id = index }, Repo.Bikes[index]);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            // Repo.Bikes.RemoveAt(id);
            var deletedBikes = Repo.Bikes.RemoveAll(bike => bike.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}