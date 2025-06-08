using System.Security.Claims;
using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExerciseTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _service.Create(dto, userId!);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var guid = Guid.Parse(id);
            var result = await _service.Get(guid, userId!);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tasks = await _service.GetAll(userId!);
        return Ok(tasks);
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateExerciseTaskDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var guid = Guid.Parse(id);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await this._service.Update(guid, dto, userId!);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var guid = Guid.Parse(id);
            await _service.Delete(guid, userId!);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}