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

    public class Commands
    {
        public const string ADD = "add";
        public const string UPDATE = "update";
        public const string DELETE = "delete";
        public const string MARK_IN_PROGRESS = "mark-in-progress";
        public const string MARK_DONE = "mark-done";
        public const string LIST = "list";
        public const string EXIT = "exit";
    }

    public class Constants
    {
        public const string FILENAME = "tasks.json";
        public static readonly string FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), FILENAME);
    }
}
