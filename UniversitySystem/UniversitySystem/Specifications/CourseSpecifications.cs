using UniversitySystem.Models;

namespace UniversitySystem.Specifications
{
    public class CourseByHoursSpecification(int minHours) : BaseSpecification<Course>
    {
        public override System.Linq.Expressions.Expression<Func<Course, bool>> Criteria => c => c.Hours >= minHours;
    }

    public class CourseByNameSpecification(string name) : BaseSpecification<Course>
    {
        public override System.Linq.Expressions.Expression<Func<Course, bool>> Criteria => c => c.Cname.Contains(name);
    }
}