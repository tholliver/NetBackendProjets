using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Order
{
    [Column("order_id")]
    public int Id { get; set; }
}