using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Mapping;

public class WorksOnProfile : Profile
{
    public WorksOnProfile()
    {
        CreateMap<WorksOnHours, WorksOnDto>().ReverseMap();
    }
}