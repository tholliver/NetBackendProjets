using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace ImageProcessingService.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.Development.json", optional: false)
                                .Build();

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
                            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly("ImageProcessingService"));

        return new RepositoryContext(builder.Options);
    }
}



