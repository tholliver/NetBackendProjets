namespace BikeStoresAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("brands", Schema = "production")]
public class Brand
{
    [Column("brand_id")]

    public int Id { get; set; }
    [Column("brand_name")]

    public required string Name { get; set; }

    public ICollection<Bike> Bikes { get; set; } = new List<Bike>();
}