using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStoresAPI.Entities
{
    [Table("customers", Schema = "sales")]
    public class Customer
    {
        [Column("customer_id")]
        public int Id { get; set; }

        [Column("first_name")]
        public required string FirstName { get; set; }

        [Column("last_name")]
        public required string LastName { get; set; }

        [Column("phone")]
        public required string Phone { set; get; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("street")]
        public required string Street { get; set; }

        [Column("city")]
        public required string City { get; set; }

        [Column("zip_code")]
        public required string ZipCode { get; set; }

    }
}
