using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Store
{
    [Column("store_id")]
    public int Id { get; set; }

    [Required]
    [Column("store_name")]
    [MaxLength(255)]
    public string StoreName { get; set; } = null!;

    [Column("phone")]
    [MaxLength(25)]
    public string? Phone { get; set; }

    [Column("email")]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Column("street")]
    [MaxLength(255)]
    public string? Street { get; set; }

    [Column("city")]
    [MaxLength(255)]
    public string? City { get; set; }

    [Column("state")]
    [MaxLength(10)]
    public string? State { get; set; }

    [Column("zip_code")]
    [MaxLength(5)]
    public string? ZipCode { get; set; }

    public List<Stock> Stocks { get; set; }
}