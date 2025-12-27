using ChainSignal.Crypto.AI.API.Options;
using Microsoft.Extensions.AI;
using OpenAI;

namespace ChainSignal.Crypto.AI.API.Factory
{
    public static class ChatClientFactory
    {
        public static IChatClient CreateChatClient(AIOptions options)
        {
            return options.Provider switch
            {
                "OpenAI" => new OpenAIClient(options.ApiKey).GetChatClient(options.Model ?? "").AsIChatClient(),
                _ => throw new InvalidOperationException($"Unknown AI provider '{options.Provider}'.")
            };
        }
    }
}
