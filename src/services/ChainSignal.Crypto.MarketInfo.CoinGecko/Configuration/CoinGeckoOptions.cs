namespace ChainSignal.Crypto.MarketInfo.CoinGecko.Configuration
{
    public sealed class CoinGeckoOptions
    {
        public string BaseUrl { get; init; } = "https://api.coingecko.com/";
        public string? ApiKey { get; init; }
        public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(20);
        public string UserAgent { get; init; } = "ChainSignal.Crypto/1.0";
    }
}
