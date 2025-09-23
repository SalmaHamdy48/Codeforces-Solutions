using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Categories.Dtos;
using Project.Domain.Models;
using Project.Domain.Models.Categories;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Queries.GetById;

public class GetCategoryByIdQueryHandler(IMapper mapper, IReadRepository<Category> categoryRepository) : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<Response<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            return Response<CategoryDto>.NotFound("Category not found");
        }

        var categoryDto = mapper.Map<CategoryDto>(category);
        return Response<CategoryDto>.Success(categoryDto);
    }
}