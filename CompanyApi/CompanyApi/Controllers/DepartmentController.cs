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
    private readonly IGenericRepository<Department> _repo = repo;
    private readonly IMapper _mapper = mapper;


    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(_mapper.Map<DepartmentDto>) });
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> Get(int id)
    {
        var dep = await _repo.GetByIdAsync(id);
        return dep is null ? NotFound() : Ok(_mapper.Map<DepartmentDto>(dep));
    }


    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create(DepartmentDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        await _repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.D_No }, _mapper.Map<DepartmentDto>(entity));
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentDto dto)
    {
        if (id != dto.D_No) return BadRequest();
        var entity = _mapper.Map<Department>(dto);
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