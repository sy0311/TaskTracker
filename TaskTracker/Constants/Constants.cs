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

        // Dictionary of the commands and their descriptions
        public static readonly Dictionary<string, string> CommandDescriptions = new()
        {
            { $"{ADD} \"<task-name>\"", "Add a new task" },
            { $"{UPDATE} <id> \"<new-task-name>\"", "Update an existing task" },
            { $"{DELETE} <id>", "Delete a task" },
            { $"{MARK_IN_PROGRESS} <id>", "Mark a task in progress" },
            { $"{MARK_DONE} <id>", "Mark a task done" },
            { $"{LIST}", "List all tasks" },
            { $"{LIST} <status>", "List tasks by status" },
            { EXIT , "End Program"}
        };
    }

    public static class Constants
    {
        public const string FILENAME = "tasks.json";
        public static readonly string FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), FILENAME);
        public const string LOG_FILENAME = "log.txt";
    }
}
