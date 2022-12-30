namespace task_tracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string Nickname { get; set; } = null!;
        public string? Avatar { get; set; }

    }
}
