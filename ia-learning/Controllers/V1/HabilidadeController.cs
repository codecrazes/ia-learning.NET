using ia_learning.Data;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HabilidadeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HabilidadeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Habilidades.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var habilidade = await _context.Habilidades.FindAsync(id);
            return habilidade == null ? NotFound() : Ok(habilidade);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Habilidade habilidade)
        {
            _context.Habilidades.Add(habilidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = habilidade.Id }, habilidade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Habilidade model)
        {
            var habilidade = await _context.Habilidades.FindAsync(id);
            if (habilidade == null) return NotFound();

            habilidade.Nome = model.Nome;

            await _context.SaveChangesAsync();
            return Ok(habilidade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var habilidade = await _context.Habilidades.FindAsync(id);
            if (habilidade == null) return NotFound();

            _context.Habilidades.Remove(habilidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
