using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Staff
{
    [Key]
    [Column("staff_id")]
    public int Id { get; set; }

    [Required]
    [Column("first_name")]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Column("last_name")]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Column("email")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Column("phone")]
    [MaxLength(25)]
    public string? Phone { get; set; }  // Nullable column

    [Required]
    [Column("active")]
    public byte Active { get; set; }  // Using byte for tinyint

    [Required]
    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("manager_id")]
    public int? ManagerId { get; set; }  // Nullable column
}