using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace TaskTracker
{
    class Program
    {

        public static string FILENAME = "tasks.json";
        public static string FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), FILENAME);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to your Task Tracker!\n");

            List<TaskItem> taskList = ImportTasksJsonFile();

            // Create a Dictionary of all the commands
            Dictionary<string, string> commands = new Dictionary<string, string>
            {
                { "add", "Add a new task" },
                { "update", "Update an existing task" },
                { "delete", "Delete a task" },
                { "end" , "End Program"}
            };

            // Display the commands to the user
            Console.WriteLine("Available commands:");
            foreach (var command in commands)
            {
                Console.WriteLine($" - {command.Key}: {command.Value}");
            }

            string? cmdLine = "";

            while (cmdLine != "end")
            {
                // Ask user for a command
                Console.WriteLine("\nEnter a command: ");
                cmdLine = Console.ReadLine();

                // try splitting the cmdLine
                string cmdVerb = cmdLine.Split(' ')[0];

                // todo: check cmdWords[0] exists / search positional arguments

                string taskName;
                int id;

                // check which command verb was called
                switch (cmdVerb)
                {
                    case "add":
                        // todo: check taskName exists
                        taskName = cmdLine.Split('"', '"')[1];
                        AddTask(taskName, taskList);
                        break;
                    case "update":
                        id = int.Parse(cmdLine.Split(' ')[1]);
                        taskName = cmdLine.Split('"', '"')[1];
                        UpdateTask(id, taskList, description: taskName);
                        break;
                    case "delete":
                        id = int.Parse(cmdLine.Split(' ')[1]);
                        DeleteTask(id, taskList);
                        break;
                    case "mark-in-progress":
                        id = int.Parse(cmdLine.Split(' ')[1]);
                        UpdateTask(id, taskList, status: "in-progress");    // todo: change these to constants
                        break;
                    case "mark-done":
                        id = int.Parse(cmdLine.Split(' ')[1]);
                        UpdateTask(id, taskList, status: "done");    // todo: change these to constants
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }

        static void AddTask(string taskName, List<TaskItem> taskList)
        {
            Console.WriteLine($"Adding task {taskName}...");

            // create task item
            var newTaskId = taskList.Any() ? taskList.Max(x => x.Id) + 1 : 1;
            var newTask = CreateTaskItem(newTaskId, taskName);

            // add to list
            taskList.Add(newTask);

            WriteToTaskListToFile(taskList);
        }

        static void UpdateTask(int id, List<TaskItem> taskList, string? description=null, string? status=null)
        {
            Console.WriteLine($"Updating task with id {id}...");

            // Find item on list with matching id
            var task = taskList.FirstOrDefault(i => i.Id == id);

            // Update item
            if (task != null)
            {
                if (description != null)
                {
                    task.Description = description;
                }
                if (status != null)
                {
                    task.Status = status;
                }
                task.updatedAt = DateTime.Now;
            }
            else
            {
                Console.WriteLine($"Task with id={id} doesn't exist.");

            }

            // write to file
            WriteToTaskListToFile(taskList);
        }

        static void DeleteTask(int id, List<TaskItem> taskList)
        {
            Console.WriteLine($"Deleting task with id {id}...");

            // find item on list with matching id
            var task = taskList.FirstOrDefault(i => i.Id == id);

            if (task != null) taskList.Remove(task);

            // write to file
            WriteToTaskListToFile(taskList);
        }

        static List<TaskItem> ImportTasksJsonFile()
        {
            // check if file exists
            if (!File.Exists(FILEPATH))
            {
                Console.WriteLine($"File doesn't exist... Creating file.");
                // if not, create file
                File.Create(FILEPATH).Close();
                Console.WriteLine($"File created.");
            }

            // read file contents
            string jsonString = File.ReadAllText(FILEPATH);

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

        static void WriteToTaskListToFile(List<TaskItem> taskList)
        {
            // serialize object
            string jsonString = JsonSerializer.Serialize(taskList);

            // write to file
            File.WriteAllText(FILEPATH, jsonString);

            Console.WriteLine(jsonString);
        }

        static TaskItem CreateTaskItem(int id, string description, string status="todo")
        {
            // todo: generate id
            return new TaskItem
            {
                Id = id,
                Description = description,
                Status = status,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };
        }
    }
}