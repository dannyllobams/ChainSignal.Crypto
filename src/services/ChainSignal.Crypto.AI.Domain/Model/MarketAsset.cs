namespace ChainSignal.Crypto.AI.Domain.Model
{
    public sealed class MarketAsset
    {
        public required string AssetId { get; init; }
        public required string Symbol { get; init; }
        public required string Name { get; init; }
        public string? ImageUrl { get; init; }
        public int? MarketCapRank { get; init; }
        public decimal? Price { get; init; }
        public decimal? High24h { get; init; }
        public decimal? Low24h { get; init; }
        public decimal? PriceChange24h { get; init; }
        public decimal? PriceChangePct24h { get; init; }
        public decimal? Volume24h { get; init; }
        public decimal? MarketCap { get; init; }
        public DateTime? LastUpdatedUtc { get; init; }
    }
}
