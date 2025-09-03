using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Mapping;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>().ReverseMap();
    }
}