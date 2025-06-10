using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker
{
    public class Status
    {
        public const string TODO = "todo";
        public const string IN_PROGRESS = "in-progress";
        public const string DONE = "done";
    }

    public class Constants
    {
        public const string FILENAME = "tasks.json";
        public static readonly string FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), FILENAME);
    }
}
