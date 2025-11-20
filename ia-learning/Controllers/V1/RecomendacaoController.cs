using ia_learning.Data;
using ia_learning.Models;
using ia_learning.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly OpenAIService _openAi;

        public RecomendacaoController(AppDbContext context, OpenAIService openAi)
        {
            _context = context;
            _openAi = openAi;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recs = await _context.Recomendacoes
                .Include(r => r.Usuario)
                .ToListAsync();

            return Ok(recs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rec = await _context.Recomendacoes
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);

            return rec == null ? NotFound() : Ok(rec);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Recomendacao model)
        {
            model.Data = DateTime.Now;

            _context.Recomendacoes.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost("gerar")]
        public async Task<IActionResult> GerarRecomendacao(int tarefaId)
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Id == tarefaId);

            if (tarefa == null)
                return BadRequest("Tarefa não encontrada.");

            var prompt = $@"
Você é uma IA especialista em aprendizagem profissional.

Com base na tarefa abaixo, gere uma recomendação detalhada, clara e objetiva.
Não gere textos gigantes, mas mantenha profundidade e qualidade.

Tarefa:
Título: {tarefa.Titulo}
Descrição: {tarefa.Descricao}
Dificuldade: {tarefa.Dificuldade}
Tempo disponível: {tarefa.TempoDisponivelMin} minutos
Usuário: {tarefa.Usuario.Nome} - {tarefa.Usuario.Email}

Regras:
- A recomendação deve ter alta qualidade e orientação prática.
- Não seja repetitivo.
- Evite introduções e conclusões longas.
- Tamanho ideal: entre 800 e 1500 caracteres.
- Se precisar encurtar, remova redundâncias, nunca conteúdo importante.
- Seja direto, estruturado e motivador.
";

            var respostaIA = await _openAi.GerarConteudoAsync(prompt);

            if (respostaIA.Length > 1800)
            {
                respostaIA = respostaIA.Substring(0, 1700).Trim();

                int ultimoPonto = respostaIA.LastIndexOf('.');
                if (ultimoPonto > 0)
                    respostaIA = respostaIA.Substring(0, ultimoPonto + 1);

                respostaIA += "...";
            }

            var rec = new Recomendacao
            {
                UsuarioId = tarefa.UsuarioId,
                Mensagem = respostaIA,
                Data = DateTime.Now
            };

            _context.Recomendacoes.Add(rec);
            await _context.SaveChangesAsync();

            return Ok(rec);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rec = await _context.Recomendacoes.FindAsync(id);
            if (rec == null) return NotFound();

            _context.Recomendacoes.Remove(rec);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
