using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Entities.Models;
using FluentAssertions;
using ImageProcessingService.API.IntegrationTests.ImageTestData;
using ImageProcessingService.API.IntegrationTests.TestUtils;
using Xunit.Abstractions;

namespace ImageProcessingService.API.IntegrationTests;

public class ImagesControllerTests(ITestOutputHelper outputHelper, IPSWebAppFactory<Program> factory)
: IClassFixture<IPSWebAppFactory<Program>>
{
    private readonly IPSWebAppFactory<Program> _factory = factory;
    private readonly HttpClient _client = factory.CreateClient();
    private readonly ITestOutputHelper _outputHelper = outputHelper;

    [Fact]
    public async Task GET_Retrieves_All_Images()
    {
        // Arrange
        // Set data
        var authenticatedUser = await AuthUserTests.AuthenticateUser(_client, "johndoe", "StrongPassword123!");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticatedUser.AccessToken);

        // Act
        var response = await _client.GetAsync("api/images");
        _outputHelper.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var images = await response.Content.ReadFromJsonAsync<List<Image>>();

        images.Should().NotBeNull();
        images.Should().HaveCountGreaterThan(0);
        images.Should().AllSatisfy(img =>
        {
            img.Id.Should().BePositive();
            img.Path.Should().NotBeNullOrEmpty();
            img.Name.Should().NotBeNullOrEmpty();
            img.UserId.Should().NotBeNullOrEmpty();
        });
    }

    [Theory]
    [MemberData(nameof(ImageTestCases.ImageFilesTestData), MemberType = typeof(ImageTestCases))]
    public async Task POST_Create_Image_With_External_File(string testFilePath,
                                                           string userId,
                                                           HttpStatusCode expectedStatusCode)
    {
        // Arrange

        // Ensure the file exists before proceeding
        Assert.True(File.Exists(testFilePath), $"Test file not found at {testFilePath}");

        await using var fileContent = new FileStream(testFilePath, FileMode.Open, FileAccess.Read);
        var formData = ImageUtils.BuildMultipartFormDataForImage(testFilePath, fileContent);

        // Act
        var response = await _client.PostAsync($"api/images/user/{userId}", formData);
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
    public async Task POST_Invalid_ImageFomat_Should_Not_Be_Uploaded(string testFilePath,
                                                                     string userId,
                                                                     HttpStatusCode expectedStatusCode)
    {
        // Arrange 

        // Act
        // Ensure the file exists before proceeding
        Assert.True(File.Exists(testFilePath), $"Test file not found at {testFilePath}");

        await using var fileContent = new FileStream(testFilePath, FileMode.Open, FileAccess.Read);
        var formData = ImageUtils.BuildMultipartFormDataForImage(testFilePath, fileContent);

        var response = await _client.PostAsync($"api/images/user/{userId}", formData);
        _outputHelper.WriteLine($"{response.StatusCode} - {response.ReasonPhrase}");

        //Assert
        response.StatusCode.Should().Be(expectedStatusCode);
        response.ReasonPhrase.Should().Be("Bad Request");
    }

    [Theory]
    [MemberData(nameof(ImageTestCases.ImageIdsTestData), MemberType = typeof(ImageTestCases))]
    public async Task GET_Retrieve_ImageById(int imageId,
                                             HttpStatusCode expectedStatusCode)
    {
        // Arrange

        // Act
        var response = await _client.GetAsync($"api/images/user/{imageId}");

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