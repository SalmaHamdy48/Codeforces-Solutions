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
    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(mapper.Map<WorksOnDto>) });
    }


    [HttpGet("{employeeId:guid}/{projectId:int}")]
    public async Task<ActionResult<WorksOnDto>> Get(Guid employeeId, int projectId)
    {
        var entity = await repo.GetByIdAsync(new object[] { employeeId, projectId });
        return entity is null ? NotFound() : Ok(mapper.Map<WorksOnDto>(entity));
    }


    [HttpPost]
    public async Task<ActionResult<WorksOnDto>> Create(WorksOnDto dto)
    {
        var entity = mapper.Map<WorksOnHours>(dto);
        await repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { employeeId = entity.EmployeeId, projectId = entity.ProjectId }, mapper.Map<WorksOnDto>(entity));
    }


    [HttpPut]
    public async Task<IActionResult> Update(WorksOnDto dto)
    {
        var entity = mapper.Map<WorksOnHours>(dto);
        await repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{employeeId:guid}/{projectId:int}")]
    public async Task<IActionResult> Delete(Guid employeeId, int projectId)
    {
        await repo.DeleteAsync(new object[] { employeeId, projectId });
        return NoContent();
    }
}