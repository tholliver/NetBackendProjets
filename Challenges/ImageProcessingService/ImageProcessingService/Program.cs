using ImageProcessingService.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;

var builder = WebApplication.CreateBuilder(args);

{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(opts =>
    {
        opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        opts.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });

    builder.Services.ConfigureCors();

    builder.Services.AddAuthentication();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJWT(builder.Configuration);
    builder.Services.AddJwtConfiguration(builder.Configuration);

    builder.Services.AddAuthorization();
    builder.Services.AddControllers().AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);

    // Database service
    builder.Services.AddDbContext<RepositoryContext>(opts =>
        opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    // Services 
    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureServiceManager();
}

var app = builder.Build();

{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapControllers();

    app.Run();
}

public partial class Program { }