using AutoMapper;
using ExerciseTrackerAPI.Features.ExerciseTasks.DTOs;
using ExerciseTrackerAPI.Features.ExerciseTasks.Models;
using ExerciseTrackerAPI.Features.Users.DTOs;
using ExerciseTrackerAPI.Features.Users.Models;

namespace ExerciseTrackerAPI.Mappers;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<CreateExerciseTaskDto, ExerciseTask>();
        CreateMap<UpdateExerciseTaskDto, ExerciseTask>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateUserDto, User>();
    }
}
