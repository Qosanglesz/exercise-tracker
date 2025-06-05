using ExerciseTrackerAPI.DatabaseProvider;
using ExerciseTrackerAPI.Exceptions;
using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;

namespace ExerciseTrackerAPI.Features.ExerciseTasks.Services;

public class ExerciseTaskService : IExerciseService
{
    private readonly AppDbContext _context;

    public ExerciseTaskService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<ExerciseTask> Create(CreateExerciseTaskDto createExerciseTaskDto)
    {
        var exerciseTask = new ExerciseTask()
        {
            Name = createExerciseTaskDto.Name,
            NumberOfSet = createExerciseTaskDto.NumberOfSet,
            Weight = createExerciseTaskDto.Weight,
        };
        this._context.ExerciseTasks.Add(exerciseTask);
    }

public Task<ExerciseTask> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ExerciseTask>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}