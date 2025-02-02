using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LinQMax.Models;

[Table("stocks"), PrimaryKey(nameof(StoreId), nameof(ProductId))]
public class Stock
{
    [Key, Column("store_id", Order = 0)]
    public int StoreId { get; set; }

    [Key, Column("product_id", Order = 1)]
    public int ProductId { get; set; }
}