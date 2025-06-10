using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
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
                { $"{Commands.ADD} \"<task-name>\"", "Add a new task" },
                { $"{Commands.UPDATE} <id> \"<new-task-name>\"", "Update an existing task" },
                { $"{Commands.DELETE} <id>", "Delete a task" },
                { $"{Commands.MARK_IN_PROGRESS} <id>", "Mark a task in progress" },
                { $"{Commands.MARK_DONE} <id>", "Mark a task done" },
                { $"{Commands.LIST}", "List all tasks" },
                { $"{Commands.LIST} <status>", "List tasks by status" },
                { Commands.EXIT , "End Program"}
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

                /* todo: test scenarios
                 * - if user enters an empty command
                 * - if user enters an invalid command
                 * - if user enters whitespaces
                 * - command with capitals
                */

                Console.WriteLine("\nEnter a command: ");
                cmdLine = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(cmdLine))
                {
                    Console.WriteLine("Invalid command. Please try again.");
                    continue;
                }

                string cmdVerb = cmdLine.Split(' ')[0];

                string taskName;
                int id;

                // todo: try catch FormatException

                try
                {
                    // check which command verb was called
                    switch (cmdVerb)
                    {
                        case Commands.ADD:

                            /* todo tests
                             * - add "task name
                             * - add task name"
                             * - add task name
                             * - add "task name"
                             * - add "task name" "and another"
                             * - add "task name" (white spaces at the end)
                             * - add "task name" fsfsdkj
                             */

                            taskName = GetArgsForAddCommand(cmdLine);
                            TaskService.AddTask(taskName, taskList);
                            break;

                        case Commands.UPDATE:

                            /* todo tests
                             * - update 1 "new task name"
                             * - update "new task name" 1
                             * - update 1 new task name
                             * - update 1 "new task name
                             * - update 1.0 "new task name"
                             * - update id which does not exist
                             */

                            (id, taskName) = GetArgsForUpdateCommand(cmdLine);
                            TaskService.UpdateTask(id, taskList, description: taskName);
                            break;

                        case Commands.DELETE:
                            id = GetArgsForCommandWithId(cmdLine, Commands.DELETE);
                            TaskService.DeleteTask(id, taskList);
                            break;
                        case Commands.MARK_IN_PROGRESS:
                            id = GetArgsForCommandWithId(cmdLine, Commands.MARK_IN_PROGRESS);
                            TaskService.UpdateTask(id, taskList, status: Status.IN_PROGRESS);
                            break;
                        case Commands.MARK_DONE:
                            id = GetArgsForCommandWithId(cmdLine, Commands.MARK_DONE);
                            TaskService.UpdateTask(id, taskList, status: Status.DONE);
                            break;
                        case Commands.LIST:
                            if (cmdLine.Split(' ').Length == 1)
                                // list all
                                TaskService.ListTasks(taskList);
                            else if (cmdLine.Split(' ').Length == 2)
                                // list by status
                                TaskService.ListTasks(taskList, cmdLine.Split(' ')[1]);
                            else
                                Console.WriteLine($"Invalid command format for '{Commands.LIST}' command.");
                            break;
                        default:
                            Console.WriteLine("Invalid command. Please try again.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        static string GetArgsForAddCommand(string input)
        {

            if (IsValidCommandFormat(input, $@"^{Commands.ADD}\s+""[^""]+""$"))
                return input.Split('"')[1];

            throw new FormatException($"Invalid command format for '{Commands.ADD}' command.");
        }

        static (int, string) GetArgsForUpdateCommand(string input)
        {
            if (IsValidCommandFormat(input, $@"^{Commands.UPDATE}\s+\d+\s+""[^""]+""$"))
            {
                int id = int.Parse(input.Split(' ')[1]);
                string taskName = input.Split('"')[1];
                return (id, taskName);
            }

            throw new FormatException($"Invalid command format for '{Commands.UPDATE}' command.");
        }

        static int GetArgsForCommandWithId(string input, string commandVerb)
        {
            if (IsValidCommandFormat(input, $@"^{commandVerb}\s+\d+$"))
            {
                return int.Parse(input.Split(' ')[1]);
            }
            throw new FormatException($"Invalid command format for '{commandVerb}' command.");
        }

        static bool IsValidCommandFormat(string input, string regexPattern)
        {
            // Check if the command line is of the correct format
            return Regex.IsMatch(input.Trim(), regexPattern);
        }
    }
}