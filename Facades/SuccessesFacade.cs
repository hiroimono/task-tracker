using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using task_tracker.Hub;
using task_tracker.Interfaces;
using task_tracker.Models;

namespace task_tracker.Facades
{
    public class SuccessesFacade : ISuccessesFacade
    {
        private readonly ILogger<SuccessesFacade> _logger;
        private readonly Context _context;
        private IHubContext<SuccessHub, ISuccessHubClient> messageHub;

        public SuccessesFacade(
            Context context,
            ILogger<SuccessesFacade> logger,
            IHubContext<SuccessHub, ISuccessHubClient> _messageHub)
        {
            messageHub = _messageHub;
            _logger = logger;
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public Success? AddSuccess(int taskId, int userId)
        {
            _logger.LogInformation($"Adding a new Success for TaskId: {taskId} and UserId: {userId}");

            var newSuccessToAdd = new Success { TaskId = taskId, UserId = userId };

            var newSuccess = _context.Successes!.Add(newSuccessToAdd);
            _context.SaveChanges();

            return GetSuccessById(newSuccess.Entity.Id);
        }

        public async Task<Success?> AddSuccessAsync(int taskId, int userId)
        {
            _logger.LogInformation($"Adding a new Success for TaskId: {taskId} and UserId: {userId}");

            var newSuccessToAdd = new Success { TaskId = taskId, UserId = userId };

            var newSuccess = await _context.Successes!.AddAsync(newSuccessToAdd);
            await _context.SaveChangesAsync();

            return await GetSuccessByIdAsync(newSuccess.Entity.Id);
        }

        public bool AddSuccessesByTaskId(int taskId)
        {
            _logger.LogInformation("AddSuccessesByTaskId is started");

            var users = _context.Users!.ToArray();
            try
            {
                if (users.Length > 0)
                {
                    foreach (var user in users)
                    {
                        AddSuccess(taskId, user.Id);
                    }
                }
                _logger.LogInformation($"AddSuccessesByTaskId is completed successfully. All Successes were added for a task with TaskId: {taskId}");
                return true;
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was an ERROR while adding successes for a task", Ex.Message);
                return false;
            }
        }

        public async Task<bool> AddSuccessesByTaskIdAsync(int taskId)
        {
            _logger.LogInformation("AddSuccessesByTaskId is started");

            var users = await _context.Users!.ToArrayAsync();
            try
            {
                if (users.Length > 0)
                {
                    foreach (var user in users)
                    {
                        await AddSuccessAsync(taskId, user.Id);
                    }
                }
                _logger.LogInformation($"AddSuccessesByTaskId is completed successfully. All Successes were added for a task with TaskId: {taskId}");
                return true;
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was an ERROR while adding successes for a task", Ex.Message);
                return false;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} from the context.");
            _context.Remove(entity);
        }

        public bool DeleteSuccess(int id)
        {
            _logger.LogInformation($"Getting a Success for Id: {id}");

            var success = _context.Successes!
                .FirstOrDefault(c => c.Id == id);

            if (success == null) return false;

            _context.Successes!.Remove(success);

            _context.SaveChanges();

            _logger.LogInformation($"Success data for TaskId: {success.TaskId} and UserId: {success.UserId} has been successfully deleted from the database.");

            return true;
        }

        public async Task<bool> DeleteSuccessAsync(int id)
        {
            _logger.LogInformation($"Getting a Success for Id: {id}");

            var success = await _context.Successes!
                .FirstOrDefaultAsync(c => c.Id == id);

            if (success == null) return false;

            _context.Successes!.Remove(success);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Success data for TaskId: {success.TaskId} and UserId: {success.UserId} has been successfully deleted from the database.");

            return true;
        }

        public bool DeleteSuccessesByTaskId(int taskId)
        {
            _logger.LogInformation("DeleteSuccessesByTaskId is started");

            var successes = _context.Successes!
                .Where(success => success.TaskId == taskId)
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArray();
            try
            {
                if (successes.Length > 0)
                {
                    foreach (var success in successes)
                    {
                        DeleteSuccess(success.Id);
                    }
                }
                _logger.LogInformation($"DeleteSuccessesByTaskId is completed successfully. All Successes were added for a task with TaskId: {taskId}");
                return true;
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was an ERROR while adding successes for a task", Ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSuccessesByTaskIdAsync(int taskId)
        {
            _logger.LogInformation("DeleteSuccessesByTaskId is started");

            var successes = await _context.Successes!
                .Where(success => success.TaskId == taskId)
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArrayAsync();
            try
            {
                if (successes.Length > 0)
                {
                    foreach (var success in successes)
                    {
                        await DeleteSuccessAsync(success.Id);
                    }
                }
                _logger.LogInformation($"DeleteSuccessesByTaskId is completed successfully. All Successes were added for a task with TaskId: {taskId}");
                return true;
            }
            catch (Exception Ex)
            {
                _logger.LogError("There was an ERROR while adding successes for a task", Ex.Message);
                return false;
            }
        }

        public Success? EditSuccess(Success success)
        {
            _logger.LogInformation($"Editing a Success with Id: {success.Id}");

            if (success.User == null)
            {
                var user = _context.Users!.FirstOrDefault(u => u.Id == success.UserId);
                success.User = user!;
            }

            if (success.Task == null)
            {
                var task = _context.Tasks!.FirstOrDefault(t => t.Id == success.TaskId);
                success.Task = task!;
            }

            var checkUser = _context.Users!.Any(u => u.Id == success.UserId) && _context.Users!.Any(u => u.Id == success.User.Id);
            var checkTask = _context.Tasks!.Any(u => u.Id == success.TaskId) && _context.Tasks!.Any(u => u.Id == success.Task.Id);

            if (!checkUser || !checkTask)
            {
                _logger.LogError($"User or Task in Success with Id: {success.Id} does NOT place in DB!. \nPlease check the User and Task data in Success object!");
                new InvalidDataException();
            }

            _context.Entry(success).State = EntityState.Modified;

            _context.SaveChanges();

            var editedSuccess = _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == success.Id);

            if (editedSuccess != null)
            {
                if (editedSuccess.User.Id != editedSuccess.UserId)
                {
                    var user = _context.Users!.FirstOrDefault(u => u.Id == editedSuccess.UserId);
                    editedSuccess.User = user!;
                }

                if (editedSuccess.Task.Id != editedSuccess.TaskId)
                {
                    var task = _context.Tasks!.FirstOrDefault(t => t.Id == editedSuccess.TaskId);
                    editedSuccess.Task = task!;
                }
            }

            _logger.LogInformation($"Success with Id: {success.Id} was edited succesfully!");

            return editedSuccess;
        }

        public async Task<Success?> EditSuccessAsync(Success success)
        {
            _logger.LogInformation($"Editing a Success with Id: {success.Id}");

            if (success.User == null)
            {
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == success.UserId);
                success.User = user!;
            }

            if (success.Task == null)
            {
                var task = await _context.Tasks!.FirstOrDefaultAsync(t => t.Id == success.TaskId);
                success.Task = task!;
            }

            var checkUser = await _context.Users!.AnyAsync(u => u.Id == success.UserId) && await _context.Users!.AnyAsync(u => u.Id == success.User.Id);
            var checkTask = await _context.Tasks!.AnyAsync(u => u.Id == success.TaskId) && await _context.Tasks!.AnyAsync(u => u.Id == success.Task.Id);

            if (!checkUser || !checkTask)
            {
                _logger.LogError($"User or Task in Success with Id: {success.Id} does NOT place in DB!. \nPlease check the User and Task data in Success object!");
                new InvalidDataException();
            }

            _context.Entry(success).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var editedSuccess = await _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == success.Id);

            if (editedSuccess != null)
            {
                if (editedSuccess.User.Id != editedSuccess.UserId)
                {
                    var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == editedSuccess.UserId);
                    editedSuccess.User = user!;
                }

                if (editedSuccess.Task.Id != editedSuccess.TaskId)
                {
                    var task = await _context.Tasks!.FirstOrDefaultAsync(t => t.Id == editedSuccess.TaskId);
                    editedSuccess.Task = task!;
                }
            }
            _logger.LogInformation($"Success with Id: {success.Id} was edited succesfully!");

