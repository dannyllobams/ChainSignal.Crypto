using System.Text.Json.Serialization;

namespace ChainSignal.Crypto.MarketInfo.CoinGecko.Model
{
    public sealed class CoinGeckoMarketItemDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string? Image { get; set; } = string.Empty;

        [JsonPropertyName("current_price")]
        public decimal? CurrentPrice { get; set; }

        [JsonPropertyName("market_cap")]
        public decimal? MarketCap { get; set; }

        [JsonPropertyName("market_cap_rank")]
        public int? MarketCapRank { get; set; }

        [JsonPropertyName("total_volume")]
        public decimal? TotalVolume { get; set; }

        [JsonPropertyName("high_24h")]
        public decimal? High24h { get; set; }

        [JsonPropertyName("low_24h")]
        public decimal? Low24h { get; set; }

        [JsonPropertyName("price_change_24h")]
        public decimal? PriceChange24h { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public decimal? PriceChangePercentage24h { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime? LastUpdatedUtc { get; set; }
    }
}
