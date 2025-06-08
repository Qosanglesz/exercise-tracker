using ExerciseTrackerAPI.Features.Users.DTOs;
using ExerciseTrackerAPI.Features.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTrackerAPI.Features.Users.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _service;
    
    // Constructor zone
    public UserController(IUserService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.Create(createUserDto);
        return Ok(result);
    }
}