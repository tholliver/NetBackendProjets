using System.ComponentModel.DataAnnotations;
using System.Net;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Controllers;

[Route("api/images"), Authorize]
[ApiController]
public class ImagesController(IServiceManager service) : ControllerBase
{
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
}
