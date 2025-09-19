
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Repositories.Implementations;
using UniversitySystem.Specifications;

namespace UniversitySystem.Repositories.Implementations
{
    public class StudentRepository(ApplicationDbContext context) 
        : GenericRepository<Student>(context), IStudentRepository
    {
        // ✅ Basic CRUD - Delegate to GenericRepository
        public async Task<IEnumerable<Student>> GetAllAsync()
            => await base.GetAllAsync();

        public async Task<Student?> GetByIdAsync(int id)
            => await base.GetByIdAsync(id);

        public async Task AddAsync(Student student)
            => await base.AddAsync(student);

        public async Task UpdateAsync(Student student)
            => await base.UpdateAsync(student);

        public async Task DeleteAsync(Student student)
            => await base.DeleteAsync(student);

        public async Task SaveChangesAsync()
            => await base.SaveChangesAsync();

        // ✅ Specification-based methods - Delegate to GenericRepository
        public async Task<Student?> GetSingleAsync(ISpecification<Student> spec)
            => await base.GetSingleAsync(spec);

        public async Task<IEnumerable<Student>> GetListAsync(ISpecification<Student> spec)
            => await base.GetListAsync(spec);

        public async Task<int> CountAsync(ISpecification<Student> spec)
            => await base.CountAsync(spec);

        // ✅ Your custom methods
        public async Task<Student?> GetStudentWithCoursesAsync(int studentId)
        {
            var spec = new StudentWithCoursesSpecification(studentId);
            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<Student>> GetStudentsByAgeRangeAsync(int minAge, int maxAge)
        {
            var spec = new StudentByAgeRangeSpecification(minAge, maxAge);
            return await GetListAsync(spec);
        }

        public async Task<int> GetStudentEnrollmentCountAsync(int studentId)
        {
            var spec = new StudentSpecification(studentId);
            var student = await GetSingleAsync(spec);
            return student?.StudentCourses?.Count ?? 0;
        }

        public async Task<IEnumerable<Student>> GetStudentsWithMinimumCoursesAsync(int minCourses)
        {
            var spec = new StudentWithMinimumCoursesSpecification(minCourses);
            return await GetListAsync(spec);
        }
    }
}