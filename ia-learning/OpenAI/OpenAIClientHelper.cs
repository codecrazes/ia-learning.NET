using OpenAI.Chat;

namespace ia_learning.OpenAI
{
    public class OpenAIClientHelper
    {
        private readonly ChatClient _chatClient;

        public OpenAIClientHelper(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("Chave da OpenAI não encontrada. Configure 'OpenAI:ApiKey' no appsettings.json.");

            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
        }

        public async Task<string> EnviarMensagem(string prompt)
        {
            var resposta = await _chatClient.CompleteChatAsync(prompt);

            return resposta.Value.Content[0].Text;
        }
    }
}
