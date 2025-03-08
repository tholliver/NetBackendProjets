using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Order
{
    [Column("order_id")]
    public int Id { get; set; }


    [Column("order_status")]
    public byte OrderStatus { get; set; }

    [Column("order_date")]
    public DateTime OrderDate { get; set; }

    [Column("shipped_date")]
    public DateTime ShippedDate { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}