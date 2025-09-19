// Specifications/StudentSpecifications.cs
using UniversitySystem.Models;
using UniversitySystem.Specifications;

namespace UniversitySystem.Specifications
{

    public class StudentSpecification : BaseSpecification<Student>
    {
        public StudentSpecification(int studentId)
            : base(s => s.Id == studentId)
        {
        }
    }
    public class StudentWithCoursesSpecification : BaseSpecification<Student>
    {
        public StudentWithCoursesSpecification(int studentId) 
            : base(s => s.Id == studentId)
        {
            AddInclude(s => s.StudentCourses);
        }
    }
    public class StudentByAgeRangeSpecification : BaseSpecification<Student>
    {
        public StudentByAgeRangeSpecification(int minAge, int maxAge)
            : base(s => s.Age >= minAge && s.Age <= maxAge)
        {
            AddOrderBy(s => s.Sname);
            AddInclude(s => s.StudentCourses);
        }

        public StudentByAgeRangeSpecification(int minAge, int maxAge, int skip, int take) 
            : this(minAge, maxAge)
        {
            ApplyPaging(skip, take);
        }
    }
    public class StudentWithMinimumCoursesSpecification : BaseSpecification<Student>
    {
        public StudentWithMinimumCoursesSpecification(int minCourses)
            : base(s => s.StudentCourses.Count >= minCourses)
        {
            AddInclude(s => s.StudentCourses);
            AddOrderByDescending(s => s.StudentCourses.Count);
        }
    }
    
    public class AllStudentsSpecification : BaseSpecification<Student>
    {
        public AllStudentsSpecification(int skip = 0, int take = 10)
            : base()
        {
            AddOrderBy(s => s.Sname);
            AddInclude(s => s.StudentCourses);
            ApplyPaging(skip, take);
        }
    }

    public class StudentSearchSpecification : BaseSpecification<Student>
    {
        public StudentSearchSpecification(string searchTerm, int skip = 0, int take = 10)
            : base(s => s.Sname.Contains(searchTerm))
        {
            AddOrderBy(s => s.Sname);
            AddInclude(s => s.StudentCourses);
            ApplyPaging(skip, take);
        }
    }
}