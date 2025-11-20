using ia_learning.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("ias-mais-usadas")]
        public async Task<IActionResult> IAsMaisUsadas()
        {
            var resultado = await _context.Tarefas
                .GroupBy(t => t.IA.Nome)
                .Select(g => new
                {
                    IA = g.Key,
                    Quantidade = g.Count()
                })
                .OrderByDescending(x => x.Quantidade)
                .ToListAsync();

            return Ok(resultado);
        }

        [HttpGet("media-avaliacoes")]
        public async Task<IActionResult> MediaAvaliacoes()
        {
            var medias = await _context.Avaliacoes
                .GroupBy(a => a.IA.Nome)
                .Select(g => new
                {
                    IA = g.Key,
                    Media = g.Average(a => a.Nota)
                })
                .ToListAsync();

            return Ok(medias);
        }

        [HttpGet("tarefas-por-usuario")]
        public async Task<IActionResult> TarefasPorUsuario()
        {
            var dados = await _context.Tarefas
                .GroupBy(t => t.Usuario.Nome)
                .Select(g => new
                {
                    Usuario = g.Key,
                    Total = g.Count()
                })
                .ToListAsync();

            return Ok(dados);
        }
    }
}
