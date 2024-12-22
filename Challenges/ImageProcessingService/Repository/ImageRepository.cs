using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace Repository;

public class ImageRepository(RepositoryContext repository) : RepositoryBase<Image>(repository), IImageRepository
{

    public async Task<IEnumerable<Image>> GetAllImages(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Image> GetImageById(int id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(id), trackChanges)
                                                    .SingleOrDefaultAsync();
    }

    public void SaveImage(Guid userId, Image image)
    {
        Create(image);
    }

    public void DeleteImage(Image image)
    {
        Delete(image);
    }
}
