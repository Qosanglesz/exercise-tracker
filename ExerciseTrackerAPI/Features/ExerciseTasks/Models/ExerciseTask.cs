namespace ExerciseTrackerAPI.Features.ExerciseTasks.Models;

using System.ComponentModel.DataAnnotations;

public class ExerciseTask
{
    // Attributes Declaration zone
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "You must enter a task name")]
    [MaxLength(50, ErrorMessage = "The task name cannot exceed 50 characters")]
    public string Name { get; set; } = string.Empty;
    
    public int NumberOfSet {get; set;}
    public double Weight { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}