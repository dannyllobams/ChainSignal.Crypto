using ChainSignal.Crypto.AI.API.Facade;
using ChainSignal.Crypto.Core.Mediator;
using ChainSignal.Crypto.MarketInfo.CoinGecko.Configuration;
using ChainSignal.Crypto.MarketInfo.CoinGecko.Services;
using Cortex.Mediator.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ChainSignal.Crypto.AI.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterCoinGeckoService(services);

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IMarketInfoFacade, MarketInfoFacade>();


            services.AddCortexMediator(
                configuration: configuration,
                handlerAssemblyMarkerTypes: new[] { typeof(Program) },
                configure: options =>
                {
                    options.AddDefaultBehaviors();
                }
            );
        }

        private static void RegisterCoinGeckoService(IServiceCollection services)
        {
            services.AddSingleton<CoinGeckoOptions>(new CoinGeckoOptions());
            services.AddHttpClient<ICoinGeckoMarketInfoService, CoinGeckoMarketInfoService>();
        }
    }
}
