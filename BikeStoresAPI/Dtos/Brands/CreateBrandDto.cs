using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Dtos.Brands
{
    public record class CreateBrandDto
    (
        [Required]
        [StringLength(20)]
        string Name
    );
}
