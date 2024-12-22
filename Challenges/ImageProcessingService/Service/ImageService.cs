using System;
using Entities.Models;
using Service.Contracts;

namespace Service;

internal sealed class ImageService(IRepositoryManager repository) : IImageService
{
    private readonly IRepositoryManager _repository = repository;

    public void DeleteImage(Image image)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Image>> GetAllImages(bool trackChanges)
    {
        return await _repository.Images.GetAllImages(trackChanges: false);
    }

    public async Task<Image> GetImageById(int id, bool trackChanges)
    {
        return await _repository.Images.GetImageById(id, trackChanges);
    }

    public void SaveImage(Guid userId, Image image)
    {
        throw new NotImplementedException();
    }
}
