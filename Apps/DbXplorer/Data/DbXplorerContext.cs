using DbXplorer.Models;
using Microsoft.EntityFrameworkCore;

public class DbXplorerContext(IConfiguration configuration) : DbContext
{
    protected readonly IConfiguration Configuration = configuration;
    public DbSet<DbConnectionInfo> DbCredentials { get; set; }
    public DbSet<DbConnectionInfo> DBconnectionInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbConnectionInfo>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp");

        modelBuilder.Entity<DbConnectionInfo>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("current_timestamp");
    }
}