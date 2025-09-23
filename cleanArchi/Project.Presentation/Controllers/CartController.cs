using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Carts.Commands.Add;
using Project.Application.Features.Carts.Commands.Delete;
using Project.Application.Features.Carts.Commands.Update;
using Project.Application.Features.Carts.Queries.GetAll;
using Project.Application.Features.Carts.Queries.GetById;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class CartController : BaseController
{
    [HttpPost(Router.CartRouter.Add)]
    public async Task<IActionResult> Create(AddCartCommand command)
    {
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpPut(Router.CartRouter.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCartCommand command)
    {
        command = new UpdateCartCommand(id, command.UserId);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpDelete(Router.CartRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCartCommand(id));
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCartsQuery request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }

    [HttpGet(Router.CartRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCartByIdQuery(id));
        return Result(result);
    }
    
}