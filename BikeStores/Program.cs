using System.Text.Json.Serialization;
using BikeStores.Data;
using BikeStores.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BikeStores");
builder.Services.AddSqlServer<BikeStoreContext>(connString);

builder.Services.AddScoped<BikeStoreContext>();
builder.Services
.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>
(opts => opts.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapBikesEndpoints();
app.MapBrandsEndpoints();

app.MapGet("/", () => "Hello from my firts API in .NET");

//Add controllers 

app.Run();
