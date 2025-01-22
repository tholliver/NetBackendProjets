using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Contracts;

namespace ImageProcessingService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(opts =>
        {
            opts.AddPolicy("CorsPolicy", builder =>
                                    builder.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddJwtConfiguration(this IServiceCollection services,
                                            IConfiguration configuration) =>
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetSection("Secret").Value;

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.ValidIssuer,
                ValidAudience = jwtConfiguration.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey))
            };
        });
    }

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
}