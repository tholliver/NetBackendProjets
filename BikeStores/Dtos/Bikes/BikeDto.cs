namespace BikeStores.Dtos.Bikes;

public record class BikeDto
(
    int Id,
    string Name,
    short ModelYear,
    decimal Price,
    int BrandId,
    int CategoryId
);