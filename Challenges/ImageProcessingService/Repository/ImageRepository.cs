using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.Mapping;

namespace Repository;

public class ImageRepository(RepositoryContext repository) : 
        RepositoryBase<Image>(repository), IImageRepository
{

    public async Task<IEnumerable<Image>> GetAllImages(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Image?> GetImageById(int id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(id), trackChanges)
                                                    .SingleOrDefaultAsync();
    }

    public async Task<Image> SaveImage(Image image)
    {
        Create(image);
        await RepositoryContext.SaveChangesAsync();
        return image;
    }

    public void DeleteImage(Image image)
    {
        Delete(image);
    }
}
