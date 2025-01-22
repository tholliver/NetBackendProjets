using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Shared.DataTransferObjects;

namespace ImageProcessingService.API.IntegrationTests.TestUtils;

public class AuthUserTests
{
    public static async Task<TokenDto> AuthenticateUser(HttpClient _client,
                                                        string username,
                                                        string password)
    {
        var user = new UserForAuthenticationDto
        {
            UserName = username,
            Password = password
        };

        var jsonContent = JsonSerializer.Serialize(user);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/auth/login", httpContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenDto = JsonSerializer.Deserialize<TokenDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return tokenDto;
    }
}
