using System;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit.Abstractions;

namespace ImageProcessingService.API.IntegrationTests;

public class ImagesControllerTests(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _outputHelper = outputHelper;

    [Fact]
    public async Task GET_Retrieves_All_Images()
    {
        // Arrange
        var app = new ImageProcessingServiceWebAppFactory();
        var client = app.CreateClient();

        // Act
        var response = await client.GetAsync("api/images");

        // Assert
        response.EnsureSuccessStatusCode();
        var match = await response.Content.ReadFromJsonAsync<List<Image>>();
        match?[0].Id.Should().BePositive();
        match?.Count.Should().Be(1);
    }

    [Fact]
    public async Task POST_Create_Image()
    {
        var app = new ImageProcessingServiceWebAppFactory();
        var client = app.CreateClient();
        
        // User id
        const string userId = "8bfc8b29-a139-4883-a0ac-ae04095951ce";
        var fileContent = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
        var testFileName = "C:\\Users\\DaMagic\\Pictures\\Screenshots\\Screenshot 2024-11-22 205058.png";
        var formFile = new FormFile(fileContent, 0, fileContent.Length, "file", testFileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg",
        };
        var formData = new MultipartFormDataContent
        {
            { new StreamContent(fileContent), "file", testFileName }
        };
        
        // Act
        var response = await client.PostAsJsonAsync("api/images", formData);
        
        //Assert 
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var createdImage = await response.Content.ReadFromJsonAsync<Image>();
        createdImage.Should().NotBeNull();
        // createdImage?.Id.Should().BePositive();
        // createdImage?.Name.Should().Be(newImage.Name);
        // createdImage?.Path.Should().Be(newImage.Path);
        // createdImage?.UserId.Should().Be(userId);
        
        // return Ok(new
        // {
        //     message = "File uploaded successfully",
        //     fileName = fileName,
        //     filePath = uploadPath
        // });
    }

    [Fact]
    public async Task GET_Retrieve_ImageById()
    {
        // Arrange
        var app = new ImageProcessingServiceWebAppFactory();
        var client = app.CreateClient();

        // Define id for test
        const int imageId = 1;

        // Act
        var response = await client.GetAsync($"api/images/{imageId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var imageMatch = await response.Content.ReadFromJsonAsync<Image>();
        imageMatch.Should().NotBeNull();
        imageMatch?.Id.Should().Be(imageId);
    }

    // AddImageRequest_AddsImage

}
