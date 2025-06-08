using AutoMapper;
using ExerciseTrackerAPI.DatabaseProvider;
using ExerciseTrackerAPI.Features.Users.DTOs;
using ExerciseTrackerAPI.Features.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace ExerciseTrackerAPI.Features.Users.Services;

public class UserService: IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<User> _hasher = new();
    
    // Constructor zone
    public UserService(AppDbContext context, IMapper mapper)
    {
        this._mapper = mapper;
        this._context = context;
    }
    
    public async Task<User> Create(CreateUserDto createUserDto)
    {
        var user = new User();
        this._mapper.Map(createUserDto, user);
        user.Password = _hasher.HashPassword(user, user.Password);
        var result = this._context.Users.Add(user);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}