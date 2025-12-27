using ChainSignal.Crypto.AI.API.Mappers;
using ChainSignal.Crypto.AI.Domain.Model;
using ChainSignal.Crypto.MarketInfo.CoinGecko.Services;

namespace ChainSignal.Crypto.AI.API.Facade
{
    public class MarketInfoFacade : IMarketInfoFacade
    {
        private readonly ICoinGeckoMarketInfoService _coinGeckoMarketInfoService;
        public MarketInfoFacade(ICoinGeckoMarketInfoService coinGeckoMarketInfoService)
        {
            _coinGeckoMarketInfoService = coinGeckoMarketInfoService;
        }

        public async Task<MarketSnapshot> GetMarketInfoAsync(CancellationToken cancellationToken = default)
        {
            var coinGeckoMarketItens = await _coinGeckoMarketInfoService.GetMarketDataAsync(cancellationToken: cancellationToken);

            return new MarketSnapshot
            {
                QuoteCurrency = "usd",
                CapturedAtUtc = DateTime.UtcNow,
                Assets = coinGeckoMarketItens.Select(CoinGeckoMarketItemMapper.ToMarketAsset).ToList()
            };
        }
    }
}
