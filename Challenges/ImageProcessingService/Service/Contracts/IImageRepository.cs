using Entities.Models;

namespace Service.Contracts;

public interface IImageRepository
{
    Task<IEnumerable<Image>> GetAllImages(bool trackChanges);
    Task<Image> GetImageById(int id, bool trackChanges);
    void SaveImage(Guid userId, Image image);
    void DeleteImage(Image image);
}
