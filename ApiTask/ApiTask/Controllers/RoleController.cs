using ApiTask.Data;
using ApiTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.ToListAsync(cancellationToken);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role, CancellationToken cancellationToken)
        {
            await _context.Roles.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Role role, CancellationToken cancellationToken)
        {
            if (id != role.Id) return BadRequest();

            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
            if (role == null) return NotFound();

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
