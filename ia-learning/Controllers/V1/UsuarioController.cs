using ia_learning.Data;
using ia_learning.Hateoas;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
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

            var totalItems = await _context.Usuarios.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var usuarios = await _context.Usuarios
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = LinkHelper.WithLinks(
                usuarios,
                Url,
                "GetUsuario",
                "UpdateUsuario",
                "DeleteUsuario",
                u => u.Id
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

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            var result = LinkHelper.WithLinks(
                usuario,
                Url,
                "GetUsuario",
                "UpdateUsuario",
                "DeleteUsuario",
                usuario.Id
            );

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var result = LinkHelper.WithLinks(
                usuario,
                Url,
                "GetUsuario",
                "UpdateUsuario",
                "DeleteUsuario",
                usuario.Id
            );

            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateUsuario")]
        public async Task<IActionResult> Update(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;

            await _context.SaveChangesAsync();

            var result = LinkHelper.WithLinks(
                usuario,
                Url,
                "GetUsuario",
                "UpdateUsuario",
                "DeleteUsuario",
                usuario.Id
            );

            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteUsuario")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
