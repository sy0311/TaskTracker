using System.Text.Json;
using TaskTracker.Models;

namespace TaskTracker.Utilities
{
    // defines FileManager as a static class that can be used in Program.cs like a utility class
    public static class FileManager
    {
        public static List<TaskItem> ImportTasksJsonFile()
        {
            // check if file exists
            if (!File.Exists(Constants.FILEPATH))
            {
                LoggerProvider.logger.Information($"Task file {Constants.FILEPATH} doesn't exist. Creating an empty file...");
                // if not, create file
                File.Create(Constants.FILEPATH).Close();
            }

            // read file contents
            string jsonString = File.ReadAllText(Constants.FILEPATH);

            // convert json string to object
            if (string.IsNullOrEmpty(jsonString))
            {
                // if file is empty, create empty list
                return new List<TaskItem>();
            }
            else
            {
                var tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);
                return tasks ?? new List<TaskItem>(); // Ensure a non-null return value
            }
        }

        public static void WriteToTaskListToFile(List<TaskItem> taskList)
        {
            // serialize object
            string jsonString = JsonSerializer.Serialize(taskList);

            // write to file
            File.WriteAllText(Constants.FILEPATH, jsonString);

            LoggerProvider.logger.Information($"Wrote tasks into file.");
        }
    }
}
