using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to your Task Tracker!\n");

            List<TaskItem> taskList = FileManager.ImportTasksJsonFile();

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
                cmdLine = Console.ReadLine();   // todo check readline is not null and valid

                // try splitting the cmdLine
                string[] cmdLineArgs = cmdLine.Split(' ');
                if (cmdLineArgs.Length == 0 || string.IsNullOrWhiteSpace(cmdLineArgs[0]))
                {
                    Console.WriteLine("Invalid command. Please try again.");
                    continue;
                }
                string cmdVerb = cmdLineArgs[0];

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
                        id = int.Parse(cmdLineArgs[1]);
                        taskName = cmdLine.Split('"', '"')[1];
                        UpdateTask(id, taskList, description: taskName);
                        break;
                    case "delete":
                        id = int.Parse(cmdLineArgs[1]);
                        DeleteTask(id, taskList);
                        break;
                    case "mark-in-progress":
                        id = int.Parse(cmdLineArgs[1]);
                        UpdateTask(id, taskList, status: Status.IN_PROGRESS);
                        break;
                    case "mark-done":
                        id = int.Parse(cmdLineArgs[1]);
                        UpdateTask(id, taskList, status: Status.DONE);
                        break;
                    case "list":
                        if (cmdLineArgs.Length == 1)
                            ListTasks(taskList);
                        else
                            ListTasks(taskList, cmdLineArgs[1]);
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

            FileManager.WriteToTaskListToFile(taskList);
        }

        static void UpdateTask(int id, List<TaskItem> taskList, string? description = null, string? status = null)
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
            FileManager.WriteToTaskListToFile(taskList);
        }

        static void DeleteTask(int id, List<TaskItem> taskList)
        {
            Console.WriteLine($"Deleting task with id {id}...");

            // find item on list with matching id
            var task = taskList.FirstOrDefault(i => i.Id == id);

            if (task != null) taskList.Remove(task);

            // write to file
            FileManager.WriteToTaskListToFile(taskList);
        }

        static TaskItem CreateTaskItem(int id, string description, string status = Status.TODO)
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

        static void ListTasks(List<TaskItem> taskList, string? status = null)
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            string[] headers = { "Id", "Description", "Status", "Created At", "Updated At" };

            Console.WriteLine($"| {headers[0],-5} | {headers[1],-25} | {headers[2],-15} | {headers[3],-25} | {headers[4],-25} |");

            foreach (TaskItem task in taskList)
            {
                if (status == null || task.Status == status)
                {
                    Console.WriteLine($"| {task.Id,-5} | {task.Description,-25} | {task.Status,-15} | {task.createdAt,-25} | {task.updatedAt,-25} |");
                }
            }
        }
    }
}