using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects;
public record TokenDto(string AccessToken, string RefreshToken);