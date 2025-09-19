
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Collections.Generic;

namespace UniversitySystem.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
        Task SaveChangesAsync();
        Task<Course?> GetSingleAsync(ISpecification<Course> spec);
        Task<IEnumerable<Course>> GetListAsync(ISpecification<Course> spec);
        Task<int> CountAsync(ISpecification<Course> spec);
        Task<Course?> GetCourseWithStudentsAsync(int courseId);
        Task<IEnumerable<Course>> GetCoursesByStudentAsync(int studentId);
        Task<int> GetCourseEnrollmentCountAsync(int courseId);
        Task<IEnumerable<Course>> GetCoursesWithMinimumStudentsAsync(int minStudents);
    }
}