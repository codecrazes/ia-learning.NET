using ia_learning.Data;
using ia_learning.DTOs;
using ia_learning.Hateoas;
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
            var list = await _context.Avaliacoes
                .Include(a => a.Usuario)
                .Include(a => a.IA)
                .ToListAsync();

            var response = LinkHelper.WithLinks(
                list,
                Url,
                "GetAvaliacao",
                "UpdateAvaliacao",
                "DeleteAvaliacao",
                a => a.Id
            );

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetAvaliacao")]
        public async Task<IActionResult> Get(int id)
        {
            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Usuario)
                .Include(a => a.IA)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (avaliacao == null)
                return NotFound();

            var result = LinkHelper.WithLinks(
                avaliacao,
                Url,
                "GetAvaliacao",
                "UpdateAvaliacao",
                "DeleteAvaliacao",
                id
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AvaliacaoCreateDto dto)
        {
            var model = new Avaliacao
            {
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                UsuarioId = dto.UsuarioId,
                IAId = dto.IAId,
                Data = DateTime.Now
            };

            _context.Avaliacoes.Add(model);
            await _context.SaveChangesAsync();

            var result = LinkHelper.WithLinks(
                model,
                Url,
                "GetAvaliacao",
                "UpdateAvaliacao",
                "DeleteAvaliacao",
                model.Id
            );

            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateAvaliacao")]
        public async Task<IActionResult> Update(int id, AvaliacaoCreateDto dto)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao == null)
                return NotFound();

            avaliacao.Nota = dto.Nota;
            avaliacao.Comentario = dto.Comentario;
            avaliacao.UsuarioId = dto.UsuarioId;
            avaliacao.IAId = dto.IAId;

            await _context.SaveChangesAsync();

            var result = LinkHelper.WithLinks(
                avaliacao,
                Url,
                "GetAvaliacao",
                "UpdateAvaliacao",
                "DeleteAvaliacao",
                avaliacao.Id
            );

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteAvaliacao")]
        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao == null)
                return NotFound();

            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
