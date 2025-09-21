using Project.Application.Features.Categories.Commands.Add;

namespace Project.Application.Mapping.Category;

public class CategoryProfile : AutoMapper.Profile
{
    public CategoryProfile()
    {
        CreateMap<Domain.Models.Categories.Category, AddCategoryCommand>().ReverseMap();
    }
}