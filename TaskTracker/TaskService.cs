using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker
{
    public static class TaskService
    {
        public static void AddTask(string taskName, List<TaskItem> taskList)
        {
            Console.WriteLine($"Adding task {taskName}...");

            // create task item
            var newTaskId = taskList.Any() ? taskList.Max(x => x.Id) + 1 : 1;
            var newTask = CreateTaskItem(newTaskId, taskName);

            // add to list
            taskList.Add(newTask);

            FileManager.WriteToTaskListToFile(taskList);
        }

        public static void UpdateTask(int id, List<TaskItem> taskList, string? description = null, string? status = null)
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

        public static void DeleteTask(int id, List<TaskItem> taskList)
        {
            Console.WriteLine($"Deleting task with id {id}...");

            // find item on list with matching id
            var task = taskList.FirstOrDefault(i => i.Id == id);

            if (task != null) taskList.Remove(task);

            // write to file
            FileManager.WriteToTaskListToFile(taskList);
        }

        public static TaskItem CreateTaskItem(int id, string description, string status = Status.TODO)
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

        public static void ListTasks(List<TaskItem> taskList, string? status = null)
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
