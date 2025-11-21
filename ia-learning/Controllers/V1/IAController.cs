using ia_learning.Data;
using ia_learning.Hateoas;
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
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.IAs.ToListAsync();

            var response = LinkHelper.WithLinks(
                list,
                Url,
                "GetIA",
                "UpdateIA",
                "DeleteIA",
                i => i.Id
            );

            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetIA")]
        public async Task<IActionResult> Get(int id)
        {
            var ia = await _context.IAs.FirstOrDefaultAsync(i => i.Id == id);

            if (ia == null)
                return NotFound();

            var result = LinkHelper.WithLinks(
                ia,
                Url,
                "GetIA",
                "UpdateIA",
                "DeleteIA",
                id
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IA model)
        {
            _context.IAs.Add(model);
            await _context.SaveChangesAsync();

            var result = LinkHelper.WithLinks(
                model,
                Url,
                "GetIA",
                "UpdateIA",
                "DeleteIA",
                model.Id
            );

            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateIA")]
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

            var result = LinkHelper.WithLinks(
                ia,
                Url,
                "GetIA",
                "UpdateIA",
                "DeleteIA",
                ia.Id
            );

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteIA")]
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
