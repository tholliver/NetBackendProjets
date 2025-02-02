using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Stock
{
    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }
}