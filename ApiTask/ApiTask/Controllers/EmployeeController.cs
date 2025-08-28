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
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .ToListAsync(cancellationToken);

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (employee == null) return NotFound();

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<EmployeeDto>(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, resultDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { id }, cancellationToken);
            if (employee == null) return NotFound();

            _mapper.Map(employeeDto, employee);

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { id }, cancellationToken);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
