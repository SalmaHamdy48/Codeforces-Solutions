// Controllers/StudentController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            var result = await Mediator.Send(query);
            return Result(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetStudentByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return Result(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto studentDto)
        {
            var result = await Mediator.Send(studentDto);
            
            if (result.Status)
            {
                var createdId = ((dynamic)result.Data)?.Id;
                return CreatedAtAction(nameof(GetById), new { id = createdId }, result);
            }
            
            return Result(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            updateStudentDto.Id = id;
            var result = await Mediator.Send(updateStudentDto);
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteStudentDto { Id = id };
            var result = await Mediator.Send(command);
            return Result(result);
        }
    }
}