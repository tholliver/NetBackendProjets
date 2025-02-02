using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

[Table("brands")]
public class Brand
{
    [Column("brand_id")]
    public int Id { get; set; }

    [Column("brand_name")]
    [StringLength(255)]
    public required string Name { get; set; }

    public List<Product>? Products = [];
}