using task_tracker.Models;
using Task = System.Threading.Tasks.Task;

namespace task_tracker.Interfaces
{
    public interface IUsersFacade
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        // GET
        User[] GetAllUsers();
        Task<User[]> GetAllUsersAsync();
        Task GetAllUsersWithHub();
        User? GetUserById(int Id);
        Task<User?> GetUserByIdAsync(int Id);
        string GetUserAvatar(int Id);
        Task<string> GetUserAvatarAsync(int Id);
        bool GetUserRole(int Id);
        Task<bool> GetUserRoleAsync(int Id);
        bool CheckUserExist(int id);
        Task<bool> CheckUserExistAsync(int id);

        // PUT
        User? AddUser(User user);
        Task<User?> AddUserAsync(User user);

        // PATCH
        User? EditUser(User user);
        Task<User?> EditUserAsync(User user);

        // DELETE
        bool DeleteUser(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
