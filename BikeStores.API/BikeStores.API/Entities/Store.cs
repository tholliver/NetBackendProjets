using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStoresAPI.Entities
{
    [Table("stores", Schema = "sales")]
    public class Store
    {
        [Column("store_id")]
        public int Id { get; set; }

        [Column("store_name")]
        public required string Name { get; set; }

        [Column("phone")]
        public required string Phone { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("street")]
        public required string Street { get; set; }

        [Column("city")]
        public required string City { get; set; }

        [Column("state")]
        public required string State { get; set; }

        [Column("zip_code")]
        public required string ZipCode { get; set; }

        public Stock Stock { get; set; } = null!;
    }
}