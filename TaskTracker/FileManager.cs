using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTracker
{
    // defines FileManager as a static class that can be used in Program.cs like a utility class
    public static class FileManager
    {
        public static List<TaskItem> ImportTasksJsonFile()
        {
            // check if file exists
            if (!File.Exists(Constants.FILEPATH))
            {
                Console.WriteLine($"File doesn't exist... Creating file.");
                // if not, create file
                File.Create(Constants.FILEPATH).Close();
                Console.WriteLine($"File created.");
            }

            // read file contents
            string jsonString = File.ReadAllText(Constants.FILEPATH);

            // convert json string to object
            if (string.IsNullOrEmpty(jsonString))
            {
                Console.WriteLine($"File is empty... Creating empty list.");
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

            //Console.WriteLine(jsonString);    // todo: change to debug log maybe?
        }
    }
}
