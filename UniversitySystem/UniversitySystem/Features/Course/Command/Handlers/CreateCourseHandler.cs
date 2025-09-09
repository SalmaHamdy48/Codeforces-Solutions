using AutoMapper;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Features.Course.Command.Handlers;

public class CreateCourseHandler(ICourseRepository repo, IMapper mapper)
{
    public async Task<UniversitySystem.Models.Course> Handle(CreateCourseDto dto)
    {
        var course = mapper.Map<UniversitySystem.Models.Course>(dto);
        await repo.AddAsync(course);
        return course;
    }
}