
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniversitySystem.Global;

namespace UniversitySystem.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        private IMediator? _mediator;

        protected IActionResult Result(Response response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(string.Empty, response),
                HttpStatusCode.Accepted => Accepted(response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
                HttpStatusCode.Conflict => Conflict(response),
                HttpStatusCode.UnsupportedMediaType => StatusCode((int)HttpStatusCode.UnsupportedMediaType, response),
                HttpStatusCode.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, response),
                _ => BadRequest(response)
            };
        }

        
        protected IActionResult CreatedResult(Response response, string routeName, object routeValues)
        {
            return response.StatusCode == HttpStatusCode.Created 
                ? CreatedAtRoute(routeName, routeValues, response)
                : Result(response);
        }
    }
}