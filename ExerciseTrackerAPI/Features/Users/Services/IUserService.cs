using ExerciseTrackerAPI.Features.Users.DTOs;
using ExerciseTrackerAPI.Features.Users.Models;

namespace ExerciseTrackerAPI.Features.Users.Services;

public interface IUserService
{
    public Task<User> Create(CreateUserDto createUserDto);
    public Task<User> CheckLogin(UserLoginDto userLoginDto);
}