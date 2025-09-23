
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;

namespace UniversitySystem.Repositories.Implementations
{
    public class CourseRepository(ApplicationDbContext context) 
        : GenericRepository<Course>(context), ICourseRepository
    {
        public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken ct = default)
            => await base.GetAllAsync(ct);

        public async Task<Course?> GetByCodeAsync(string code, CancellationToken ct = default)
            => await base.GetByCodeAsync(code, ct);

        public async Task<Course?> GetByIdAsync(int id, CancellationToken ct = default)
            => await base.GetByIdAsync(id, ct);

        public async Task<Course> AddAsync(Course course, CancellationToken ct = default)
            => await base.AddAsync(course, ct);

        public async Task<Course> UpdateAsync(Course course, CancellationToken ct = default)
            => await base.UpdateAsync(course, ct);

        public async Task DeleteAsync(Course course, CancellationToken ct = default)
            => await base.DeleteAsync(course, ct);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await base.SaveChangesAsync(ct);

        public async Task<Course?> GetSingleAsync(ISpecification<Course> spec, CancellationToken ct = default)
            => await base.GetSingleAsync(spec, ct);

        public async Task<IEnumerable<Course>> GetListAsync(ISpecification<Course> spec, CancellationToken ct = default)
            => await base.GetListAsync(spec, ct);

        public async Task<int> CountAsync(ISpecification<Course> spec, CancellationToken ct = default)
            => await base.CountAsync(spec, ct);

        public async Task<Course?> GetCourseWithStudentsAsync(int courseId, CancellationToken ct = default)
        {
            var spec = new CourseWithStudentsSpecification(courseId);
            return await GetSingleAsync(spec, ct);
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentAsync(int studentId, CancellationToken ct = default)
        {
            var spec = new CoursesByStudentSpecification(studentId);
            return await GetListAsync(spec, ct);
        }

        public async Task<int> GetCourseEnrollmentCountAsync(int courseId, CancellationToken ct = default)
        {
            var spec = new CourseSpecification(courseId);
            var course = await GetSingleAsync(spec, ct);
            return course?.StudentCourses?.Count ?? 0;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithMinimumStudentsAsync(int minStudents, CancellationToken ct = default)
        {
            var spec = new CoursesWithMinimumStudentsSpecification(minStudents);
            return await GetListAsync(spec, ct);
        }
    }
}
