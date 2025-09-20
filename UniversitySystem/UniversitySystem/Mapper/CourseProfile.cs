
using AutoMapper;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Models;
using UniversitySystem.Features.Course.Query.Models;

namespace UniversitySystem.Mapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();
            
            
            CreateMap<Course, object>()
                .ForMember("Id", opt => opt.MapFrom(src => src.Id))
                .ForMember("Code", opt => opt.MapFrom(src => src.Code))
                .ForMember("Cname", opt => opt.MapFrom(src => src.Cname))
                .ForMember("Hours", opt => opt.MapFrom(src => src.Hours))
                .ForMember("EnrolledStudents", opt => opt.MapFrom(src => 
                    src.StudentCourses.Select(sc => new
                    {
                        StudentId = sc.StudentId,
                        Sname = sc.Student.Sname,
                        Age = sc.Student.Age
                    }).ToList()));
        }
    }
}