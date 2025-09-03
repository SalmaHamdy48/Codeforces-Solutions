using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Mapping;

public class ManageProfile : Profile
{
    public ManageProfile()
    {
        CreateMap<Manages, ManageDto>().ReverseMap();
    }
}