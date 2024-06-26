using System.ComponentModel.DataAnnotations.Schema;
namespace BikeStoresAPI.Entities
{
    [Table("staffs", Schema = "sales")]
    public class Staff
    {
        [Column("staff_id")]
        public int Id { get; set; }

        [Column("first_name")]
        public required string FirstName { get; set; }

        [Column("last_name")]
        public required string LastName { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("phone")]
        public required string Phone { set; get; }

        [Column("active")]
        public required byte Active { get; set; }

        [Column("store_id")]
        public required int StoreId { set; get; }

        [Column("manager_id")]
        public required int ManagerId { get; set; }

        public required Staff Manager;

    }
}