using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProjectController(IGenericRepository<Project> repo, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(mapper.Map<ProjectDto>) });
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDto>> Get(int id)
    {
        var p = await repo.GetByIdAsync(id);
        return p is null ? NotFound() : Ok(mapper.Map<ProjectDto>(p));
    }


    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(ProjectDto dto)
    {
        var entity = mapper.Map<Project>(dto);
        await repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.P_No }, mapper.Map<ProjectDto>(entity));
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProjectDto dto)
    {
        if (id != dto.P_No) return BadRequest();
        var entity = mapper.Map<Project>(dto);
        await repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}