using System.ComponentModel.DataAnnotations;

namespace BikeStores.Dtos.Bikes;
public record class CreateBikeDto
(
    [Required][StringLength(20)] string Name,
    [Range(1000, 9999)] short ModelYear,
    decimal Price,
    [Required] int BrandId,
    [Required] int CategoryId
);