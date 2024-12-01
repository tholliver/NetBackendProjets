using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStoresAPI.Entities
{
    [Table("order_items", Schema = "sales")]

    public class OrderItem
    {
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("item_id")]
        public int ItemId { set; get; }

        [Column("product_id")]
        public int BikeId { get; set; }

        [Column("list_price", TypeName = "decimal(10, 2)")]
        public required decimal Price { get; set; }

        [Column("discount", TypeName = "decimal(4, 2)")]
        public required decimal Discount { get; set; } = default;

        public Bike Bike { set; get; } = null!;
        public Order Order { set; get; } = null!;

    }
}