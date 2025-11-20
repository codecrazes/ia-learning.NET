using ia_learning.Data;
using ia_learning.DTOs;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _context.Tarefas
                .Include(t => t.Usuario)
                .Include(t => t.IA)
                .ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .Include(t => t.IA)
                .FirstOrDefaultAsync(t => t.Id == id);

            return tarefa == null ? NotFound() : Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TarefaCreateDto dto)
        {
            var model = new Tarefa
            {
                Titulo = dto.Titulo,
                Dificuldade = dto.Dificuldade,
                TempoDisponivelMin = dto.TempoDisponivelMin,
                Descricao = dto.Descricao,
                UsuarioId = dto.UsuarioId,
                IAId = dto.IAId,
                DataExecucao = DateTime.Now
            };

            _context.Tarefas.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tarefa model)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();

            tarefa.Descricao = model.Descricao;
            tarefa.UsuarioId = model.UsuarioId;
            tarefa.IAId = model.IAId;

            await _context.SaveChangesAsync();
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
