using Microsoft.EntityFrameworkCore;
using task_tracker.Interfaces;
using task_tracker.Models;
using Task = task_tracker.Models.Task;

namespace task_tracker.Facades
{
    public class TasksFacade : ITasksFacade
    {
        private readonly ILogger<TasksFacade> _logger;
        private readonly Context _context;

        public TasksFacade(Context context, ILogger<TasksFacade> logger)
        {
            _logger = logger;
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public Task? AddTask(Task task)
        {
            _logger.LogInformation($"Getting a Task for Id: {task.Id}; NickName: {task.TaskName}");

            var newTask = _context.Tasks!.Add(task);
            _context.SaveChanges();

            return GetTaskById(newTask.Entity.Id);
        }

        public async Task<Task?> AddTaskAsync(Task task)
        {
            _logger.LogInformation($"Getting a Task for Id: {task.Id}; NickName: {task.TaskName}");

            var newTask = _context.Tasks!.Add(task);
            await _context.SaveChangesAsync();

            return await GetTaskByIdAsync(newTask.Entity.Id);
        }

        public bool CheckTaskExist(int id)
        {
            return _context.Tasks!.Any(c => c.Id == id);
        }

        public async Task<bool> CheckTaskExistAsync(int id)
        {
            return await _context.Tasks!.AnyAsync(c => c.Id == id);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} from the context.");
            _context.Remove(entity);
        }

        public bool DeleteTask(int id)
        {
            _logger.LogInformation($"Getting a Task for Id: {id}");

            var task = _context.Tasks!
                .FirstOrDefault(c => c.Id == id);

            if (task == null) return false;

            _context.Tasks!.Remove(task);

            _context.SaveChanges();

            _logger.LogInformation($"Task data has been successfully deleted from the database. Task TaskName: {task.TaskName}");

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            _logger.LogInformation($"Getting a Task for Id: {id}");

            using (var ctx = new Context())
            {
                try
                {
                    var task = new Task { Id = id };
                    ctx.Tasks!.Attach(task);
                    ctx.Tasks.Remove(task);
                    await ctx.SaveChangesAsync();
                    _logger.LogInformation($"Task data has been successfully deleted from the database. Task TaskName: {task.TaskName}");
                    return true;
                }
                catch (Exception)
                {
                    _logger.LogInformation($"Task data has NOT been successfully deleted from the database for the Task Id: {id}");
                    return false;
                }
            }
        }

        public Task? EditTask(Task task)
        {
            _logger.LogInformation($"Editing a Task for TaskName: {task.TaskName}");

            _context.Entry(task).State = EntityState.Modified;

            _context.SaveChanges();

            var editedTask = _context.Tasks!.FirstOrDefault(c => c.Id == task.Id);
            _logger.LogInformation($"Task for TaskName: {task.TaskName} was edited succesfully!");

            return editedTask;
        }

        public async Task<Task?> EditTaskAsync(Task task)
        {
            _logger.LogInformation($"Editing a Task for TaskName: {task.TaskName}");

            _context.Entry(task).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var editedTask = await _context.Tasks!.FirstOrDefaultAsync(c => c.Id == task.Id);
            _logger.LogInformation($"Task was edited succesfully!");

            return editedTask;
        }

        public Task[] GetAllTasks()
        {
            _logger.LogInformation($"Getting all Tasks");

            IQueryable<Task> query = _context.Tasks!;

            // Order Tasks by Id Desc
            query = query.OrderByDescending(c => c.Id);

            return query.ToArray();
        }

        public async Task<Task[]> GetAllTasksAsync()
        {
            _logger.LogInformation($"Getting all Tasks");

            IQueryable<Task> query = _context.Tasks!;

            // Order Tasks by Id Desc
            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public int GetTaskDuration(int id)
        {
            _logger.LogInformation($"Getting Duration for a Task with Id: {id}");

            var task = _context.Tasks!
                .FirstOrDefault(c => c.Id == id);

            _logger.LogInformation($"Task Duration has been successfully retrieved from the database. Task Name: {task!.TaskName}");

            return (task.Duration ?? 0)!;
        }

        public async Task<int> GetTaskDurationAsync(int id)
        {
            _logger.LogInformation($"Getting Duration for a Task with Id: {id}");

            var task = await _context.Tasks!
                .FirstOrDefaultAsync(c => c.Id == id);

            _logger.LogInformation($"Task Duration has been successfully retrieved from the database. Task Name: {task!.TaskName} and Task Duration: {task!.Duration}");

            return (task.Duration ?? 0)!;
        }

        public bool IsTaskActive(int id)
        {
            _logger.LogInformation($"Getting Task with Id: {id} for active status");

            var task = _context.Tasks!.FirstOrDefault(c => c.Id == id);
            if (task != null) return task.IsActiv;

            _logger.LogInformation($"No Task with Id: {id} for active status");
            return false;
        }

        public async Task<bool> IsTaskActiveAsync(int id)
        {
            _logger.LogInformation($"Getting Task with Id: {id} for active status");

            var task = await _context.Tasks!.FirstOrDefaultAsync(c => c.Id == id);
            if (task != null) return task.IsActiv;

            _logger.LogInformation($"No Task with Id: {id} for active status");
            return false;
        }

        public bool IsTaskBreak(int id)
        {
            _logger.LogInformation($"Getting Task with Id: {id} to check if it is a task for a break");

            var task = _context.Tasks!.FirstOrDefault(c => c.Id == id);
            if (task != null) return task.IsActiv;

            _logger.LogInformation($"No Task with Id: {id} to check if it is a task for a break");
            return false;
        }

        public async Task<bool> IsTaskBreakAsync(int id)
        {
            _logger.LogInformation($"Getting Task with Id: {id} to check if it is a task for a break");

            var task = await _context.Tasks!.FirstOrDefaultAsync(c => c.Id == id);
            if (task != null) return task.IsActiv;

            _logger.LogInformation($"No Task with Id: {id} to check if it is a task for a break");
            return false;
        }

        public Task? GetTaskById(int Id)
        {
            _logger.LogInformation($"Getting a Task for Id: {Id}");

            var task = _context.Tasks!
                .FirstOrDefault(c => c.Id == Id);

            if (task == null) return null;

            _logger.LogInformation($"Task data has been successfully retrieved from the database. Task TaskName: {task.TaskName}");

            return task;
        }

        public async Task<Task?> GetTaskByIdAsync(int id)
        {
            _logger.LogInformation($"Getting a Task for Id: {id}");

            var result = await _context.Tasks!
                .FirstOrDefaultAsync(c => c.Id == id);

            if (result == null) return null;

            _logger.LogInformation($"Task data has been successfully retrieved from the database. Task TaskName: {result.TaskName}");

            return result;
        }

        public bool SaveChanges()
        {
            _logger.LogInformation($"Saving changes in context.");

            try
            {
                _context.SaveChanges();
                _logger.LogInformation($"Changes in context saved succesfully!");
                return true;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Changes in context could NOT be saved succesfully!");
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Saving changes in context.");

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Changes in context saved succesfully!");
                return true;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Changes in context could NOT be saved succesfully!");
                return false;
            }
        }
    }
}
