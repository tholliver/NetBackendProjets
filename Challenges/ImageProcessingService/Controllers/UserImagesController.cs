using System.ComponentModel.DataAnnotations;
using System.Net;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Controllers;

[Route("api/user-images"), Authorize]
[ApiController]
public class UserImagesController(IServiceManager service) : ControllerBase
{
    private const long _maxFileSize = 5 * 1024 * 1024; // 5MB
    private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];

    private readonly IServiceManager _service = service;

    // [HttpGet("{userId:guid}")]
    // public async Task<IActionResult> GetAllImagesByUserId([FromRoute, Required] string userId)
    // {
    //     var userImages = await _service.ImageService.GetAllImagesByUserId(userId, false);
    //     return Ok(userImages);
    // }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetAllImagesByUserName([FromRoute, Required] string userName)
    {
        var userImages = await _service.ImageService.GetAllImagesByUserName(userName, false);
        return Ok(userImages);
    }

    [HttpPost("{userName}")]
    public async Task<IActionResult> UploadImage([FromRoute] string userName, IFormFile file)
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

            var userDetails = await _service.UserService.GetUserByUserName(userName, false);

            if (userDetails is null)
            {
                return BadRequest(new { message = "User not found" });
            }

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
                Path = uploadPath.Replace("\\", "/"),
                UserId = userDetails.Id
            };

            // Save the new image to database 
            var imageSaved = await _service.ImageService.SaveImage(imageEntity);
            Console.WriteLine($"{imageSaved.Name} - {imageSaved.Path}");

            // Save image and relate to user
            // return CreatedAtAction(
            //     nameof(GetImageById),
            //     new { id = imageSaved.Id },
            //     imageSaved
            // );
            return Created($"/api/images/{imageSaved.Id}", imageSaved);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                               new { message = $"An error occurred while uploading the file {e.Message}" });
        }
    }
}
