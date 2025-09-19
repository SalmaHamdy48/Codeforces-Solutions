// Repositories/Implementations/CourseRepository.cs
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Repositories.Implementations;
using UniversitySystem.Specifications;

namespace UniversitySystem.Repositories.Implementations
{
    public class CourseRepository(ApplicationDbContext context) 
        : GenericRepository<Course>(context), ICourseRepository
    {

        public async Task<IEnumerable<Course>> GetAllAsync()
            => await base.GetAllAsync();

        public async Task<Course?> GetByIdAsync(int id)
            => await base.GetByIdAsync(id);

        public async Task AddAsync(Course course)
            => await base.AddAsync(course);

        public async Task UpdateAsync(Course course)
            => await base.UpdateAsync(course);

        public async Task DeleteAsync(Course course)
            => await base.DeleteAsync(course);

        public async Task SaveChangesAsync()
            => await base.SaveChangesAsync();

        public async Task<Course?> GetSingleAsync(ISpecification<Course> spec)
            => await base.GetSingleAsync(spec);

        public async Task<IEnumerable<Course>> GetListAsync(ISpecification<Course> spec)
            => await base.GetListAsync(spec);

        public async Task<int> CountAsync(ISpecification<Course> spec)
            => await base.CountAsync(spec);


        public async Task<Course?> GetCourseWithStudentsAsync(int courseId)
        {
            var spec = new CourseWithStudentsSpecification(courseId);
            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentAsync(int studentId)
        {
            var spec = new CoursesByStudentSpecification(studentId);
            return await GetListAsync(spec);
        }

        public async Task<int> GetCourseEnrollmentCountAsync(int courseId)
        {
            var spec = new CourseSpecification(courseId);
            var course = await GetSingleAsync(spec);
            return course?.StudentCourses?.Count ?? 0;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithMinimumStudentsAsync(int minStudents)
        {
            var spec = new CoursesWithMinimumStudentsSpecification(minStudents);
            return await GetListAsync(spec);
        }
        
    }
}