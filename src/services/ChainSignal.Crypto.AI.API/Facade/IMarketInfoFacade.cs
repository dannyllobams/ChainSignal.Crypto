using ChainSignal.Crypto.AI.Domain.Model;

namespace ChainSignal.Crypto.AI.API.Facade
{
    public interface IMarketInfoFacade
    {
        Task<MarketSnapshot> GetMarketInfoAsync(CancellationToken cancellationToken = default);
    }
}
