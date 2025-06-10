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
                { "list", "List out tasks" },
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
                        TaskService.AddTask(taskName, taskList);
                        break;
                    case "update":
                        id = int.Parse(cmdLineArgs[1]);
                        taskName = cmdLine.Split('"', '"')[1];
                        TaskService.UpdateTask(id, taskList, description: taskName);
                        break;
                    case "delete":
                        id = int.Parse(cmdLineArgs[1]);
                        TaskService.DeleteTask(id, taskList);
                        break;
                    case "mark-in-progress":
                        id = int.Parse(cmdLineArgs[1]);
                        TaskService.UpdateTask(id, taskList, status: Status.IN_PROGRESS);
                        break;
                    case "mark-done":
                        id = int.Parse(cmdLineArgs[1]);
                        TaskService.UpdateTask(id, taskList, status: Status.DONE);
                        break;
                    case "list":
                        if (cmdLineArgs.Length == 1)
                            TaskService.ListTasks(taskList);
                        else
                            TaskService.ListTasks(taskList, cmdLineArgs[1]);
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }
    }
}