using System;
using System.Net.Http.Headers;

namespace ImageProcessingService.API.IntegrationTests.TestUtils;

public class ImageUtils
{
    public static MultipartFormDataContent BuildMultipartFormDataForImage(string filePath, FileStream fileContent)
    {
        var formData = new MultipartFormDataContent
        {
            {
                new StreamContent(fileContent)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue($"image/{Path.GetExtension(filePath)}") }
                },
                "file", Path.GetFileName(filePath)
            }
        };

        return formData;
    }
}
