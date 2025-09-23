
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Collections.Generic;

namespace UniversitySystem.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByIdAsync(int id, CancellationToken ct = default);
            
        Task<Student> AddAsync(Student student, CancellationToken cancellationToken = default);
        Task<Student> UpdateAsync(Student student, CancellationToken cancellationToken = default);
        Task DeleteAsync(Student student, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Student?> GetSingleAsync(ISpecification<Student> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<Student>> GetListAsync(ISpecification<Student> spec, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<Student> spec, CancellationToken cancellationToken = default);
        Task<Student?> GetStudentWithCoursesAsync(int studentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Student>> GetStudentsByAgeRangeAsync(int minAge, int maxAge, CancellationToken cancellationToken = default);
        Task<int> GetStudentEnrollmentCountAsync(int studentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Student>> GetStudentsWithMinimumCoursesAsync(int minCourses, CancellationToken cancellationToken = default);
    }
}