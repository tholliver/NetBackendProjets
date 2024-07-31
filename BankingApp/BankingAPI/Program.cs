using System.Text.Json.Serialization;
using BankingAPI.Data;
using BankingAPI.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_AllowSpecificOrigins";

string[] allowedOriging = { "http://localhost:3000" };
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database setup [POSTGRES]
// builder.AddNpgsqlDbContext<BankingContext>("DefaultConnection");

builder.Services.AddDbContext<BankingContext>(opts
  => opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ignore large json generation -> Cycles
builder.Services.Configure<Microsoft.AspNetCore.Http.Json
    .JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins(allowedOriging)
                         .AllowAnyMethod()
                         .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
app.UseCors(AllowSpecificOrigins);
app.MapCustomerEndpoint().WithTags("Customers").WithOpenApi();
app.MapAccountEndpoint().WithTags("Accounts").WithOpenApi();
app.MapTransactionEndpoint().WithTags("Transactions").WithOpenApi();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapGet("/echo",
//         context => context.Response.WriteAsync("echo"))
//         .RequireCors(MyAllowSpecificOrigins);

//     endpoints.MapControllers()
//              .RequireCors(MyAllowSpecificOrigins);

//     endpoints.MapGet("/echo2",
//         context => context.Response.WriteAsync("echo2"));

//     endpoints.MapRazorPages();
// });

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
