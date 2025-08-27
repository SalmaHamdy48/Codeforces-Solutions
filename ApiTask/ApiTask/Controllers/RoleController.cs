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
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoleController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.ToListAsync(cancellationToken);
            var roleDtos = _mapper.Map<List<RoleDto>>(roles);
            return Ok(roleDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
            if (role == null) return NotFound();

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _context.Roles.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<RoleDto>(role);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleDto roleDto, CancellationToken cancellationToken)
        {
            if (id != roleDto.Id) return BadRequest();

            var role = _mapper.Map<Role>(roleDto);
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
