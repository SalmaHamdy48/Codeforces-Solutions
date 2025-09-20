// Repositories/Interfaces/ICourseRepository.cs
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Collections.Generic;

namespace UniversitySystem.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        
        Task<IEnumerable<Course>> GetAllAsync(CancellationToken ct = default);
        Task<Course?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Course> AddAsync(Course course, CancellationToken ct = default); 
        Task<Course> UpdateAsync(Course course, CancellationToken ct = default); 
        Task DeleteAsync(Course course, CancellationToken ct = default); 
        Task<int> SaveChangesAsync(CancellationToken ct = default); 
        
        
        Task<Course?> GetSingleAsync(ISpecification<Course> spec, CancellationToken ct = default); 
        Task<IEnumerable<Course>> GetListAsync(ISpecification<Course> spec, CancellationToken ct = default); 
        Task<int> CountAsync(ISpecification<Course> spec, CancellationToken ct = default); 
        
        
        Task<Course?> GetCourseWithStudentsAsync(int courseId, CancellationToken ct = default); 
        Task<IEnumerable<Course>> GetCoursesByStudentAsync(int studentId, CancellationToken ct = default); 
        Task<int> GetCourseEnrollmentCountAsync(int courseId, CancellationToken ct = default); 
        Task<IEnumerable<Course>> GetCoursesWithMinimumStudentsAsync(int minStudents, CancellationToken ct = default); 
    }
}