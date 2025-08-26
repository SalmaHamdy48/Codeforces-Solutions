using AutoMapper;
using ApiTask.Models;
using ApiTask.Dto;

namespace ApiTask.Mapping
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginDto, Login>().ReverseMap();
        }
    }
}