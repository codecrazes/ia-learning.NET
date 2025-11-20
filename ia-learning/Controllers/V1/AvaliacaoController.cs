using ia_learning.Models;
using ia_learning.Data;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var avaliacoes = await _context.Avaliacoes
                .Include(a => a.Usuario)
                .Include(a => a.IA)
                .ToListAsync();

            return Ok(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Usuario)
                .Include(a => a.IA)
                .FirstOrDefaultAsync(a => a.Id == id);

            return avaliacao == null ? NotFound() : Ok(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Avaliacao model)
        {
            model.Data = DateTime.Now;

            _context.Avaliacoes.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Avaliacao model)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null) return NotFound();

            avaliacao.Nota = model.Nota;
            avaliacao.Comentario = model.Comentario;
            avaliacao.UsuarioId = model.UsuarioId;
            avaliacao.IAId = model.IAId;

            await _context.SaveChangesAsync();
            return Ok(avaliacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null) return NotFound();

            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
