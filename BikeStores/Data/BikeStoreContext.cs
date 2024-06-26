using BikeStores.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStores.Data;

public class BikeStoreContext(DbContextOptions<BikeStoreContext> options)
 : DbContext(options)
{
    public DbSet<Bike> Bikes => Set<Bike>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Brand> Brands => Set<Brand>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Only will define the 
        modelBuilder.Entity<Category>()
                        .HasMany(e => e.Bikes)
                        .WithOne(e => e.Category)
                        .HasPrincipalKey(e => e.Id)
                        .HasForeignKey(e => e.CategoryId);

        modelBuilder.Entity<Brand>()
                        .HasMany(e => e.Bikes)
                        .WithOne(e => e.Brand)
                        .HasPrincipalKey(e => e.Id)
                        .HasForeignKey(e => e.BrandId);

        // For Bikes 
        // modelBuilder.Entity<Bike>()
        // .HasOne(e => e.Brand)
        // .WithMany(e => e.Bikes)
        // .HasForeignKey(e => e.BrandId);

        // modelBuilder.Entity<Bike>()
        // .HasOne(e => e.Category)
        // .WithMany(e => e.Bikes)
        // .HasForeignKey(e => e.CategoryId);

        modelBuilder.Entity<Category>().Navigation(b => b.Bikes).AutoInclude();
        modelBuilder.Entity<Brand>().Navigation(b => b.Bikes);

        // modelBuilder.Entity<Brand>()
        // .Ignore(b => b.Bikes);

        // modelBuilder.Entity<Category>()
        //     .Ignore(c => c.Bikes);
    }

}