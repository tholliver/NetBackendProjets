using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStoresAPI.Entities
{
    [Table("stocks", Schema = "production")]
    public class Stock
    {
        [Column("store_id")]
        public int StoreId { get; set; }

        [Column("product_id")]
        public int BikeId { set; get; }

        [Column("quantity")]
        public int Quantity { set; get; }

        public Bike Bike { get; set; } = null!;

        public Store Store { get; set; } = null!;
    }
}