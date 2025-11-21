using OpenAI.Chat;
using Microsoft.Extensions.Configuration;

namespace ia_learning.OpenAI
{
    public class OpenAIClientHelper
    {
        private readonly ChatClient _client;

        public OpenAIClientHelper(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("Chave da OpenAI não encontrada. Configure 'OpenAI:ApiKey' no appsettings.json.");

            _client = new ChatClient(
                model: "gpt-4o-mini",
                apiKey: apiKey
            );
        }

        public async Task<string> EnviarMensagem(string prompt)
        {
            var response = await _client.CompleteChatAsync(new[]
            {
                new UserChatMessage(prompt)
            });

            return response.Value.Content[0].Text;
        }
    }
}
