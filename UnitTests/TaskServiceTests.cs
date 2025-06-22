using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Services;

namespace UnitTests
{
    public class TaskServiceTests
    {
        #region AddTask
        [Fact]
        public void AddTask_EmptyTaskItem_AddTaskToList()
        {
            // Arrange
            var taskName = "Task Name";
            var taskList = new List<TaskItem>();

            var timeBefore = DateTime.Now;

            // Act
            TaskService.AddTask(taskName, taskList);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(1, task.Id);
            Assert.Equal("todo", task.Status);
            Assert.InRange(task.CreatedAt, timeBefore, timeAfter);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }

        [Fact]
        public void AddTask_ExistingTaskItem_AddTaskToList()
        {
            // Arrange
            var taskName = "Task Name";
            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "First task",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.AddTask(taskName, taskList);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Equal(2, taskList.Count);

            var task = taskList[1];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(2, task.Id);
            Assert.Equal("todo", task.Status);
            Assert.InRange(task.CreatedAt, timeBefore, timeAfter);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }

        [Fact]
        public void AddTask_TaskWithSameTaskName_AddTaskToList()
        {
            // Arrange
            var taskName = "Task Name";
            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.AddTask(taskName, taskList);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Equal(2, taskList.Count);

            var task = taskList[1];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(2, task.Id);
            Assert.Equal("todo", task.Status);
            Assert.InRange(task.CreatedAt, timeBefore, timeAfter);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }

        [Fact]
        public void AddTask_TaskItemWithGreaterId_AddTaskToList()
        {
            // Arrange
            var taskName = "Task Name";
            var taskItem = new TaskItem
            {
                Id = 5,
                Description = "First task",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.AddTask(taskName, taskList);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Equal(2, taskList.Count);

            var task = taskList[1];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(6, task.Id);
            Assert.Equal("todo", task.Status);
            Assert.InRange(task.CreatedAt, timeBefore, timeAfter);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }
        #endregion

        #region UpdateTask
        [Fact]
        public void UpdateTask_GivenDescription_UpdatesTask()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newTaskName = "New Task Name";

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(taskId, taskList, description: newTaskName);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(newTaskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(status, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }

        [Fact]
        public void UpdateTask_GivenStatus_UpdatesTask()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newStatus = "in-progress";

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(taskId, taskList, status: newStatus);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(newStatus, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }

        [Fact]
        public void UpdateTask_GivenDescriptionAndStatus_UpdatesTask()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newTaskName = "New Task Name";
            var newStatus = "in-progress";

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(taskId, taskList, description: newTaskName, status: newStatus);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(newTaskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(newStatus, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.InRange(task.UpdatedAt, timeBefore, timeAfter);
        }

        [Fact]
        public void UpdateTask_IdDoesNotExist_NoUpdates()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newTaskName = "New Task Name";

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(2, taskList, description: newTaskName);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(status, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.Equal(createdTime, task.UpdatedAt);
        }

        [Fact]
        public void UpdateTask_TaskListIsEmpty_NoUpdates()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newTaskName = "New Task Name";

            var taskList = new List<TaskItem>();

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(1, taskList, description: newTaskName);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Empty(taskList);
            // todo using dependency injection, we can check if the logger was called with the correct message
        }

        [Fact]
        public void UpdateTask_EmptyTaskName_NoUpdates()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newTaskName = "";

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(taskId, taskList, description: newTaskName);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(status, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.Equal(createdTime, task.UpdatedAt);
        }

        [Fact]
        public void UpdateTask_EmptyStatus_NoUpdates()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var newStatus = "";

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(taskId, taskList, status: newStatus);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(status, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.Equal(createdTime, task.UpdatedAt);
        }

        [Fact]
        public void UpdateTask_TaskNameAndStatusIsNull_NoUpdates()
        {
            // Arrange
            var taskId = 1;
            var taskName = "Old Task Name";
            var status = "todo";
            var createdTime = DateTime.Now.AddHours(-2);

            var taskItem = new TaskItem
            {
                Id = taskId,
                Description = taskName,
                Status = status,
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };
            var taskList = new List<TaskItem> { taskItem };

            var timeBefore = DateTime.Now;

            // Act
            TaskService.UpdateTask(taskId, taskList);

            var timeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);

            var task = taskList[0];
            Assert.Equal(taskName, task.Description);
            Assert.Equal(taskId, task.Id);
            Assert.Equal(status, task.Status);
            Assert.Equal(createdTime, task.CreatedAt);
            Assert.Equal(createdTime, task.UpdatedAt);
        }
        #endregion

        #region DeleteTask
        [Fact]
        public void DeleteTask_TaskExists_DeletesTask()
        {
            // Arrange
            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };

            var taskList = new List<TaskItem> { taskItem };

            // Act
            TaskService.DeleteTask(1, taskList);

            // Assert
            Assert.NotNull(taskList);
            Assert.Empty(taskList);
        }

