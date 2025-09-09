using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Features.Course.Command.Handlers;

public class DeleteCourseHandler(ICourseRepository repo)
{
    public async Task<bool> Handle(int id)
    {
        var course = await repo.GetByIdAsync(id);
        if (course == null)
            throw new Exception("Course not found");

        await repo.DeleteAsync(course);
        return true;
    }
}