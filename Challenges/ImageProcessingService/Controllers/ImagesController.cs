using System.ComponentModel.DataAnnotations;
using System.Net;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Controllers;

[Route("api/images")]
[ApiController]
public class ImagesController(IServiceManager service) : ControllerBase
{
    private const long _maxFileSize = 5 * 1024 * 1024; // 5MB
    private readonly string[] _allowedExtensions = [ ".jpg", ".jpeg", ".png", ".gif" ];

    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllImages()
    {
        var images = await _service.ImageService
                             .GetAllImages(trackChanges: false);
        return Ok(images);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetImageById([FromRoute, Required] int id)
    {
        var image = await _service.ImageService
                                  .GetImageById(id, trackChanges: false);
        return image != null ? Ok(image) : NotFound();
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> UploadImage([FromRoute]string userId, IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file was uploaded" });

            if (file.Length > _maxFileSize)
                return BadRequest(new { message = "File is too big" });

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return BadRequest(new { message = "Invalid file type. Only jpg, jpeg, png, and gif are allowed" });

            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine("uploads", fileName);

            // Ensure directory exists
            Directory.CreateDirectory("uploads");

            // Save file
            await using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // Save reference for user
            var imageEntity = new Image
            {
                Name = fileName,
                Path = uploadPath,
                UserId = userId
            };
            
            // Save the new image to database 
            var imageSaved = await _service.ImageService.SaveImage(imageEntity);
            Console.WriteLine($"{imageSaved.Name} - {imageSaved.Path}");
            
            // Save image and relate to user
            return CreatedAtAction(
                nameof(GetImageById),
                new { id = imageSaved.Id }, 
                imageSaved
            );
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                               new { message = $"An error occurred while uploading the file {e.Message}" });
        }
    }
}
