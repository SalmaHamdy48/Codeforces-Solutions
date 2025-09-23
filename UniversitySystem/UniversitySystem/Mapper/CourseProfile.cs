
using AutoMapper;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Models;
using UniversitySystem.Features.Course.Query.Models;
using UniversitySystem.Features.Student.Command.Models;

namespace UniversitySystem.Mapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();
            CreateMap<Course, GetAllCoursesQuery>().ReverseMap();
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            
            
            
        }
    }
}