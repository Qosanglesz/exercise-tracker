using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTrackerAPI.Features.ExerciseTasks.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ExerciseTaskController: ControllerBase
{
    private readonly IExerciseService _service;

    public ExerciseTaskController(IExerciseService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExerciseTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.Create(dto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var guid = Guid.Parse(id);
            var result = await _service.Get(guid);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _service.GetAll();
        return Ok(tasks);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateExerciseTaskDto dto)
    {
        var guid = Guid.Parse(id);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await this._service.Update(guid, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var guid = Guid.Parse(id);
            await _service.Delete(guid);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}