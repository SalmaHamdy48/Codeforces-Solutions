using AutoMapper;

using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Features.Course.Command.Handlers;

public class UpdateCourseHandler(ICourseRepository repo, IMapper mapper)
{
    public async Task<UniversitySystem.Models.Course> Handle(UpdateCourseDto dto)
    {
        var course = await repo.GetByIdAsync(dto.Id);
        if (course == null)
            throw new Exception("Course not found");

        mapper.Map(dto, course);
        await repo.UpdateAsync(course);

        return course;
    }
}