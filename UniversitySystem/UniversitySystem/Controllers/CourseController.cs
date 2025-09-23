
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
            return Result(result);
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCourseByIdQuery { Id = id };
            var result = await mediator.Send(query);
            return Result(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto courseDto)
        {
            var result = await mediator.Send(courseDto);
            return Result(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            updateCourseDto.Id = id;
            var result = await mediator.Send(updateCourseDto);
            return Result(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCourseDto { Id = id };
            var result = await mediator.Send(command);
            return Result(result);
        }
    }
}