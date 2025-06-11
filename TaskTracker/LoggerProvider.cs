using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker
{
    public static class LoggerProvider
    {
        // configure logging using serilog
        public static readonly Logger logger = new LoggerConfiguration()
            .WriteTo.File(Constants.LOG_FILENAME)
            .CreateLogger();
    }
}
