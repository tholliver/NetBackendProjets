using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ImageProcessingService.API.Tests;

public class IPSImagesControllerTests
{
    [Fact]
    public async Task GET_Retrieve_all_images()
    {
        await using var app = new WebApplicationFactory<Api.Startup>();
    }
}
