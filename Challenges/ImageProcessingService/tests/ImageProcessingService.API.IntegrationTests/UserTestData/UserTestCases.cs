using System;
using System.Net;

namespace ImageProcessingService.API.IntegrationTests.UserTestData;

public class UserTestCases
{
    public static IEnumerable<object[]> UserCredentialTestData => new[]
    {
        new object []{ "johndoe", "StrongPassword123!", true },
        new object []{ "johndoez","StrongPassword123!", false },
        new object []{ "johndoe", "no", false},
        new object []{ "no", "NoPass", false},
    };


    public static IEnumerable<object[]> UserEmptyCredentialsTestData => new[]
    {
        new object []{ "", "StrongPassword123!",HttpStatusCode.BadRequest, "User name is required." },
        new object []{ "johndoe","", HttpStatusCode.BadRequest, "Password name is required." },
        new object []{ "", "",HttpStatusCode.BadRequest ,"One or more validation errors occurred."},
    };
}
