using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExerciseTrackerAPI.Configuration;
using ExerciseTrackerAPI.Features.Users.DTOs;
using ExerciseTrackerAPI.Features.Users.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExerciseTrackerAPI.Features.Users.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _service;
    private readonly JwtSettings _jwtSettings;
    
    // Constructor zone
    public UserController(IUserService service, JwtSettings jwtSettings)
    {
        this._service = service;
        this._jwtSettings = jwtSettings;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.Create(createUserDto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var user = await _service.CheckLogin(userLoginDto);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this._jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", user.Id.ToString()),
                    new Claim("username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = this._jwtSettings.Issuer,
                Audience = this._jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Cookies
            Response.Cookies.Append("access_token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok("Logged in with cookie");
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
}