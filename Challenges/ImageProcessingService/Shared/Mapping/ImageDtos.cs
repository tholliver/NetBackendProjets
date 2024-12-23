using Entities.Models;

namespace Shared.Mapping;

public record ImageDtos(int Id, string Name, string Path, string UserId)
{
    public static ImageDtos FromEntity(Image image)
    {
        return new ImageDtos(
            image.Id,
            image.Name,
            image.Path,
            image.UserId
        );
    }

    public Image ToEntity()
    {
        return new Image
        {
            Id = this.Id,
            Name = this.Name,
            Path = this.Path,
            UserId = this.UserId
        };
    }
}