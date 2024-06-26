namespace BikeStores.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("products", Schema = "production")]
public class Bike
{
    [Column("product_id")]
    public int Id { get; set; }

    [Column("product_name")]
    public required string Name { get; set; }

    [Column("model_year")]
    public required short ModelYear { get; set; }

    [Column("list_price", TypeName = "decimal(10, 2)")]
    public required decimal Price { get; set; }

    [Column("brand_id")]
    public required int BrandId { get; set; }

    [Column("category_id")]
    public required int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Brand Brand { get; set; } = null!;

}