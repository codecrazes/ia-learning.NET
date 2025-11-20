using ia_learning.Data;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IAController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IAController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.IAs.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ia = await _context.IAs.FindAsync(id);
            return ia == null ? NotFound() : Ok(ia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IA model)
        {
            _context.IAs.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IA model)
        {
            var ia = await _context.IAs.FindAsync(id);
            if (ia == null) return NotFound();

            ia.Nome = model.Nome;
            ia.Provedor = model.Provedor;
            ia.Descricao = model.Descricao;
            ia.Custo = model.Custo;
            ia.Tipo = model.Tipo;

            await _context.SaveChangesAsync();
            return Ok(ia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ia = await _context.IAs.FindAsync(id);
            if (ia == null) return NotFound();

            _context.IAs.Remove(ia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
