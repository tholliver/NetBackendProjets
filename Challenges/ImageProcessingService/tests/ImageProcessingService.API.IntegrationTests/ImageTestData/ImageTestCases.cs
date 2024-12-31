using System.Net;

namespace ImageProcessingService.API.IntegrationTests.ImageTestData;

public class ImageTestCases
{
    public static IEnumerable<object[]> ImageIdsTestData => new[]
    {
        new object []{ 2, HttpStatusCode.OK },
        new object []{ 999, HttpStatusCode.NotFound },
    };

    public static IEnumerable<object[]> ImageFilesTestData => new[]
    {
        new object [] { @"C:\Users\DaMagic\Pictures\Screenshots\Screenshot 2024-11-22 205058.png",
                            "8bfc8b29-a139-4883-a0ac-ae04095951ce",
                            HttpStatusCode.Created
        },
        new object [] {
            @"C:\Users\DaMagic\Pictures\Screenshots\Screenshot 2024-11-26 184222.png",
            "8bfc8b29-a139-4883-a0ac-ae04095951ce",
            HttpStatusCode.Created
        },
        // new object[] { @"C:\Users\DaMagic\Pictures\NissanDocs121.jpg",
        //                     "8bfc8b29-a139-4883-a0ac-ae04095951ce",
        //                     HttpStatusCode.Created
        // },
    };

    public static IEnumerable<object[]> NotValidImageFilesTestData => new[]
    {
        new object[] {
            @"C:\Users\DaMagic\Documents\SoleQUINQUENIO.docx",
            "8bfc8b29-a139-4883-a0ac-ae04095951ce",
            HttpStatusCode.BadRequest
        },
        new object[] {
            @"C:\Users\DaMagic\Documents\PassportCopy.docx",
            "8bfc8b29-a139-4883-a0ac-ae04095951ce",
            HttpStatusCode.BadRequest
        },
    };

    public static IEnumerable<object[]> LargeSizeImagesTestData => new[]
    {
        new object[]
        {
            @"C:\Users\DaMagic\Pictures\NissanDocs121.jpg",
            "8bfc8b29-a139-4883-a0ac-ae04095951ce",
            HttpStatusCode.BadRequest
        }
    };
}