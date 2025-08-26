using AutoMapper;
using ApiTask.Models;
using ApiTask.Dto;

namespace ApiTask.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}