        [Fact]
        public void DeleteTask_TaskExists_DeletesOnlyOneTask()
        {
            // Arrange
            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task one",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };

            var taskItem2 = new TaskItem
            {
                Id = 2,
                Description = "Task two",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };

            var taskList = new List<TaskItem> { taskItem, taskItem2 };

            // Act
            TaskService.DeleteTask(2, taskList);

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);
            Assert.Equal(1, taskList[0].Id);
        }

        [Fact]
        public void DeleteTask_EmptyTaskList_NoDeletes()
        {
            // Arrange
            var taskList = new List<TaskItem>();

            // Act
            TaskService.DeleteTask(1, taskList);

            // Assert
            Assert.NotNull(taskList);
            Assert.Empty(taskList);

            // todo using dependency injection, we can check if the logger was called with the correct message
            // todo console log right output "Task with id=1 doesn't exist."
        }

        [Fact]
        public void DeleteTask_TaskIdDoesNotExist_NoDeletes()
        {
            // Arrange
            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            };

            var taskList = new List<TaskItem> { taskItem };

            // Act
            TaskService.DeleteTask(2, taskList);

            // Assert
            Assert.NotNull(taskList);
            Assert.Single(taskList);
            Assert.Equal(1, taskList[0].Id);
        }

        #endregion

        #region ListTasks
        [Fact]
        public void ListTasks_OneTaskNoStatusFilter_ShowsInConsole()
        {
            // Arrange

            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = new DateTime(2025, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2025, 6, 22, 14, 30, 10)
            };

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem> { taskItem };

            // Act
            TaskService.ListTasks(taskList);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.Contains($"| 1     | Task Name                 | todo            | 20/06/2025 11:30:05 am    | 22/06/2025 2:30:10 pm     |\r\n", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }

        [Fact]
        public void ListTasks_MultipleTasksNoStatusFilter_ShowsInConsole()
        {
            // Arrange

            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = new DateTime(2025, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2025, 6, 22, 14, 30, 10)
            };

            var taskItem2 = new TaskItem
            {
                Id = 2,
                Description = "Second Task Name",
                Status = "in-progress",
                CreatedAt = new DateTime(2024, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2024, 6, 22, 14, 30, 10)
            };

            var taskItem3 = new TaskItem
            {
                Id = 3,
                Description = "Third Task Name",
                Status = "done",
                CreatedAt = new DateTime(2023, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2023, 6, 22, 14, 30, 10)
            };

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem> { taskItem, taskItem2, taskItem3 };

            // Act
            TaskService.ListTasks(taskList);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.Contains($"| 1     | Task Name                 | todo            | 20/06/2025 11:30:05 am    | 22/06/2025 2:30:10 pm     |\r\n", output);
            Assert.Contains($"| 2     | Second Task Name          | in-progress     | 20/06/2024 11:30:05 am    | 22/06/2024 2:30:10 pm     |\r\n", output);
            Assert.Contains($"| 3     | Third Task Name           | done            | 20/06/2023 11:30:05 am    | 22/06/2023 2:30:10 pm     |\r\n", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }

        [Fact]
        public void ListTasks_MultipleTasksWithStatusFilter_ShowsInConsole()
        {
            // Arrange

            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = new DateTime(2025, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2025, 6, 22, 14, 30, 10)
            };

            var taskItem2 = new TaskItem
            {
                Id = 2,
                Description = "Second Task Name",
                Status = "in-progress",
                CreatedAt = new DateTime(2024, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2024, 6, 22, 14, 30, 10)
            };

            var taskItem3 = new TaskItem
            {
                Id = 3,
                Description = "Third Task Name",
                Status = "done",
                CreatedAt = new DateTime(2023, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2023, 6, 22, 14, 30, 10)
            };

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem> { taskItem, taskItem2, taskItem3 };

            // Act
            TaskService.ListTasks(taskList, "done");

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.DoesNotContain($"| 1     | Task Name                 | todo            | 20/06/2025 11:30:05 am    | 22/06/2025 2:30:10 pm     |\r\n", output);
            Assert.DoesNotContain($"| 2     | Second Task Name          | in-progress     | 20/06/2024 11:30:05 am    | 22/06/2024 2:30:10 pm     |\r\n", output);
            Assert.Contains($"| 3     | Third Task Name           | done            | 20/06/2023 11:30:05 am    | 22/06/2023 2:30:10 pm     |\r\n", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }

        [Fact]
        public void ListTasks_EmptyTaskListNoStatusFilter_ShowNoTaskMessage()
        {
            // Arrange

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem>();

            // Act
            TaskService.ListTasks(taskList);

            // Assert
            string output = stringWriter.ToString();
            Assert.DoesNotContain($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.Contains("No tasks found.", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }

        [Fact]
        public void ListTasks_EmptyTaskListWithStatusFilter_ShowNoTaskMessage()
        {
            // Arrange

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem>();

            // Act
            TaskService.ListTasks(taskList, "done");

            // Assert
            string output = stringWriter.ToString();
            Assert.DoesNotContain($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.Contains("No tasks found.", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }

        [Fact]
        public void ListTasks_NoTaskWithStatusFilter_ShowNoTaskMessage()
        {
            // Arrange

            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name",
                Status = "todo",
                CreatedAt = new DateTime(2025, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2025, 6, 22, 14, 30, 10)
            };

            var taskItem2 = new TaskItem
            {
                Id = 2,
                Description = "Second Task Name",
                Status = "in-progress",
                CreatedAt = new DateTime(2024, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2024, 6, 22, 14, 30, 10)
            };

            var taskItem3 = new TaskItem
            {
                Id = 3,
                Description = "Third Task Name",
                Status = "in-progress",
                CreatedAt = new DateTime(2023, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2023, 6, 22, 14, 30, 10)
            };

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem> { taskItem, taskItem2, taskItem3 };

            // Act
            TaskService.ListTasks(taskList, "done");

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains("No tasks found.", output);
            Assert.DoesNotContain($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.DoesNotContain($"| 1     | Task Name                 | todo            | 20/06/2025 11:30:05 am    | 22/06/2025 2:30:10 pm     |\r\n", output);
            Assert.DoesNotContain($"| 2     | Second Task Name          | in-progress     | 20/06/2024 11:30:05 am    | 22/06/2024 2:30:10 pm     |\r\n", output);
            Assert.DoesNotContain($"| 3     | Third Task Name           | done            | 20/06/2023 11:30:05 am    | 22/06/2023 2:30:10 pm     |\r\n", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }
        [Fact]
        public void ListTasks_TaskWithLongDescription_ShowsInConsole()
        {
            // Arrange

            var taskItem = new TaskItem
            {
                Id = 1,
                Description = "Task Name Task Name Task Name Task Name Task Name",
                        // todo: probably better for us to limit task names when creating tasks and throw if too long
                Status = "todo",
                CreatedAt = new DateTime(2025, 6, 20, 11, 30, 05),
                UpdatedAt = new DateTime(2025, 6, 22, 14, 30, 10)
            };

            // redirect console output to a StringWriter. So we can verify the console output
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            var taskList = new List<TaskItem> { taskItem };

            // Act
            TaskService.ListTasks(taskList);

            // Assert
            string output = stringWriter.ToString();
            Assert.Contains($"| Id    | Description               | Status          | Created At                | Updated At                |\r\n", output);
            Assert.Contains($"| 1     | Task Name Task Name Task Name Task Name Task Name | todo            | 20/06/2025 11:30:05 am    | 22/06/2025 2:30:10 pm     |\r\n", output);

            // restore the original console output
            Console.SetOut(originalOut);
        }
        #endregion
    }
}
