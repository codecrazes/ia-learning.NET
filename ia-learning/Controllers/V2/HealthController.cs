using ia_learning.Data;
using ia_learning.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ia_learning.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/health")]
    public class HealthCheckController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly OpenAIService _openAi;

        public HealthCheckController(AppDbContext context, OpenAIService openAi)
        {
            _context = context;
            _openAi = openAi;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var health = new
            {
                api = "Healthy",
                database = await CheckDatabaseAsync(),
                openai = await CheckOpenAIAsync(),
                timestamp = DateTime.UtcNow
            };

            return Ok(health);
        }

        private async Task<string> CheckDatabaseAsync()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("SELECT 1 FROM DUAL");
                return "Connected";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        private async Task<string> CheckOpenAIAsync()
        {
            try
            {
                var resposta = await _openAi.GerarConteudoAsync("Teste rápido da OpenAI. Responda apenas 'ok'.");
                if (string.IsNullOrWhiteSpace(resposta))
                    return "Error: No response";

                return "Connected";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
