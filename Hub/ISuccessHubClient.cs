using task_tracker.Models;
using Task = System.Threading.Tasks.Task;

namespace task_tracker.Hub
{
    public interface ISuccessHubClient
    {
        Task SendSuccessesToUser(Success[] successes);
    }
}
