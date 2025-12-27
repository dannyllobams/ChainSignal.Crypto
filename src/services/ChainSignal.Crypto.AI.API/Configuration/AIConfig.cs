using ChainSignal.Crypto.AI.API.Factory;
using ChainSignal.Crypto.AI.API.Options;
using ChainSignal.Crypto.AI.API.Prompts;
using Microsoft.Extensions.AI;

namespace ChainSignal.Crypto.AI.API.Configuration
{
    public static class AIConfig
    {
        public static IServiceCollection AIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var aiSettingsSection = configuration.GetSection("AI");
            services.Configure<AIOptions>(aiSettingsSection);

            services.AddSingleton<IPromptCatalog, PromptCatalog>();
            services.AddScoped<IChatClient>(sp => ChatClientFactory.CreateChatClient(aiSettingsSection.Get<AIOptions>()!));

            return services;
        }
    }
}
