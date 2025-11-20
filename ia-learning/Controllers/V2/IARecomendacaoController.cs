using ia_learning.Data;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ia_learning.OpenAI;

namespace ia_learning.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class IARecomendacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly OpenAIClientHelper _openAI;

        public IARecomendacaoController(AppDbContext context, OpenAIClientHelper openAI)
        {
            _context = context;
            _openAI = openAI;
        }

        [HttpPost("gerar/{usuarioId}")]
        public async Task<IActionResult> GerarRecomendacao(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuarioHabilidades)
                    .ThenInclude(uh => uh.Habilidade)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var habilidades = string.Join(", ", usuario.UsuarioHabilidades.Select(h => h.Habilidade.Nome));

            var prompt = $@"
Um usuário possui as seguintes habilidades: {habilidades}.
Sugira quais Inteligências Artificiais (IA) são mais adequadas para ele aprender.
Explique de forma simples e motivadora.";

            var resposta = await _openAI.EnviarMensagem(prompt);

            return Ok(new
            {
                usuario = usuario.Nome,
                recomendacao = resposta
            });
        }
    }
}
