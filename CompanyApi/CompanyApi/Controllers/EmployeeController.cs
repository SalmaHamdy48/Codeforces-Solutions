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
    private readonly IGenericRepository<Employee> _repo = repo;
    private readonly IMapper _mapper = mapper;
    private readonly IFileUpload _fileUpload = fileUpload;


    [HttpGet]
    public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize, includeProperties: "Department");
        var dtos = items.Select(_mapper.Map<EmployeeDto>);
        return Ok(new { total, pageNumber, pageSize, data = dtos });
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> Get(Guid id)
    {
        var e = await _repo.GetByIdAsync(id);
        return e is null ? NotFound() : Ok(_mapper.Map<EmployeeDto>(e));
    }


    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create([FromForm] EmployeeCreateDto dto)
    {
        var entity = _mapper.Map<Employee>(dto);
        entity.ImageFileName = await _fileUpload.SaveAsync(dto.Image, "employee");
        await _repo.AddAsync(entity);
        var result = _mapper.Map<EmployeeDto>(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, result);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] EmployeeUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return NotFound();


        var oldImage = entity.ImageFileName;
        _mapper.Map(dto, entity);
        if (dto.Image != null)
        {
            await _fileUpload.DeleteIfExistsAsync(oldImage, "employee");
            entity.ImageFileName = await _fileUpload.SaveAsync(dto.Image, "employee");
        }


        await _repo.UpdateAsync(entity);
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return NotFound();
        await _fileUpload.DeleteIfExistsAsync(e.ImageFileName, "employee");
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
