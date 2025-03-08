using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace Repository;

public class ImageRepository(RepositoryContext repository) :
        RepositoryBase<Image>(repository), IImageRepository
{
    public async Task<IEnumerable<Image>> GetAllImages(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<IEnumerable<Image>> GetAllImagesByUserId(string userId, bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(c => c.Name).Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Image>> GetAllImagesByUserName(string userName, bool trackChanges)
    {
        var result = await (from user in RepositoryContext.Users
                            join image in RepositoryContext.Images

                             on user.Id equals image.UserId
                            where user.UserName == userName
                            select new Image
                            {
                                Id = image.Id,
                                Name = image.Name,
                                Path = image.Path,
                                UserId = user.Id
                            }).ToListAsync();

        return result;
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
