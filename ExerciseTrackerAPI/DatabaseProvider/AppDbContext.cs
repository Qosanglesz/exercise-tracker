using ExerciseTrackerAPI.Features.Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.DatabaseProvider;

public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) {}
    public DbSet<ExerciseTask> ExerciseTasks { get; set; }
}