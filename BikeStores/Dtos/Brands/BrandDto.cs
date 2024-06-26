using System.ComponentModel.DataAnnotations;

namespace BikeStores.Dtos.Brands
{
    public record class BrandDto
    (
        int Id,
        [Required]
    [StringLength(20)]
    string Name
    );

}
