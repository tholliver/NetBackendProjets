using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Customer
{
    [Column("customer_id")]
    public int Id { get; set; }

    [Column("first_name")]
    public required string FirstName { get; set; }

    [Column("last_name")]
    public required string LastName { get; set; }

    [Column("city")]
    public required string City { get; set; }
}