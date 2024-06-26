using BikeStoresAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Endpoints
{
    public static class StoresEndpoints
    {
        public static RouteGroupBuilder MapStoresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("stores").WithParameterValidation();

            group.MapGet("/{id}", async (int id, BikeStoreContext _dbContext) =>
            {
                var storeFound = await _dbContext.Stores.Include(s => s.Stock).ThenInclude(b => b.Bike).FirstAsync(e => e.Id == id);
                return storeFound is null ? Results.NotFound() : Results.Ok(storeFound);
            });

            group.MapPost("/", async () => { });

            return group;
        }

    }
}