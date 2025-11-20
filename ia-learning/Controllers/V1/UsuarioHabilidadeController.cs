using ia_learning.Data;
using ia_learning.DTOs;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioHabilidadeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioHabilidadeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _context.UsuarioHabilidades
                .Include(u => u.Usuario)
                .Include(h => h.Habilidade)
                .ToListAsync();

            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioHabilidadeDto dto)
        {
            var model = new UsuarioHabilidade
            {
                UsuarioId = dto.UsuarioId,
                HabilidadeId = dto.HabilidadeId
            };

            _context.UsuarioHabilidades.Add(model);
            await _context.SaveChangesAsync();

            dto.Id = model.Id;

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.UsuarioHabilidades.FindAsync(id);
            if (item == null) return NotFound();

            _context.UsuarioHabilidades.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
