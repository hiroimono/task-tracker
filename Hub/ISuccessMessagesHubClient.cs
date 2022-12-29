namespace task_tracker.Hub
{
    public interface ISuccessMessagesHubClient
    {
        Task SendSuccessesToUser(List<bool> message);
    }
}
