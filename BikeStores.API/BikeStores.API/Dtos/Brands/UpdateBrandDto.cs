using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Dtos.Brands
{
    public record class UpdateBrandDto
    (
        [Required]
        [StringLength(20)]
        string Name
    );
}