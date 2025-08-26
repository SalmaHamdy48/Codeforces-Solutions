using ApiTask.Data;
using ApiTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var depts = await _context.Departments.ToListAsync(cancellationToken);
            return Ok(depts);
        }

        // Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var dept = await _context.Departments.FindAsync(new object[] { id }, cancellationToken);
            if (dept == null) return NotFound();
            return Ok(dept);
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create(Department department, CancellationToken cancellationToken)
        {
            await _context.Departments.AddAsync(department, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department, CancellationToken cancellationToken)
        {
            if (id != department.Id) return BadRequest();

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        // Delete
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
