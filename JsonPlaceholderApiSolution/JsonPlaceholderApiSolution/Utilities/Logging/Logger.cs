using Serilog;

namespace ApiTests.Utilities.Logging
{
    /*
     * Serilog logger for the test framework.
     * Writes logs to both the console and a rolling log file.
     */
    public static class Logger
    {
        public static readonly ILogger Log = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/apitests-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}