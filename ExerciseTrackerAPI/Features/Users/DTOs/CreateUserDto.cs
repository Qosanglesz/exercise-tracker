using System.ComponentModel.DataAnnotations;

namespace ExerciseTrackerAPI.Features.Users.DTOs;

public class CreateUserDto
{
    [Required]
    [Length(0, 50, ErrorMessage = "The username cannot exceed 50 characters")]
    public string Username { get; set; }
    
    [Required]
    [Length(0, 50, ErrorMessage = "The password cannot exceed 50 characters")]
    public string Password { get; set; }
}