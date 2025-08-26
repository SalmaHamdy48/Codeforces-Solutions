using AutoMapper;
using ApiTask.Models;
using ApiTask.Dto;

namespace ApiTask.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
        }
    }
}