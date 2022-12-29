namespace task_tracker.Models
{
    public class Success
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; } = null!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
