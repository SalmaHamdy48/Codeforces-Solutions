using UniversitySystem.Models;

namespace UniversitySystem.Specifications
{
    public class AdultStudentSpecification : BaseSpecification<Student>
    {
        public override System.Linq.Expressions.Expression<Func<Student, bool>> Criteria => s => s.Age >= 18;
    }

    public class StudentByNameSpecification(string name) : BaseSpecification<Student>
    {
        public override System.Linq.Expressions.Expression<Func<Student, bool>> Criteria => s => s.Sname.Contains(name);
    }
}