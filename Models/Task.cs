namespace task_tracker.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = null!;
        public int? Duration { get; set; }
        public bool IsActiv { get; set; } = false;
        public bool IsBreak { get; set; } = false;
    }
}
