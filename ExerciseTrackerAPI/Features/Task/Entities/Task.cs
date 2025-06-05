using System.ComponentModel.DataAnnotations;

namespace ExerciseTrackerAPI.Features.Task.Entities;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int NumberOfSet {get; set;}
    public double Weight { get; set; }
    public DateTime Created { get; set; }
}