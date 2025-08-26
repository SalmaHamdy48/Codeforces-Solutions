using ApiTask.Data;
using ApiTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var logins = await _context.Logins.ToListAsync(cancellationToken);
            return Ok(logins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();
            return Ok(login);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Login login, CancellationToken cancellationToken)
        {
            await _context.Logins.AddAsync(login, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = login.Id }, login);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Login login, CancellationToken cancellationToken)
        {
            if (id != login.Id) return BadRequest();

            _context.Entry(login).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
