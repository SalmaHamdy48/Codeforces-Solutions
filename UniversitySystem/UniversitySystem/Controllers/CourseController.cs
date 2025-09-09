using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Features.Course.Command.Handlers;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _repo;
    private readonly CreateCourseHandler _createHandler;
    private readonly UpdateCourseHandler _updateHandler;
    private readonly DeleteCourseHandler _deleteHandler;

    public CourseController(ICourseRepository repo, CreateCourseHandler createHandler,
        UpdateCourseHandler updateHandler, DeleteCourseHandler deleteHandler)
    {
        _repo = repo;
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
    [HttpGet("{id}")] public async Task<IActionResult> GetById(int id)
        => (await _repo.GetByIdAsync(id)) is Course course ? Ok(course) : NotFound("Course not found");

    [HttpPost] public async Task<IActionResult> Create(CreateCourseDto dto) => Ok(await _createHandler.Handle(dto));
    [HttpPut("{id}")] public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
    {
        dto.Id = id;
        return Ok(await _updateHandler.Handle(dto));
    }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id)
        => await _deleteHandler.Handle(id) ? Ok("Deleted") : NotFound("Course not found");
}
