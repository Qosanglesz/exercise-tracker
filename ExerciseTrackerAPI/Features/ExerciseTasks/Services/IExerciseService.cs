using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;

namespace ExerciseTrackerAPI.Features.ExerciseTasks.Services;

public interface IExerciseService
{
    // CRUD Operation
    
    // Create
    Task<ExerciseTask> Create(CreateExerciseTaskDto createExerciseTaskDto);
    
    // Read
    Task<ExerciseTask> Get(Guid id);
    Task<List<ExerciseTask>> GetAll();
    
    //Update
    Task<ExerciseTask> Update(Guid id, UpdateExerciseTaskDto updateExerciseTaskDto);
    
    // Delete
    Task<bool> Delete(Guid id);
}