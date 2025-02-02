using LinQMax.Models;
using Microsoft.EntityFrameworkCore;

namespace LinQMax.Data;


public class LinQMaxContext : DbContext
{
  private readonly string _connectionString;

  public LinQMaxContext(string connectionString)
  {
    _connectionString = connectionString;
  }

  /// Production
  public DbSet<Brand> Brands { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Stock> Stocks { get; set; }

  /// Sales
  public DbSet<Customer> Customers { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<Staff> Staffs { get; set; }
  public DbSet<Store> Stores { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(_connectionString);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    /// Mapping table names
    var salesSchema = "sales";
    var productsSchema = "production";
    
    modelBuilder.Entity<Brand>().ToTable("brands", productsSchema);
    modelBuilder.Entity<Category>().ToTable("categories", productsSchema);
    modelBuilder.Entity<Product>().ToTable("products", productsSchema);
    modelBuilder.Entity<Stock>().ToTable("stocks", productsSchema);

    modelBuilder.Entity<Customer>().ToTable("customers", salesSchema);
    modelBuilder.Entity<OrderItem>().ToTable("order_items", salesSchema);
    modelBuilder.Entity<Order>().ToTable("orders", salesSchema);
    modelBuilder.Entity<Staff>().ToTable("staffs", salesSchema);
    modelBuilder.Entity<Store>().ToTable("stores", salesSchema);

    modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId });
    modelBuilder.Entity<Stock>().HasKey(s => new { s.StoreId, s.ProductId });
  }
}