using System.ComponentModel.DataAnnotations;

namespace BikeStoresAPI.Dtos.Bikes;

public record class UpdateBikeDto(
    [Required][StringLength(20)] string Name,
    [Range(1000, 9999)] short ModelYear,
    decimal Price,
    [Required] int BrandId,
    [Required] int CategoryId
);