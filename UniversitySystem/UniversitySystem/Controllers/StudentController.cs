// Controllers/StudentController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.AppMetaData.BaseRouter;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Global;

namespace UniversitySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllStudentsQuery 
            { 
                Page = pageIndex, 
                PageSize = pageSize 
            };
            var result = await mediator.Send(query);
            return Result(result);
        }

        [HttpGet(Router.StudentRouter.MainId)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetStudentByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto studentDto)
        {
            var result = await mediator.Send(studentDto);
            return Ok(result);
        }

        [HttpPut(Router.StudentRouter.Main)]
        public async Task<IActionResult> Update( [FromBody] UpdateStudentDto updateStudentDto)
        {
            var result = await mediator.Send(updateStudentDto);
            return Ok(result);
        }


        [HttpDelete(Router.StudentRouter.MainId)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteStudentDto(id));
            return Ok(result);
        }
    }
}