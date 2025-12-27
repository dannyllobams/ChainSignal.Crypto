using ChainSignal.Crypto.AI.Domain.Model;
using ChainSignal.Crypto.MarketInfo.CoinGecko.Model;

namespace ChainSignal.Crypto.AI.API.Mappers
{
    public static class CoinGeckoMarketItemMapper
    {
        public static MarketAsset ToMarketAsset(CoinGeckoMarketItemDto item)
        {
            return new MarketAsset
            {
                AssetId = item.Id,
                Symbol = item.Symbol,
                Name = item.Name,
                ImageUrl = item.Image,
                MarketCapRank = item.MarketCapRank,
                Price = item.CurrentPrice,
                High24h = item.High24h,
                Low24h = item.Low24h,
                PriceChange24h = item.PriceChange24h,
                PriceChangePct24h = item.PriceChangePercentage24h,
                Volume24h = item.TotalVolume,
                MarketCap = item.MarketCap,
                LastUpdatedUtc = item.LastUpdatedUtc
            };
        }
    }
}
