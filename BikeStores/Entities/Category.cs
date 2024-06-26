namespace BikeStores.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("categories", Schema = "production")]
public class Category
{
    [Column("category_id")]
    public int Id { get; set; }

    [Column("category_name")]
    public required string Name { get; set; }

    public ICollection<Bike> Bikes { get; } = new List<Bike>();

}