using System.ComponentModel.DataAnnotations.Schema;

namespace LinQMax.Models;

public class Store
{
    [Column("store_id")]
    public int Id { get; set; }
}