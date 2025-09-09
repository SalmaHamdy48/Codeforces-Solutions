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
           
            CreateMap<Student, StudentListResponseDto>();
            
         
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}