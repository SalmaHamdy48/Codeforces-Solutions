
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.AppMetaData.BaseRouter;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Features.Course.Query.Models;
using UniversitySystem.Global;

namespace UniversitySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllCoursesQuery 
            { 
                Page = pageIndex, 
                PageSize = pageSize 
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetCourseByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto courseDto)
        {
            var result = await mediator.Send(courseDto);
            return Ok(result);
        }
        
        [HttpPut(Router.CourseRouter.Main)]
        public async Task<IActionResult> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            var result = await mediator.Send(updateCourseDto);
            return Ok(result);
        }


        [HttpDelete(Router.CourseRouter.MainId)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteCourseDto(id));
            return Ok(result);
        }
    }
}