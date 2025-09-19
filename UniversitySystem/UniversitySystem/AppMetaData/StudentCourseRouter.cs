namespace UniversitySystem.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public class StudentCourseRouter : Router
        {
            private const string Prefix = Rule + "StudentCourse";
            public const string Main = Prefix + "/";
            public const string MainId = Prefix + "/{id}";
        }
    }
  
}