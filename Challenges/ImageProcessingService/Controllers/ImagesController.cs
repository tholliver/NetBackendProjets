using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Controllers;

[Route("api/images")]
[ApiController]
public class ImagesController(IServiceManager service) : ControllerBase
{
    private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

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

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file was uploaded" });

            if (file.Length > _maxFileSize)
                return BadRequest(new { message = "No file was uploaded" });

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return BadRequest(new { message = "Invalid file type. Only jpg, jpeg, png, and gif are allowed" });

            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine("uploads", fileName);

            // Ensure directory exists
            // Directory.CreateDirectory("uploads");

            // // Save file
            // using (var stream = new FileStream(uploadPath, FileMode.Create))
            // {
            //     await file.CopyToAsync(stream);
            // }

            return Ok(new
            {
                message = "File uploaded successfully",
                fileName = fileName,
                filePath = uploadPath
            });

            // return Ok();
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                               new { message = $"An error occurred while uploading the file {e.GetType}" });
        }
    }
}
