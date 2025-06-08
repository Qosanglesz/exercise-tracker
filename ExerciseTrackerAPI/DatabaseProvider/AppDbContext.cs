using ExerciseTrackerAPI.Features.ExerciseTasks.Models;
using ExerciseTrackerAPI.Features.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.DatabaseProvider;

public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) {}
    public DbSet<ExerciseTask> ExerciseTasks { get; set; }
    public DbSet<User> Users { get; set; }
}