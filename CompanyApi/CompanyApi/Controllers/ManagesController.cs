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
    private readonly IGenericRepository<Manages> _repo = repo;
    private readonly IMapper _mapper = mapper;


    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
        return Ok(new { total, pageNumber, pageSize, data = items.Select(_mapper.Map<ManageDto>) });
    }


    [HttpGet("{employeeId:guid}/{departmentId:int}")]
    public async Task<ActionResult<ManageDto>> Get(Guid employeeId, int departmentId)
    {
        var entity = await _repo.GetByIdAsync(new object[] { employeeId, departmentId });
        return entity is null ? NotFound() : Ok(_mapper.Map<ManageDto>(entity));
    }


    [HttpPost]
    public async Task<ActionResult<ManageDto>> Create(ManageDto dto)
    {
        var entity = _mapper.Map<Manages>(dto);
        await _repo.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { employeeId = entity.EmployeeId, departmentId = entity.DepartmentId }, _mapper.Map<ManageDto>(entity));
    }


    [HttpPut]
    public async Task<IActionResult> Update(ManageDto dto)
    {
        var entity = _mapper.Map<Manages>(dto);
        await _repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{employeeId:guid}/{departmentId:int}")]
    public async Task<IActionResult> Delete(Guid employeeId, int departmentId)
    {
        await _repo.DeleteAsync(new object[] { employeeId, departmentId });
        return NoContent();
    }
}