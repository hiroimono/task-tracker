using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace task_tracker.Models
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Task>? Tasks { get; set; }
        public virtual DbSet<Success>? Successes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .Property(b => b.Nickname)
            .IsRequired();

            modelBuilder.Entity<Task>()
            .Property(b => b.TaskName)
            .IsRequired();

            modelBuilder.Entity<Success>();
        }
    }
}
