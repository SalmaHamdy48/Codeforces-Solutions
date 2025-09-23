using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.CartItems.Commands.Add;
using Project.Application.Features.CartItems.Commands.Delete;
using Project.Application.Features.CartItems.Commands.Update;
using Project.Application.Features.CartItems.Queries.GetAll;
using Project.Application.Features.CartItems.Queries.GetById;
using Project.Domain.Routes.BaseRouter;

namespace Project.Presentation.Controllers;

public class CartItemController : BaseController
{
    [HttpPost(Router.CartItemRouter.Add)]
    public async Task<IActionResult> Create(AddCartItemCommand command)
    {
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpPut(Router.CartItemRouter.Update)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCartItemCommand command)
    {
        command = new UpdateCartItemCommand(id, command.CartId, command.ProductId, command.Quantity);
        var result = await mediator.Send(command);
        return Result(result);
    }

    [HttpDelete(Router.CartItemRouter.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCartItemCommand(id));
        return Result(result);
    }

    [HttpGet(Router.CartItemRouter.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllItemsQuery request)
    {
        var result = await mediator.Send(request);
        return Result(result);
    }

    [HttpGet(Router.CartItemRouter.GetById)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetItemByIdQuery(id));
        return Result(result);
    }
}