namespace LinQMax.Utils
{
    using System;
    using System.Reflection;
    using Microsoft.Extensions.Logging;

    public static class MethodTimeLogger
    {
        public static ILogger Logger;
        public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
        {
            Logger.LogTrace("{Class}.{methodBase.Name} took {timeSpan.TotalMilliseconds} ms. {message}");
        }
    }
}