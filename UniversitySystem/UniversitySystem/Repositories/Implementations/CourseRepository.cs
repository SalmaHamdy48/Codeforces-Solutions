using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Repositories.Implementations
{
    public class CourseRepository(ApplicationDbContext context) : GenericRepository<Course>(context), ICourseRepository;
}