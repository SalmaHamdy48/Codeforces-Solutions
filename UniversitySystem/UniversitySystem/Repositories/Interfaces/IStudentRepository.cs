// Repositories/Interfaces/IStudentRepository.cs
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
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task SaveChangesAsync();
        Task<Student?> GetSingleAsync(ISpecification<Student> spec);
        Task<IEnumerable<Student>> GetListAsync(ISpecification<Student> spec);
        Task<int> CountAsync(ISpecification<Student> spec);

        Task<Student?> GetStudentWithCoursesAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsByAgeRangeAsync(int minAge, int maxAge);
        Task<int> GetStudentEnrollmentCountAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsWithMinimumCoursesAsync(int minCourses);
    }
}