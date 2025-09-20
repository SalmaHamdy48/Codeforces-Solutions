
using AutoMapper;
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
            
            
            CreateMap<Student, object>()
                .ForMember("Id", opt => opt.MapFrom(src => src.Id))
                .ForMember("Sname", opt => opt.MapFrom(src => src.Sname))
                .ForMember("Age", opt => opt.MapFrom(src => src.Age))
                .ForMember("EnrolledCourses", opt => opt.MapFrom(src => 
                    src.StudentCourses.Select(sc => new
                    {
                        CourseId = sc.CourseId,
                        Code = sc.Course.Code,
                        Cname = sc.Course.Cname,
                        Hours = sc.Course.Hours
                    }).ToList()));
        }
    }
}