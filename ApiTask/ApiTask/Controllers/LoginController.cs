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
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LoginController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var logins = await _context.Logins.ToListAsync(cancellationToken);
            var loginDtos = _mapper.Map<List<LoginDto>>(logins);
            return Ok(loginDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();

            var loginDto = _mapper.Map<LoginDto>(login);
            return Ok(loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var login = _mapper.Map<Login>(loginDto);
            await _context.Logins.AddAsync(login, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<LoginDto>(login);
            return CreatedAtAction(nameof(GetById), new { id = login.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LoginDto loginDto, CancellationToken cancellationToken)
        {
            var login = await _context.Logins.FindAsync(new object[] { id }, cancellationToken);
            if (login == null) return NotFound();

            _mapper.Map(loginDto, login);
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