            return editedSuccess;
        }

        public Success[] GetAllSuccesses()
        {
            _logger.LogInformation($"Getting all Successes");

            var successes = _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArray();

            return successes;
        }

        public async Task<Success[]> GetAllSuccessesAsync()
        {
            _logger.LogInformation($"Getting all Successes");

            var successes = await _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArrayAsync();

            return successes;
        }

        public System.Threading.Tasks.Task GetAllSuccessesWithHub()
        {
            _logger.LogInformation($"Getting all Successes with Hub");

            var successes = _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArray();

            return messageHub.Clients.All.SendSuccessesToUser(successes);
        }

        public Success[] GetAllSuccessesByTaskId(int taskId)
        {
            _logger.LogInformation($"Getting all Successes by Task Id: {taskId}");

            var successes = _context.Successes!
                .Where(success => success.TaskId == taskId)
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArray();

            return successes;
        }

        public async Task<Success[]> GetAllSuccessesByTaskIdAsync(int taskId)
        {
            _logger.LogInformation($"Getting all Successes by Task Id: {taskId}");

            var successes = await _context.Successes!
                .Where(success => success.TaskId == taskId)
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArrayAsync();

            return successes;
        }

        public Success[] GetAllSuccessesByUserId(int userId)
        {
            _logger.LogInformation($"Getting all Successes by User Id: {userId}");

            var successes = _context.Successes!
                .Where(success => success.UserId == userId)
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArray();

            return successes;
        }

        public async Task<Success[]> GetAllSuccessesByUserIdAsync(int userId)
        {
            _logger.LogInformation($"Getting all Successes by User Id {userId}");

            var successes = await _context.Successes!
                .Where(success => success.UserId == userId)
                .Include(c => c.Task)
                .Include(c => c.User)
                .ToArrayAsync();

            return successes;
        }

        public Success? GetSuccessById(int id)
        {
            _logger.LogInformation($"Getting a Success by Id: {id}");

            var success = _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefault(success => success.Id == id);

            return success;
        }

        public async Task<Success?> GetSuccessByIdAsync(int id)
        {
            _logger.LogInformation($"Getting a Success by Id: {id}");

            var success = await _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefaultAsync(success => success.Id == id);

            return success;
        }

        public bool IsSuccessDoneById(int id)
        {
            _logger.LogInformation($"Getting a Success by Id: {id} to check it if it is done!");

            var success = _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefault(success => success.Id == id);

            _logger.LogInformation($"Success by Id: {id} is {(success!.IsDone ? "done!" : "NOT done!")} ");

            return success!.IsDone;
        }

        public async Task<bool> IsSuccessDoneByIdAsync(int id)
        {
            _logger.LogInformation($"Getting a Success by Id: {id} to check it if it is done!");

            var success = await _context.Successes!
                .Include(c => c.Task)
                .Include(c => c.User)
                .FirstOrDefaultAsync(success => success.Id == id);

            _logger.LogInformation($"Success by Id: {id} is {(success!.IsDone ? "done!" : "NOT done!")} ");

            return success!.IsDone;
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
