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
        /** happy path
         * - delete task that exists
        unhappy path
         * - empty task list
         * - id doesnt exist in task list
        **/
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
        /**
         * - list all tasks
         * - list tasks with status filter
         * - list empty task list
         * - list empty task list with status filter
         * - list a very long task description
         **/
        #endregion
    }
}
