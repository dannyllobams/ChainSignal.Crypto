namespace ChainSignal.Crypto.AI.API.Prompts
{
    public class PromptCatalog : IPromptCatalog
    {
        private static readonly IReadOnlyDictionary<(string Name, int Version), PromptDefinition> Prompts
                = new Dictionary<(string, int), PromptDefinition>
                {
                    {
                        ("DailyCryptoReport", 1),
                        new PromptDefinition(
                            Name: "DailyCryptoReport",
                            Version: 1,
                            System: """
                                You are a senior software engineer generating a DAILY CRYPTO MARKET REPORT.
                                Rules:
                                - Use ONLY the provided JSON data.
                                - Do NOT invent facts or prices.
                                - If some value is missing, say "unknown".
                                - Output MUST be valid JSON only (no markdown, no extra text).
                            """,
                            UserTemplate: """
                                Given this API JSON payload, generate a concise daily report.

                                Output JSON schema:
                                {
                                  "title": "string",
                                  "dateUtc": "YYYY-MM-DD",
                                  "summary": "string",
                                  "keyTakeaways": ["string", ...],
                                  "highlights": [
                                    { "symbol": "string", "name": "string", "priceUsd": number, "change24hPct": number, "note": "string" }
                                  ],
                                  "disclaimer": "string"
                                }

                                Constraints:
                                - 4 to 6 keyTakeaways
                                - 5 highlights max
                                - "note" must reference only values present in the data (e.g., 24h change, volume, market cap rank).

                                API JSON:
                                {{apiJson}}
                            """
                        )
                    }
                };

        public PromptDefinition Get(string name, int version)
        {
            if (Prompts.TryGetValue((name, version), out var prompt))
                return prompt;

            throw new KeyNotFoundException($"Prompt '{name}' v{version} not found.");
        }
    }
}
