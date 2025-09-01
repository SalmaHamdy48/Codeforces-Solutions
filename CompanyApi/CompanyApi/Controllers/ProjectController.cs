using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProjectController(IGenericRepository<Project> repo, IMapper mapper) : ControllerBase
{
    private readonly IGenericRepository<Project> _repo = repo;
    private readonly IMapper _mapper = mapper;


    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(_mapper.Map<ProjectDto>) });
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDto>> Get(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        return p is null ? NotFound() : Ok(_mapper.Map<ProjectDto>(p));
    }


    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(ProjectDto dto)
    {
        var entity = _mapper.Map<Project>(dto);
        await _repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.P_No }, _mapper.Map<ProjectDto>(entity));
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProjectDto dto)
    {
        if (id != dto.P_No) return BadRequest();
        var entity = _mapper.Map<Project>(dto);
        await _repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}