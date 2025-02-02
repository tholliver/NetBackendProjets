using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

[Table("categories")]
public class Category
{
    [Column("category_id")]
    public int Id { get; set; }
    [Column("category_name")]
    public required string Name { get; set; }

    public List<Product>? Products { get; set; }
}