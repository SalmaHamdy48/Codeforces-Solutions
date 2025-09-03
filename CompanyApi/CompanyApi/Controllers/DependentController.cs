using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CompanyApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DependentController(IGenericRepository<Dependent> repo, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(mapper.Map<DependentDto>) });
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<DependentDto>> Get(int id)
    {
        var d = await repo.GetByIdAsync(id);
        return d is null ? NotFound() : Ok(mapper.Map<DependentDto>(d));
    }


    [HttpPost]
    public async Task<ActionResult<DependentDto>> Create(DependentDto dto)
    {
        var entity = mapper.Map<Dependent>(dto);
        await repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, mapper.Map<DependentDto>(entity));
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DependentDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var entity = mapper.Map<Dependent>(dto);
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