using System.ComponentModel.DataAnnotations;

namespace ExerciseTrackerAPI.Features.Users.DTOs;

public class UserLoginDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}