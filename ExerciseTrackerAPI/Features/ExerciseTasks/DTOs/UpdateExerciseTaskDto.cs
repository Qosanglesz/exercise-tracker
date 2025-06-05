namespace ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;

public abstract class UpdateExerciseTaskDto
{
    public string? Name { get; set; }
    public int? NumberOfSet { get; set; }
    public double? Weight { get; set; }
}