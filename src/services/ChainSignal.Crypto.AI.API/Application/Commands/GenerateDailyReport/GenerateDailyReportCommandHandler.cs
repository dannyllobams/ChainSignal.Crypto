using ChainSignal.Crypto.AI.API.Facade;
using ChainSignal.Crypto.AI.API.Prompts;
using ChainSignal.Crypto.AI.Domain.Model;
using ChainSignal.Crypto.Core.Messages;
using Cortex.Mediator.Commands;
using FluentValidation.Results;
using Microsoft.Extensions.AI;
using System;
using System.Text.Json;

namespace ChainSignal.Crypto.AI.API.Application.Commands.GenerateDailyReport
{
    public class GenerateDailyReportCommandHandler : CommandHandler, 
        ICommandHandler<GenerateDailyReportCommand, CommandResult<MarketReport>>
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly IChatClient _chatClient;
        private readonly IMarketInfoFacade _marketInfoFacade;
        private readonly IPromptCatalog _promptCatalog;

        public GenerateDailyReportCommandHandler(
            IChatClient chatClient,
            IMarketInfoFacade marketInfoFacade,
            IPromptCatalog promptCatalog)
        {
            _chatClient = chatClient;
            _marketInfoFacade = marketInfoFacade;
            _promptCatalog = promptCatalog;
        }

        public async Task<CommandResult<MarketReport>> Handle(GenerateDailyReportCommand request, CancellationToken cancellationToken)
        {
            var prompt = _promptCatalog.Get("DailyCryptoReport", 1);
            var marketInfo = await _marketInfoFacade.GetMarketInfoAsync(cancellationToken);


            var apiJson = JsonSerializer.Serialize(marketInfo);

            var json = await CompleteAsync(_chatClient, prompt.System, Render(prompt.UserTemplate, ("apiJson", apiJson)), temperature: 0.2f, cancellationToken);

            if (TryDeserialize(json, out MarketReport? report))
            {
                return Result(report);
            }
            else
            {
                var repairPrompt = _promptCatalog.Get("JsonRepair_CryptoDailyReport", 1);

                var fixedJson = await CompleteAsync(
                    _chatClient,
                    repairPrompt.System,
                    Render(repairPrompt.UserTemplate, ("invalidJson", json)),
                    temperature: 0.0f,
                    cancellationToken);

                if (TryDeserialize(fixedJson, out report))
                    return Result(report);

                this.AddError("LLM returned invalid JSON for CryptoDailyReport after one retry.");
            }

            return Result<MarketReport>();
        }

        private static async Task<string> CompleteAsync(
        IChatClient chat,
        string system,
        string user,
        float temperature,
        CancellationToken cancellationToken)
        {
            var options = new ChatOptions { Temperature = temperature };

            var completion = await chat.GetResponseAsync(
                new List<ChatMessage>() { new ChatMessage(ChatRole.System, system), new ChatMessage(ChatRole.User, user) },
                options,
                cancellationToken);

            if (string.IsNullOrWhiteSpace(completion.Text))
                throw new InvalidOperationException("LLM returned empty content.");

            return completion.Text.Trim();
        }

        private static bool TryDeserialize<T>(string json, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(json, JsonOptions);
                return result is not null;
            }
            catch
            {
                result = default;
                return false;
            }
        }

    private static string Render(string template, params (string Key, string Value)[] values)
    {
        foreach (var (key, value) in values)
            template = template.Replace($"{{{{{key}}}}}", value);

        return template;
    }
    }
}
