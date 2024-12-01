using System.Text.Json.Serialization;
using BankingAPI.Data;
using BankingAPI.Endpoints;
using BankingAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_AllowSpecificOrigins";

string[] allowedOriging = ["http://localhost:3000"];
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

// Authentication
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<Customer>()
            .AddEntityFrameworkStores<BankingContext>()
            .AddApiEndpoints();


var app = builder.Build();

// Routes for Auth
// Since customized Customer Entity, cant use {app.MapIdentityApi<Customer>();} 

//.RequireAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);
// app.MapCustomerEndpoint().WithTags("Customers").WithOpenApi();
app.MapAccountEndpoint().WithTags("Accounts").WithOpenApi();
app.MapTransactionEndpoint().WithTags("Transactions").WithOpenApi();

app.MapIdentityCustomerApi().WithTags("Customers").WithOpenApi();

app.Run();