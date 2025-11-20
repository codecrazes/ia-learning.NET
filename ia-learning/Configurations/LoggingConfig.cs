using Microsoft.Extensions.Logging;

namespace ia_learning.Configurations
{
    public static class LoggingConfig
    {
        public static ILoggingBuilder AddLoggingSetup(this ILoggingBuilder builder)
        {
            builder.ClearProviders();
            builder.AddConsole();
            return builder;
        }
    }
}
