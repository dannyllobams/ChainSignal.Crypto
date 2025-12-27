namespace ChainSignal.Crypto.AI.Domain.Model
{
    public sealed class AssetHighlight
    {
        public required string Symbol { get; init; }
        public required string Name { get; init; }
        public required decimal PriceUsd { get; init; }
        public required decimal Change24hPct { get; init; }
        public required string Note { get; init; }
    }
}
