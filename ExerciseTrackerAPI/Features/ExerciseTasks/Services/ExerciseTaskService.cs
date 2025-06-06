using AutoMapper;
using ExerciseTrackerAPI.DatabaseProvider;
using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.Features.ExerciseTasks.Services;

public class ExerciseTaskService : IExerciseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ExerciseTaskService(AppDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<ExerciseTask> Create(CreateExerciseTaskDto createExerciseTaskDto)
    {
        var exerciseTask = new ExerciseTask();
        this._mapper.Map(createExerciseTaskDto, exerciseTask);
        var result = await this._context.ExerciseTasks.AddAsync(exerciseTask);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

public async Task<ExerciseTask> Get(Guid id)
    {
        var exerciseTask = await this._context.ExerciseTasks.FindAsync(id);
        if (exerciseTask == null)
        {
            throw new Exception($"No exercise task found with id {id}");
        }
        return exerciseTask;
    }

    public async Task<List<ExerciseTask>> GetAll()
    {
        var exerciseTasks = await this._context.ExerciseTasks.ToListAsync();
        return exerciseTasks;
    }

    public async Task<ExerciseTask> Update(Guid id, UpdateExerciseTaskDto updateExerciseTaskDto)
    {
        var exerciseTask = await this.Get(id);
        this._mapper.Map(updateExerciseTaskDto, exerciseTask);
        var result = this._context.ExerciseTasks.Update(exerciseTask);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var exerciseTask = await this.Get(id);
        try
        {
            this._context.Remove(exerciseTask);
            await this._context.SaveChangesAsync();
            return  true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting exercise task with id {id}", ex);
        }
    }
}