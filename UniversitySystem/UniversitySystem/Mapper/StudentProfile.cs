
using AutoMapper;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Models;
using UniversitySystem.Features.Student.Query.Models;

namespace UniversitySystem.Mapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            
            CreateMap<CreateStudentDto, Student>().ReverseMap();
            CreateMap<UpdateStudentDto, Student>().ReverseMap();
            CreateMap<Course  , CreateCourseDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            
        }
    }
}