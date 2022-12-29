using Microsoft.AspNetCore.SignalR;

namespace task_tracker.Hub
{
    public class SuccessMessagesHub : Hub<ISuccessMessagesHubClient>
    {
        public async Task SendSuccessesToUser(List<bool> message)
        {
            await Clients.All.SendSuccessesToUser(message);
        }
    }
}
