using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Product
{
    [Column("product_id")]
    public int Id { get; set; }

    [Required]
    [Column("product_name")]
    [MaxLength(255)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [Column("brand_id")]
    public int BrandId { get; set; }

    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Required]
    [Column("model_year")]
    public short ModelYear { get; set; }

    [Required]
    [Column("list_price", TypeName = "decimal(10,2)")]
    public decimal ListPrice { get; set; }

    public required Brand Brand { get; set; }

    public List<OrderItem>? OrderItems { get; set; }
}