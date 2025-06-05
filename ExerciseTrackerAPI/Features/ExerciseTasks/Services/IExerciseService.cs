using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;

namespace ExerciseTrackerAPI.Features.ExerciseTasks.Services;

public interface IExerciseService
{
    // CRUD Operation
    
    // Create
    Task<ExerciseTask> Create(CreateExerciseTaskDto createExerciseTaskDto);
    
    // Read
    Task<ExerciseTask> Get(int id);
    Task<List<ExerciseTask>> GetAll();
    
    // Delete
    Task<bool> Delete(int id);
}