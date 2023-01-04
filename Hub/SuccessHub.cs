using Microsoft.AspNetCore.SignalR;
using task_tracker.Models;
using Task = System.Threading.Tasks.Task;

namespace task_tracker.Hub
{
    public class SuccessHub : Hub<ISuccessHubClient>
    {
        public async Task SendSuccessesToUser(Success[] successes)
        {
            await Clients.All.SendSuccessesToUser(successes);
        }
    }
}
