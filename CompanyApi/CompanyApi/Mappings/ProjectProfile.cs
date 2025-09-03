using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Mapping;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
    }
}