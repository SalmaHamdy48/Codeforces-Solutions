using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CompanyApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class WorksOnController(IGenericRepository<WorksOnHours> repo, IMapper mapper) : ControllerBase
{
    private readonly IGenericRepository<WorksOnHours> _repo = repo;
    private readonly IMapper _mapper = mapper;


    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(_mapper.Map<WorksOnDto>) });
    }


    [HttpGet("{employeeId:guid}/{projectId:int}")]
    public async Task<ActionResult<WorksOnDto>> Get(Guid employeeId, int projectId)
    {
        var entity = await _repo.GetByIdAsync(new object[] { employeeId, projectId });
        return entity is null ? NotFound() : Ok(_mapper.Map<WorksOnDto>(entity));
    }


    [HttpPost]
    public async Task<ActionResult<WorksOnDto>> Create(WorksOnDto dto)
    {
        var entity = _mapper.Map<WorksOnHours>(dto);
        await _repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { employeeId = entity.EmployeeId, projectId = entity.ProjectId }, _mapper.Map<WorksOnDto>(entity));
    }


    [HttpPut]
    public async Task<IActionResult> Update(WorksOnDto dto)
    {
        var entity = _mapper.Map<WorksOnHours>(dto);
        await _repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{employeeId:guid}/{projectId:int}")]
    public async Task<IActionResult> Delete(Guid employeeId, int projectId)
    {
        await _repo.DeleteAsync(new object[] { employeeId, projectId });
        return NoContent();
    }
}