using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.DatabaseProvider;

public class AppContext: DbContext {
    public AppContext(DbContextOptions<AppContext> option) : base(option) {}
    public DbSet<Task> Tasks { get; set; }
}