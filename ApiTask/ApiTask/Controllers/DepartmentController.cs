using ApiTask.Data;
using ApiTask.Dto;
using ApiTask.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var depts = await _context.Departments.ToListAsync(cancellationToken);
            var deptDtos = _mapper.Map<List<DepartmentDto>>(depts);
            return Ok(deptDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var dept = await _context.Departments.FindAsync(new object[] { id }, cancellationToken);
            if (dept == null) return NotFound();

            var deptDto = _mapper.Map<DepartmentDto>(dept);
            return Ok(deptDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto deptDto, CancellationToken cancellationToken)
        {
            var dept = _mapper.Map<Department>(deptDto);
            await _context.Departments.AddAsync(dept, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<DepartmentDto>(dept);
            return CreatedAtAction(nameof(GetById), new { id = dept.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentDto deptDto, CancellationToken cancellationToken)
        {
            if (id != deptDto.Id) return BadRequest();

            var dept = _mapper.Map<Department>(deptDto);
            _context.Entry(dept).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var dept = await _context.Departments.FindAsync(new object[] { id }, cancellationToken);
            if (dept == null) return NotFound();

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
