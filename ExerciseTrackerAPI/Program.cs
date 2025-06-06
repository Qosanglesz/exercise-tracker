using ExerciseTrackerAPI.DatabaseProvider;
using ExerciseTrackerAPI.Features.ExerciseTasks.Services;
using ExerciseTrackerAPI.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// App's builder Zone
var builder = WebApplication.CreateBuilder(args);

// App Service registration zone

// Register Database to AppDbContext
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("database:ConnectionStrings")["docker-compose"]));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IExerciseService, ExerciseTaskService>();

// App Zone
var app = builder.Build();
app.MapControllers();
app.Run();