using ExerciseTrackerAPI.DatabaseProvider;
using ExerciseTrackerAPI.Features.ExerciseTasks.Services;
using ExerciseTrackerAPI.Features.Users.Services;
using ExerciseTrackerAPI.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExerciseTrackerAPI.Configuration;
using Microsoft.Extensions.Options;

// App's builder Zone
var builder = WebApplication.CreateBuilder(args);

// App Service registration zone
builder.Services.AddControllers();

// Register Database to AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("database:ConnectionStrings")["docker-compose"]));

// Service registration zone
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IExerciseService, ExerciseTaskService>();
builder.Services.AddScoped<IUserService, UserService>();

// Authentication zone

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration.GetSection("JwtSettings")["Issuer"],
            ValidAudience = builder.Configuration.GetSection("JwtSettings")["Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings")["SecretKey"]!))
        };

        // 2. Use JWT from Cookie instead of Authorization Header
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Cookies["access_token"]; // Cookie name is access_token
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<JwtSettings>>().Value);

// App Zone
var app = builder.Build();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();