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
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int page = 1,
            int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.Tarefas.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var tarefas = await _context.Tarefas
                .Include(t => t.Usuario)
                .Include(t => t.IA)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = LinkHelper.WithLinks(
                tarefas,
                Url,
                "GetTarefa",
                "UpdateTarefa",
                "DeleteTarefa",
                t => t.Id
            );

            var response = new
            {
                currentPage = page,
                pageSize,
                totalItems,
                totalPages,
                data
            };

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetTarefa")]
        public async Task<IActionResult> Get(int id)
        {
            var tarefa = await _context.Tarefas
                .Include(t => t.Usuario)
                .Include(t => t.IA)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return NotFound();

            var result = LinkHelper.WithLinks(
                tarefa,
                Url,
                "GetTarefa",
                "UpdateTarefa",
                "DeleteTarefa",
                tarefa.Id
            );

            return Ok(result);
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

            var result = LinkHelper.WithLinks(
                model,
                Url,
                "GetTarefa",
                "UpdateTarefa",
                "DeleteTarefa",
                model.Id
            );

            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateTarefa")]
        public async Task<IActionResult> Update(int id, Tarefa model)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return NotFound();

            tarefa.Descricao = model.Descricao;
            tarefa.UsuarioId = model.UsuarioId;
            tarefa.IAId = model.IAId;

            await _context.SaveChangesAsync();

            var result = LinkHelper.WithLinks(
                tarefa,
                Url,
                "GetTarefa",
                "UpdateTarefa",
                "DeleteTarefa",
                tarefa.Id
            );

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteTarefa")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
