using System.ComponentModel.DataAnnotations;

namespace BikeStores.Dtos.Brands
{
    public record class CreateBrandDto
    (
        [Required]
        [StringLength(20)]
        string Name
    );
}
