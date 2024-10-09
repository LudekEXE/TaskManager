using Microsoft.EntityFrameworkCore;

namespace taskMeneger
{
    public class TaskMenegerDbContext(DbContextOptions<TaskMenegerDbContext> options) : DbContext(options)
    {
        // DbSet representing tasks in the database
        public DbSet<TaskInfo> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
                

            // Configuration of the Task entity
            modelBuilder.Entity<TaskInfo>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(e => e.Id); // Primary key
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100); // Max length for the title
                entity.Property(e => e.Description)
                    .HasMaxLength(500); // Optional description
                entity.Property(e => e.Deadline)
                    .IsRequired(); // Deadline is required
                entity.Property(e => e.Priority)
                    .IsRequired(); // Priority is required
            });
            base.OnModelCreating(modelBuilder);
        }
        
    }
}