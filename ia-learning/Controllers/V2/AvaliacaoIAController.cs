using ia_learning.Data;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ia_learning.OpenAI;

namespace ia_learning.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class AvaliacaoIAController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly OpenAIClientHelper _openAI;

        public AvaliacaoIAController(AppDbContext context, OpenAIClientHelper openAI)
        {
            _context = context;
            _openAI = openAI;
        }

        [HttpPost("feedback/{avaliacaoId}")]
        public async Task<IActionResult> GerarFeedback(int avaliacaoId)
        {
            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Usuario)
                .Include(a => a.IA)
                .FirstOrDefaultAsync(a => a.Id == avaliacaoId);

            if (avaliacao == null)
                return NotFound("Avaliação não encontrada.");

            var prompt = $@"
O usuário {avaliacao.Usuario.Nome} avaliou a IA {avaliacao.IA.Nome} com nota {avaliacao.Nota} 
e comentário: ""{avaliacao.Comentario}"".

Gere um feedback inteligente, curto e construtivo dizendo:
- como a IA pode ajudar melhor o usuário
- como o usuário pode obter melhores resultados no futuro.";

            var resposta = await _openAI.EnviarMensagem(prompt);

            return Ok(new
            {
                ia = avaliacao.IA.Nome,
                usuario = avaliacao.Usuario.Nome,
                feedback = resposta
            });
        }
    }
}
