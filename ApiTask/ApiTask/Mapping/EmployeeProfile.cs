using AutoMapper;
using ApiTask.Models;
using ApiTask.Dto;

namespace ApiTask.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
        }
    }
}