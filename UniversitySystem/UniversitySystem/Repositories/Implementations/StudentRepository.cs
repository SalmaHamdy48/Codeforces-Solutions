
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
        
        public async Task<IEnumerable<Student>> GetAllAsync()
            => await base.GetAllAsync();

        public async Task<Student?> GetByIdAsync(int id)
            => await base.GetByIdAsync(id);
        
        public async Task<Student?> GetByIdAsync(int id, CancellationToken ct = default)
            => await base.GetByIdAsync(id, ct);
        public async Task<Student> AddAsync(Student student, CancellationToken cancellationToken = default)
            => await base.AddAsync(student, cancellationToken);

        public async Task<Student> UpdateAsync(Student student, CancellationToken cancellationToken = default)
            => await base.UpdateAsync(student, cancellationToken);

        public async Task DeleteAsync(Student student, CancellationToken cancellationToken = default)
            => await base.DeleteAsync(student, cancellationToken);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await base.SaveChangesAsync(cancellationToken);
        
        public async Task<Student?> GetSingleAsync(ISpecification<Student> spec, CancellationToken cancellationToken = default)
            => await base.GetSingleAsync(spec, cancellationToken);

        public async Task<IEnumerable<Student>> GetListAsync(ISpecification<Student> spec, CancellationToken cancellationToken)
            => await base.GetListAsync(spec, cancellationToken);

        public async Task<int> CountAsync(ISpecification<Student> spec, CancellationToken cancellationToken)
            => await base.CountAsync(spec, cancellationToken);

        public async Task<Student?> GetStudentWithCoursesAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var spec = new StudentWithCoursesSpecification(studentId);
            return await GetSingleAsync(spec, cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetStudentsByAgeRangeAsync(int minAge, int maxAge, CancellationToken cancellationToken = default)
        {
            var spec = new StudentByAgeRangeSpecification(minAge, maxAge);
            return await GetListAsync(spec, cancellationToken);
        }

        public async Task<int> GetStudentEnrollmentCountAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var spec = new StudentSpecification(studentId);
            var student = await GetSingleAsync(spec, cancellationToken);
            return student?.StudentCourses?.Count ?? 0;
        }

        public async Task<IEnumerable<Student>> GetStudentsWithMinimumCoursesAsync(int minCourses, CancellationToken cancellationToken = default)
        {
            var spec = new StudentWithMinimumCoursesSpecification(minCourses);
            return await GetListAsync(spec, cancellationToken);
        }
    }
}