
using AutoMapper;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Models;

namespace UniversitySystem.Mapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();
        }
    }
}
