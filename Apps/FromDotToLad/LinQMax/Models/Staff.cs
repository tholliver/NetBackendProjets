using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Staff
{
    [Column("staff_id")]
    public int Id { get; set; }
}