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

    public async Task<ExerciseTask> Create(CreateExerciseTaskDto dto, string userId)
    {
        var task = this._mapper.Map<ExerciseTask>(dto);
        task.UserId = Guid.Parse(userId);

        var result = await this._context.ExerciseTasks.AddAsync(task);
        await this._context.SaveChangesAsync();
        return result.Entity;
    }


    public async Task<ExerciseTask> Get(Guid id, string userId)
    {
        var task = await this._context.ExerciseTasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == Guid.Parse(userId));

        if (task == null)
            throw new Exception("Task not found or access denied.");

        return task;
    }


    public async Task<List<ExerciseTask>> GetAll(string userId)
    {
        return await this._context.ExerciseTasks
            .Where(t => t.UserId == Guid.Parse(userId))
            .ToListAsync();
    }

    public async Task<ExerciseTask> Update(Guid id, UpdateExerciseTaskDto dto, string userId)
    {
        var task = await Get(id, userId); // Already checks ownership
        this._mapper.Map(dto, task);

        this._context.ExerciseTasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }


    public async Task<bool> Delete(Guid id, string userId)
    {
        var task = await Get(id, userId); // Already checks ownership
        this._context.ExerciseTasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}