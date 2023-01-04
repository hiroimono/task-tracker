using task_tracker.Models;
using Task = System.Threading.Tasks.Task;

namespace task_tracker.Interfaces
{
    public interface ISuccessesFacade
    {

        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        // GET
        Success[] GetAllSuccesses();
        Task<Success[]> GetAllSuccessesAsync();
        Task GetAllSuccessesWithHub();

        Success? GetSuccessById(int Id);
        Task<Success?> GetSuccessByIdAsync(int Id);
        Success[] GetAllSuccessesByUserId(int Id);
        Task<Success[]> GetAllSuccessesByUserIdAsync(int Id);
        Success[] GetAllSuccessesByTaskId(int Id);
        Task<Success[]> GetAllSuccessesByTaskIdAsync(int Id);
        bool IsSuccessDoneById(int Id);
        Task<bool> IsSuccessDoneByIdAsync(int Id);

        // PUT
        Success? AddSuccess(int taskId, int userId);
        Task<Success?> AddSuccessAsync(int taskId, int userId);
        bool AddSuccessesByTaskId(int id);
        Task<bool> AddSuccessesByTaskIdAsync(int id);

        // PATCH
        Success? EditSuccess(Success success);
        Task<Success?> EditSuccessAsync(Success success);

        // DELETE
        bool DeleteSuccess(int id);
        Task<bool> DeleteSuccessAsync(int id);
        bool DeleteSuccessesByTaskId(int id);
        Task<bool> DeleteSuccessesByTaskIdAsync(int id);
    }
}
