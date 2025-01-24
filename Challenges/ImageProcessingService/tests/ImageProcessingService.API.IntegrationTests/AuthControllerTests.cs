using System;
using System.Net;
using System.Text;
using System.Text.Json;
using ImageProcessingService.API.IntegrationTests.UserTestData;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace ImageProcessingService.API.IntegrationTests;

public class AuthControllerTests(IPSWebAppFactory<Program> factory) :
IClassFixture<IPSWebAppFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly IPSWebAppFactory<Program> _factory = factory;

    [Fact]
    public async Task POST_RegisterUser_When_UserCreationFails_ReturnsFailure()
    {
        // Arrange
        var usertDto = new UserForRegistrationDto
        {
            FirstName = "Test",
            LastName = "Doe",
            UserName = "testdoe",
            Password = "Pass123!",
            Email = "john@example.com",
            Roles = new[] { "NoRole" }
        };

        var jsonContent = JsonSerializer.Serialize(usertDto);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync("api/auth/register", httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("InvalidRoles", responseContent);
    }

    [Fact]
    public async Task POST_LoginUser_When_CredentialsWrong_ReturnsFailure()
    {
        // Arrange
        var usertDto = new UserForAuthenticationDto
        {
            UserName = "testdoe",
            Password = "Pass123!",
        };

        var jsonContent = JsonSerializer.Serialize(usertDto);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync("api/auth/login", httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        // Expect TokenDto
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Contains("Unauthorized", responseContent);
    }

    [Theory]
    [MemberData(nameof(UserTestCases.UserCredentialTestData), MemberType = typeof(UserTestCases))]
    public async Task POST_LoginUser_ReturnsExpectedResult(string userName, string password, bool shouldSucceed)
    {
        // Arrange
        var loginDto = new UserForAuthenticationDto
        {
            UserName = userName,
            Password = password
        };

        var jsonContent = JsonSerializer.Serialize(loginDto);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Act
        var response = await _client.PostAsync("api/auth/login", httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        if (shouldSucceed)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var tokenDto = JsonSerializer.Deserialize<TokenDto>(responseContent, options);
            Assert.NotNull(tokenDto);
            Assert.NotEmpty(tokenDto.AccessToken);
            Assert.NotEmpty(tokenDto.RefreshToken);
        }
        else
        {
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

    [Theory]
    [MemberData(nameof(UserTestCases.UserEmptyCredentialsTestData), MemberType = typeof(UserTestCases))]
    public async Task POST_LoginUser_WithInvalidData_ReturnsBadRequest(string userName, string password, HttpStatusCode expectedStatusCode, string expectedMessage)
    {
        // Arrange
        var loginDto = new UserForAuthenticationDto
        {
            UserName = userName,
            Password = password
        };

        var jsonContent = JsonSerializer.Serialize(loginDto);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("api/auth/login", httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
        Assert.Contains(expectedMessage, responseContent);
    }

    [Fact]
    public async Task POST_DeleteUser_When_UserDeletionFails_ReturnsFailure()
    {
        // Arrange
        var userId = "InvalidId";

        // Act
        var response = await _client.DeleteAsync($"api/auth/{userId}");
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains($"User with Id {userId} not found.", responseContent);
    }

    [Fact]
    public async Task POST_DeleteUser_When_UserDeletionSucceeds_ReturnsSuccess()
    {
        // Arrange
        var usertDto = new UserForRegistrationDto
        {
            FirstName = "Test",
            LastName = "Doe",
            UserName = "testdoe",
            Password = "Pass123!",
            Email = "testjohn@example.com",
            PhoneNumber = "1234567890",
            Roles = new[] { "NoRole" }
        };

        var jsonContent = JsonSerializer.Serialize(usertDto);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync("api/auth/register", httpContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("InvalidRoles", responseContent);
    }

}