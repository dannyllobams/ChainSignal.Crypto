using ChainSignal.Crypto.MarketInfo.CoinGecko.Model;

namespace ChainSignal.Crypto.MarketInfo.CoinGecko.Services
{
    public interface ICoinGeckoMarketInfoService
    {
        Task<IEnumerable<CoinGeckoMarketItemDto>> GetMarketDataAsync(
            string vsCurrency = "usd",
            int perPage = 25,
            int page = 1, 
            CancellationToken cancellationToken = default);
    }
}
