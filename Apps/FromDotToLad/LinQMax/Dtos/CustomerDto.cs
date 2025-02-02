namespace LinQMax.Dtos;

public record CustomerDto
{
    public int? Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? City { get; init; }
}
