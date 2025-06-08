using AutoMapper;
using ExerciseTrackerAPI.DatabaseProvider;
using ExerciseTrackerAPI.Features.Users.DTOs;
using ExerciseTrackerAPI.Features.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User> CheckLogin(UserLoginDto userLoginDto)
    {
        // find user by username
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Username == userLoginDto.Username);

        if (user == null)
            throw new Exception("User not found");
        
        var verificationResult = _hasher.VerifyHashedPassword(user, user.Password, userLoginDto.Password);

        if (verificationResult != PasswordVerificationResult.Success)
        {
            throw new Exception("Invalid password");
        }

        return user;
    }
}