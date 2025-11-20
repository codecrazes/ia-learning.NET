using ia_learning.Controllers.V1;
using ia_learning.Data;
using ia_learning.DTOs;
using ia_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ia_learning.Tests
{
    public class TarefaControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDB_" + Guid.NewGuid())
                .Options;

            var ctx = new AppDbContext(options);

            ctx.Usuarios.Add(new Usuario
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com"
            });

            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public async Task Post_DeveCriarTarefa()
        {
            var ctx = GetDbContext();
            var controller = new TarefaController(ctx);

            var dto = new TarefaCreateDto
            {
                Titulo = "Estudar",
                Descricao = "Aprender IA",
                Dificuldade = 2,        // CORRIGIDO
                TempoDisponivelMin = 30,
                UsuarioId = 1
            };

            var result = await controller.Create(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var tarefa = Assert.IsType<Tarefa>(ok.Value);
            Assert.Equal("Estudar", tarefa.Titulo);
        }

        [Fact]
        public async Task Delete_DeveRemoverTarefa()
        {
            var ctx = GetDbContext();

            ctx.Tarefas.Add(new Tarefa
            {
                Id = 1,
                Titulo = "Tarefa teste",
                Descricao = "Algo",
                Dificuldade = 1,    
                TempoDisponivelMin = 10,
                UsuarioId = 1
            });

            ctx.SaveChanges();

            var controller = new TarefaController(ctx);

            var result = await controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
            Assert.False(ctx.Tarefas.Any(t => t.Id == 1));
        }
    }
}
