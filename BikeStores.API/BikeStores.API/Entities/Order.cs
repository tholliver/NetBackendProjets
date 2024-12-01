using System.ComponentModel.DataAnnotations.Schema;
namespace BikeStoresAPI.Entities
{
    [Table("orders", Schema = "sales")]
    public class Order
    {
        [Column("order_id")]
        public int Id { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("order_status")]
        public required byte OrderStatus { get; set; }

        [Column("order_date")]
        public required string OrderDate { get; set; }

        [Column("required_date")]
        public required DateOnly RequiredDate { set; get; }

        [Column("shipped_date")]
        public required DateOnly ShippedDate { get; set; }

        [Column("store_id")]
        public required int StoreId { set; get; }

        [Column("staff_id")]
        public required int StaffId { get; set; }

        public Staff Staff { get; set; } = null!;
        public Store Store { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

        public OrderItem OrderItem { get; set; } = null!;


    }
}