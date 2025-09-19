
using UniversitySystem.Models;
using UniversitySystem.Specifications;

namespace UniversitySystem.Specifications
{
    public class CourseSpecification : BaseSpecification<Course>
    {
        public CourseSpecification(int courseId)
            : base(c => c.Id == courseId)
        {
        }
    }

    public class CourseWithStudentsSpecification : BaseSpecification<Course>
    {
        public CourseWithStudentsSpecification(int courseId)
            : base(c => c.Id == courseId)
        {
            AddInclude(c => c.StudentCourses);
        }
    }

    public class CoursesByStudentSpecification : BaseSpecification<Course>
    {
        public CoursesByStudentSpecification(int studentId)
            : base(c => c.StudentCourses.Any(sc => sc.StudentId == studentId))
        {
            AddOrderBy(c => c.Code);
            AddInclude(c => c.StudentCourses);
        }
    }

    public class CoursesWithMinimumStudentsSpecification : BaseSpecification<Course>
    {
        public CoursesWithMinimumStudentsSpecification(int minStudents)
            : base(c => c.StudentCourses.Count >= minStudents)
        {
            AddInclude(c => c.StudentCourses);
            AddOrderByDescending(c => c.StudentCourses.Count);
        }

        public CoursesWithMinimumStudentsSpecification(int minStudents, int skip, int take)
            : this(minStudents)
        {
            ApplyPaging(skip, take);
        }
    }

    public class AllCoursesSpecification : BaseSpecification<Course>
    {
        public AllCoursesSpecification(int skip = 0, int take = 10)
            : base()
        {
            AddOrderBy(c => c.Code);
            AddInclude(c => c.StudentCourses);
            ApplyPaging(skip, take);
        }
    }

    public class CourseSearchSpecification : BaseSpecification<Course>
    {
        public CourseSearchSpecification(string searchTerm, int skip = 0, int take = 10)
            : base(c => c.Cname.Contains(searchTerm) || c.Code.Contains(searchTerm))
        {
            AddOrderBy(c => c.Code);
            AddInclude(c => c.StudentCourses);
            ApplyPaging(skip, take);
        }
    }
}