using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using task_tracker.Hub;
using task_tracker.Interfaces;
using task_tracker.Models;

namespace task_tracker.Facades
{
    public class UsersFacade : IUsersFacade
    {
        private readonly ILogger<UsersFacade> _logger;
        private readonly Context _context;
        private IHubContext<UserHub, IUserHubClient> messageHub;

        public UsersFacade(
            Context context,
            ILogger<UsersFacade> logger,
            IHubContext<UserHub, IUserHubClient> _messageHub)
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

        public User? AddUser(User user)
        {
            _logger.LogInformation($"Getting a User for Id: {user.Id}; NickName: {user.Nickname}");

            var newUser = _context.Users!.Add(user);
            _context.SaveChanges();

            return GetUserById(newUser.Entity.Id);
        }

        public async Task<User?> AddUserAsync(User user)
        {
            _logger.LogInformation($"Getting a User for Id: {user.Id}; NickName: {user.Nickname}");

            var newUser = _context.Users!.Add(user);
            await _context.SaveChangesAsync();

            return await GetUserByIdAsync(newUser.Entity.Id);
        }

        public bool CheckUserExist(int id)
        {
            return _context.Users!.Any(c => c.Id == id);
        }

        public async Task<bool> CheckUserExistAsync(int id)
        {
            return await _context.Users!.AnyAsync(c => c.Id == id);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} from the context.");
            _context.Remove(entity);
        }

        public bool DeleteUser(int id)
        {
            _logger.LogInformation($"Getting a User for Id: {id}");

            var user = _context.Users!
                .FirstOrDefault(c => c.Id == id);

            if (user == null) return false;

            _context.Users!.Remove(user);

            _context.SaveChanges();

            _logger.LogInformation($"User data has been successfully deleted from the database. User Nickname: {user.Nickname}");

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            _logger.LogInformation($"Getting a User for Id: {id}");

            using (var ctx = new Context())
            {
                try
                {
                    var user = new User { Id = id };
                    ctx.Users!.Attach(user);
                    ctx.Users!.Remove(user);
                    await ctx.SaveChangesAsync();
                    _logger.LogInformation($"User data has been successfully deleted from the database. User Nickname: {user.Nickname}");
                    return true;
                }
                catch (Exception)
                {
                    _logger.LogInformation($"User data has NOT been successfully deleted from the database for the User Id: {id}");
                    return false;
                }
            }
        }

        public User? EditUser(User user)
        {
            _logger.LogInformation($"Editing a User for Nickname: {user.Nickname}");

            _context.Entry(user).State = EntityState.Modified;

            _context.SaveChanges();

            var editedUser = _context.Users!.FirstOrDefault(c => c.Id == user.Id);
            _logger.LogInformation($"User for Nickname: {user.Nickname} was edited succesfully!");

            return editedUser;
        }

        public async Task<User?> EditUserAsync(User user)
        {
            _logger.LogInformation($"Editing a User for Nickname: {user.Nickname}");

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var editedUser = await _context.Users!.FirstOrDefaultAsync(c => c.Id == user.Id);
            _logger.LogInformation($"User was edited succesfully!");

            return editedUser;
        }

        public User[] GetAllUsers()
        {
            _logger.LogInformation($"Getting all Users");

            IQueryable<User> query = _context.Users!;

            // Order Users by Id Desc
            query = query.OrderByDescending(c => c.Id);

            return query.ToArray();
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            _logger.LogInformation($"Getting all Users");

            IQueryable<User> query = _context.Users!;

            // Order Users by Id Desc
            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public System.Threading.Tasks.Task GetAllUsersWithHub()
        {
            _logger.LogInformation($"Getting all Users with Hub");

            var users = _context.Users!.OrderByDescending(c => c.Id).ToArray();

            return messageHub.Clients.All.SendUsersToUser(users);
        }

        public string GetUserAvatar(int Id)
        {
            _logger.LogInformation($"Getting Avatar for a User with Id: {Id}");

            var user = _context.Users!
                .FirstOrDefault(c => c.Id == Id);

            _logger.LogInformation($"User Avatar has been successfully retrieved from the database. User Nickname: {user!.Nickname}");

            return user.Nickname;
        }

        public async Task<string> GetUserAvatarAsync(int Id)
        {
            _logger.LogInformation($"Getting Avatar for a User with Id: {Id}");

            var user = await _context.Users!
                .FirstOrDefaultAsync(c => c.Id == Id);

            _logger.LogInformation($"User Avatar has been successfully retrieved from the database. User Nickname: {user!.Nickname} and User Avatar: {user!.Avatar}");

            return user.Avatar!;
        }

        public User? GetUserById(int Id)
        {
            _logger.LogInformation($"Getting a User for Id: {Id}");

            var user = _context.Users!
                .FirstOrDefault(c => c.Id == Id);

            if (user == null) return null;

            _logger.LogInformation($"User data has been successfully retrieved from the database. User Nickname: {user.Nickname}");

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int Id)
        {
            _logger.LogInformation($"Getting a User for Id: {Id}");

            var result = await _context.Users!
                .FirstOrDefaultAsync();

            if (result == null) return null;

            _logger.LogInformation($"User data has been successfully retrieved from the database. User Nickname: {result.Nickname}");

            return result;
        }

        public bool GetUserRole(int Id)
        {
            _logger.LogInformation($"Getting Role for a User with Id: {Id}");

            var user = _context.Users!
                .FirstOrDefault(c => c.Id == Id);

            _logger.LogInformation($"User Role has been successfully retrieved from the database. User has {(user!.IsAdmin ? "ADMIN" : "not ADMIN")} right!");

            return user.IsAdmin;
        }

        public async Task<bool> GetUserRoleAsync(int Id)
        {
            _logger.LogInformation($"Getting Role for a User with Id: {Id}");

            var user = await _context.Users!
                .FirstOrDefaultAsync(c => c.Id == Id);

            _logger.LogInformation($"User Role has been successfully retrieved from the database. User has {(user!.IsAdmin ? "ADMIN" : "not ADMIN")} right!");

            return user.IsAdmin;
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
