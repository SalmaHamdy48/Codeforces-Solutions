using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Categories.Commands.Add;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class CategoryController : BaseController
{
    [HttpPost(Router.CategoryRouter.Add)]
    public async Task<IActionResult> Create(AddCategoryCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Result(result);
    }
}