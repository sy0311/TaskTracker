using Serilog;
using Serilog.Core;

namespace TaskTracker.Utilities
{
    public static class LoggerProvider
    {
        // configure logging using serilog
        public static readonly Logger logger = new LoggerConfiguration()
            .WriteTo.File(Constants.LOG_FILENAME)
            .CreateLogger();
    }
}
