using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repository;

namespace ImageProcessingService.API.IntegrationTests;

internal class ImageProcessingServiceWebAppFactory :
    WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<RepositoryContext>));

            var connString = GetConnectionString();
            services.AddNpgsql<RepositoryContext>(connString);

            services.AddAuthentication()
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "TestScheme", options => { }
                    );

            var dbContext = CreateRepositoryContext(services);
            // dbContext.Database.EnsureDeleted();
            // dbContext.Database.Migrate();
        });

        // base.ConfigureWebHost(builder);
    }

    private static string? GetConnectionString()
    {
        // Getting the secrets from dotnet ENV
        // var configuration = new ConfigurationBuilder()
        // .AddUserSecrets<ImageProcessingServiceWebAppFactory>()
        // .Build();

        // Getting the secrets from CONFIG FILE
        var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.Development.Testing.json", optional: false)
                        .Build();

        var connString = configuration.GetConnectionString("DefaultConnection");
        return connString;
    }

    private static RepositoryContext CreateRepositoryContext(IServiceCollection services)
    {
        // var builder = new DbContextOptionsBuilder<RepositoryContext>()
        //                     .UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
        //                     b => b.MigrationsAssembly("ImageProcessingService"));

        var serviceProvider = services.BuildServiceProvider();

        var scope = serviceProvider.CreateScope();
        var repoContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();

        return repoContext;
    }
}
