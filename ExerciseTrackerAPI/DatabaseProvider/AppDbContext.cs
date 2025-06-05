using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.DatabaseProvider;

public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) {}
    public DbSet<Task> Tasks { get; set; }
}