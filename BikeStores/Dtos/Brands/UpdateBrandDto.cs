using System.ComponentModel.DataAnnotations;

namespace BikeStores.Dtos.Brands
{
    public record class UpdateBrandDto
    (
        [Required]
        [StringLength(20)]
        string Name
    );
}