using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CompanyApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DepartmentController(IGenericRepository<Department> repo, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(mapper.Map<DepartmentDto>) });
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> Get(int id)
    {
        var dep = await repo.GetByIdAsync(id);
        return dep is null ? NotFound() : Ok(mapper.Map<DepartmentDto>(dep));
    }


    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create(DepartmentDto dto)
    {
        var entity = mapper.Map<Department>(dto);
        await repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.D_No }, mapper.Map<DepartmentDto>(entity));
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentDto dto)
    {
        if (id != dto.D_No) return BadRequest();
        var entity = mapper.Map<Department>(dto);
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