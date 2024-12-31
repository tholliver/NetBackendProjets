using Entities.Models;
using Shared.Mapping;

namespace Service.Contracts;

public interface IImageRepository
{
    Task<IEnumerable<Image>> GetAllImages(bool trackChanges);
    Task<IEnumerable<Image>> GetAllImagesByUserId(string  userId,bool trackChanges);
    Task<Image> GetImageById(int id, bool trackChanges);
    Task<Image> SaveImage(Image image);
    void DeleteImage(Image image);
}
