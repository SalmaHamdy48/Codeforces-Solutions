using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Features.Student.Query.Handlers;
using UniversitySystem.Features.Student.Query.Models;

namespace UniversitySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto command) => Ok(await _mediator.Send(command));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound("Student not found");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllStudentsQuery()));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentDto command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : NotFound("Student not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _mediator.Send(new DeleteStudentDto { Id = id }) ? Ok("Deleted") : NotFound("Student not found");
    }
}