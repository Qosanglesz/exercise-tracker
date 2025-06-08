using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;

namespace ExerciseTrackerAPI.Features.ExerciseTasks.Services;

public interface IExerciseService
{
    // CRUD Operation
    
    // Create
    Task<ExerciseTask> Create(CreateExerciseTaskDto createExerciseTaskDto, string userId);
    
    // Read
    Task<ExerciseTask> Get(Guid id, string userId);
    Task<List<ExerciseTask>> GetAll(string userId);
    
    //Update
    Task<ExerciseTask> Update(Guid id, UpdateExerciseTaskDto updateExerciseTaskDto, string userId);
    
    // Delete
    Task<bool> Delete(Guid id, string userId);
}