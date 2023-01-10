using Microsoft.AspNetCore.SignalR;
using task_tracker.Models;
using Task = System.Threading.Tasks.Task;

namespace task_tracker.Hub
{
    public class UserHub : Hub<IUserHubClient>
    {
        public async Task SendUsersToUser(User[] users)
        {
            await Clients.All.SendUsersToUser(users);
        }
    }
}
