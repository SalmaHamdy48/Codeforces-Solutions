using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Dependent, DependentDto>().ReverseMap();
        CreateMap<WorksOnHours, WorksOnDto>().ReverseMap();
        CreateMap<Manages, ManageDto>().ReverseMap();

        CreateMap<Employee, EmployeeDto>()
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null))
            .ForMember(d => d.ImageUrl, o => o.MapFrom<ImageUrlResolver>());

        CreateMap<EmployeeCreateDto, Employee>()
            .ForMember(d => d.ImageFileName, o => o.Ignore());

        CreateMap<EmployeeUpdateDto, Employee>()
            .ForMember(d => d.ImageFileName, o => o.Ignore());
    }
}