using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Mapping;

public class DependentProfile : Profile
{
    public DependentProfile()
    {
        CreateMap<Dependent, DependentDto>().ReverseMap();
    }
}