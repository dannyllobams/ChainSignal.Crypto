namespace ChainSignal.Crypto.AI.Domain.Model
{
    public sealed class MarketSnapshot
    {
        public required string QuoteCurrency { get; init; }
        public required DateTime CapturedAtUtc { get; init; }

        public required IReadOnlyList<MarketAsset> Assets { get; init; }
    }
}
