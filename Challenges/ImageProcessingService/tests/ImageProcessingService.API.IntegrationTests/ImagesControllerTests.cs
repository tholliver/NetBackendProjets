using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Entities.Models;
using FluentAssertions;
using ImageProcessingService.API.IntegrationTests.ImageTestData;
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

    [Theory]
    [MemberData(nameof(ImageTestCases.ImageFilesTestData), MemberType = typeof(ImageTestCases))]
    public async Task POST_Create_Image_With_External_File(string imageFilePath,
                                                           string userId,
                                                           HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var app = new ImageProcessingServiceWebAppFactory();
        var client = app.CreateClient();

        // Ensure the file exists before proceeding
        Assert.True(File.Exists(imageFilePath), $"Test file not found at {imageFilePath}");

        await using var fileContent = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
        var formData = new MultipartFormDataContent
        {
            {
                new StreamContent(fileContent)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue($"image/{Path.GetExtension(imageFilePath)}") }
                },
                "file", Path.GetFileName(imageFilePath)
            }
        };

        // Act
        var response = await client.PostAsync($"api/images/{userId}", formData);
        _outputHelper.WriteLine($"{response.StatusCode} - {response.ReasonPhrase}");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(expectedStatusCode);
        if (expectedStatusCode == HttpStatusCode.Created)
        {
            var createdImage = await response.Content.ReadFromJsonAsync<Image>();
            createdImage.Should().NotBeNull();
            Assert.NotNull(createdImage?.Name);
            Assert.NotNull(createdImage.Path);
        }
    }

    [Theory]
    [MemberData(nameof(ImageTestCases.NotValidImageFilesTestData), MemberType = typeof(ImageTestCases))]
    public async Task POST_Invalid_ImageFomat_Should_Not_Be_Uploaded(string testFilePath, string userId)
    {
        // Arrange 
        var app = new ImageProcessingServiceWebAppFactory();
        var client = app.CreateClient();

        // Act
        // Ensure the file exists before proceeding
        Assert.True(File.Exists(testFilePath), $"Test file not found at {testFilePath}");

        await using var fileContent = new FileStream(testFilePath, FileMode.Open, FileAccess.Read);
        var formData = new MultipartFormDataContent
        {
            {
                new StreamContent(fileContent)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("image/png") }
                },
                "file", Path.GetFileName(testFilePath)
            }
        };

        var response = await client.PostAsync($"api/images/{userId}", formData);
        _outputHelper.WriteLine($"{response.StatusCode} - {response.ReasonPhrase}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Theory]
    [MemberData(nameof(ImageTestCases.ImageIdsTestData), MemberType = typeof(ImageTestCases))]
    public async Task GET_Retrieve_ImageById(int imageId, HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var app = new ImageProcessingServiceWebAppFactory();
        var client = app.CreateClient();

        // Act
        var response = await client.GetAsync($"api/images/{imageId}");

        // Assert
        response.StatusCode.Should().Be(expectedStatusCode);
        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var imageMatch = await response.Content.ReadFromJsonAsync<Image>();
            imageMatch.Should().NotBeNull();
            imageMatch?.Id.Should().Be(imageId);
        }
    }


    // AddImageRequest_AddsImage
}