using Task = task_tracker.Models.Task;

namespace task_tracker.Interfaces
{
    public interface ITasksFacade
    {

        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        // GET
        Task[] GetAllTasks();
        Task<Task[]> GetAllTasksAsync();
        Task? GetTaskById(int Id);
        Task<Task?> GetTaskByIdAsync(int Id);
        int GetTaskDuration(int Id);
        Task<int> GetTaskDurationAsync(int Id);
        bool IsTaskActive(int Id);
        Task<bool> IsTaskActiveAsync(int Id);
        bool IsTaskBreak(int Id);
        Task<bool> IsTaskBreakAsync(int Id);
        bool CheckTaskExist(int id);
        Task<bool> CheckTaskExistAsync(int id);

        // PUT
        Task? AddTask(Task user);
        Task<Task?> AddTaskAsync(Task user);

        // PATCH
        Task? EditTask(Task user);
        Task<Task?> EditTaskAsync(Task user);

        // DELETE
        bool DeleteTask(int id);
        Task<bool> DeleteTaskAsync(int id);
    }
}
