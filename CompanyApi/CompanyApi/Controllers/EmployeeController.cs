using AutoMapper;
using CompanyApi.Dto;
using CompanyApi.Models;
using CompanyApi.Repositories;
using CompanyApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CompanyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IGenericRepository<Employee> repo, IMapper mapper, IFileUpload fileUpload)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await repo.GetPagedAsync(pageNumber, pageSize, includeProperties: "Department");
        var dtos = items.Select(mapper.Map<EmployeeDto>);
        return Ok(new { total, pageNumber, pageSize, data = dtos });
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> Get(Guid id)
    {
        var e = await repo.GetByIdAsync(id);
        return e is null ? NotFound() : Ok(mapper.Map<EmployeeDto>(e));
    }


    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create([FromForm] EmployeeCreateDto dto)
    {
        var entity = mapper.Map<Employee>(dto);
        entity.ImageFileName = await fileUpload.SaveAsync(dto.Image, "employee");
        await repo.AddAsync(entity);
        var result = mapper.Map<EmployeeDto>(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, result);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] EmployeeUpdateDto dto)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity is null) return NotFound();


        var oldImage = entity.ImageFileName;
        mapper.Map(dto, entity);
        if (dto.Image != null)
        {
            await fileUpload.DeleteIfExistsAsync(oldImage, "employee");
            entity.ImageFileName = await fileUpload.SaveAsync(dto.Image, "employee");
        }


        await repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var e = await repo.GetByIdAsync(id);
        if (e == null) return NotFound();
        await fileUpload.DeleteIfExistsAsync(e.ImageFileName, "employee");
        await repo.DeleteAsync(id);
        return NoContent();
    }
}
