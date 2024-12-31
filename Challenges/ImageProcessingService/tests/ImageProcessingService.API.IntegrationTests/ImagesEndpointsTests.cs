using System;
using System.Net.Http.Json;
using Entities.Models;
using FluentAssertions;

namespace ImageProcessingService.API.IntegrationTests;

public class ImagesEndpointsTests(IPSWebAppFactory<Program> factory) :
    IClassFixture<IPSWebAppFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly IPSWebAppFactory<Program> _factory = factory;

    [Fact]
    public async Task GET_Retrieves_All_Images()
    {
        // Arrange
        var response = await _client.GetAsync("api/images");

        // Act
        var match = await response.Content.ReadFromJsonAsync<List<Image>>();

        // Assert
        response.EnsureSuccessStatusCode();
        match?[0].Id.Should().BePositive();
        match?.Count.Should().Be(11);
    }
}
