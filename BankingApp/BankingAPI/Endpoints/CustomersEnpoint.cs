using BankingAPI.Data;
using BankingAPI.Dtos;
using BankingAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Endpoints;
static class CustomersEnpoint
{
    const string GetCustomerEndpoint = "GetCustomer";
    public static RouteGroupBuilder MapCustomerEndpoint(this WebApplication app)
    {

        RouteGroupBuilder group = app.MapGroup("customers"); //WithParameterValidation();

        group.MapGet("/", async (BankingContext _context) =>
        {
            var customers = await _context.Customers.ToListAsync();
            return Results.Ok(customers);
        });

        group.MapGet("/{id}", async (int id, BankingContext context) =>
          {
              var customerFound = await context.Customers
              .Where(b => b.CustomerId == id).FirstOrDefaultAsync();

              return customerFound is null ? Results.NotFound() : Results.Ok(customerFound);
          })
          .WithName(GetCustomerEndpoint);

        group.MapPost("/", async (CreateCustomerDto customer, BankingContext _context) =>
        {
            // Validations
            var newCustomer = new Customer()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Password = customer.Password,
                Accounts = customer.Accounts
            };
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

            return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = newCustomer.CustomerId }, newCustomer);
        });

        return group;
    }
}
