using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Product
{
    [Column("product_id")]
    public int Id { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}