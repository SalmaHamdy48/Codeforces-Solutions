using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CompanyApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ManagesController(IGenericRepository<Manages> repo, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(mapper.Map<ManageDto>) });
    }


    [HttpGet("{employeeId:guid}/{departmentId:int}")]
    public async Task<ActionResult<ManageDto>> Get(Guid employeeId, int departmentId)
    {
        var entity = await repo.GetByIdAsync(new object[] { employeeId, departmentId });
        return entity is null ? NotFound() : Ok(mapper.Map<ManageDto>(entity));
    }


    [HttpPost]
    public async Task<ActionResult<ManageDto>> Create(ManageDto dto)
    {
        var entity = mapper.Map<Manages>(dto);
        await repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { employeeId = entity.EmployeeId, departmentId = entity.DepartmentId }, mapper.Map<ManageDto>(entity));
    }


    [HttpPut]
    public async Task<IActionResult> Update(ManageDto dto)
    {
        var entity = mapper.Map<Manages>(dto);
        await repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{employeeId:guid}/{departmentId:int}")]
    public async Task<IActionResult> Delete(Guid employeeId, int departmentId)
    {
        await repo.DeleteAsync(new object[] { employeeId, departmentId });
        return NoContent();
    }
}