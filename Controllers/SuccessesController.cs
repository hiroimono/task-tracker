using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using task_tracker.Hub;

namespace task_tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuccessesController : Controller
    {
        private IHubContext<SuccessMessagesHub, ISuccessMessagesHubClient> messageHub;

        public SuccessesController(IHubContext<SuccessMessagesHub, ISuccessMessagesHubClient> _messageHub)
        {
            messageHub = _messageHub;
        }

        [HttpPost]
        [Route("")]
        public string GetAllSuccesses()
        {
            List<bool> successes = new List<bool>();
            successes.Add(true);
            successes.Add(false);
            successes.Add(true);

            messageHub.Clients.All.SendSuccessesToUser(successes);
            return "Successes sent successfully to all users!";
        }
    }
}
