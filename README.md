# TaskTracker
C# console application task tracker used to track and manage tasks.

This is a solution for the [Task Tracker](https://roadmap.sh/projects/task-tracker) project on [roadmap.sh](https://roadmap.sh/).

## Features
- Add new tasks
- Update existing tasks with a new description
- Delete tasks by ID
- Mark tasks as either in progress, or done
- List all tasks and filter by status

### Example Usage
```bash
# Add a new task with the description "Buy groceries" and set it as to do
task-cli add "Buy groceries"

# Update the task description with ID of 1 to "Buy groceries and cook dinner"
task-cli update 1 "Buy groceries and cook dinner"

# Deleting a task with ID of 1
task-cli delete 1

# Mark the task with ID of 1 as in-progress or done
task-cli mark-in-progress 1
task-cli mark-done 1

# List all tasks
task-cli list

# List tasks by status
task-cli list done
task-cli list todo
task-cli list in-progress
```

## Installation
1. Clone the repository
```bash
git clone https://github.com/sy0311/TaskTracker.git
```
2. Navigate to the project directory
```bash
cd TaskTracker
```
3. Restore dependencies
```bash
dotnet restore
```
4. Build the project
```bash
dotnet build
```
5. Run the application
```bash
dotnet run
```