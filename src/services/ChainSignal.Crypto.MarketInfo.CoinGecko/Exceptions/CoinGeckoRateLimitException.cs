namespace ChainSignal.Crypto.MarketInfo.CoinGecko.Exceptions
{
    public sealed class CoinGeckoRateLimitException : Exception
    {
        public CoinGeckoRateLimitException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
