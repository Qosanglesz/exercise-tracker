using System.ComponentModel.DataAnnotations;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.Features.Users.Models;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    
    public List<ExerciseTask> ExerciseTasks { get; set; } = new List<ExerciseTask>();
}
