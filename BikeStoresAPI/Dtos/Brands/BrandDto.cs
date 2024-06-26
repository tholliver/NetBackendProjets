using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Dtos.Brands
{
    public record class BrandDto
    (
        int Id,
        [Required]
    [StringLength(20)]
    string Name
    );

}
