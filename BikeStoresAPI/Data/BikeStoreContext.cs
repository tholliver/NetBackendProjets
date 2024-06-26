using BikeStoresAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Data
{

    public class BikeStoreContext(DbContextOptions<BikeStoreContext> options)
     : DbContext(options)
    {
        public DbSet<Bike> Bikes => Set<Bike>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Brand> Brands => Set<Brand>();

        public DbSet<Store> Stores => Set<Store>();

        public DbSet<Stock> Stocks => Set<Stock>();

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

            modelBuilder.Entity<Bike>()
                            .HasMany(e => e.OrderItems)
                            .WithOne(e => e.Bike)
                            .HasPrincipalKey(e => e.Id)
                            .HasForeignKey(e => e.BikeId);


            // For STOCKS
            modelBuilder.Entity<Stock>().HasKey(st => new { st.StoreId, st.BikeId });

            modelBuilder.Entity<Stock>().HasOne(p => p.Bike)
                                        .WithOne(b => b.Stock)
                                        .HasForeignKey<Stock>(a => a.BikeId);

            modelBuilder.Entity<Stock>().HasOne(p => p.Store)
                                        .WithOne(b => b.Stock)
                                        .HasForeignKey<Stock>(a => a.StoreId);

            // For ORDER -ITEMS
            modelBuilder.Entity<OrderItem>().HasKey(po => new { po.BikeId, po.OrderId });

            modelBuilder.Entity<OrderItem>().HasOne(p => p.Order).WithOne(p => p.OrderItem)
                                                        .HasForeignKey<OrderItem>(e => e.OrderId);

            modelBuilder.Entity<OrderItem>().HasOne(p => p.Bike).WithMany(p => p.OrderItems)
                                                        .HasForeignKey(e => e.BikeId);



            // modelBuilder.Entity<Stock>().HasOne(p => p.Bike).WithOne(b => b.Stock)
            //                                  .HasForeignKey<Stock>(a => a.BikeId);

            // modelBuilder.Entity<Category>().Navigation(b => b.Bikes).AutoInclude();
            // modelBuilder.Entity<Brand>().Navigation(b => b.Bikes);


        }

    }
}