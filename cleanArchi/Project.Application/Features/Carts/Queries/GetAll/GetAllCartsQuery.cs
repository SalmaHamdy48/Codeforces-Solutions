using Project.Application.Abstractions.Messaging;
using Project.Application.Features.Carts.Dtos;
using Project.Domain.Filters;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Queries.GetAll;

public record GetAllCartsQuery(string? UserId) : BaseFilter, IQuery<PaginatedResult<CartDto>>;