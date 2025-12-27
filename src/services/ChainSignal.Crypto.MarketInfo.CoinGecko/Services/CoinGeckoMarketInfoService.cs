using ChainSignal.Crypto.MarketInfo.CoinGecko.Configuration;
using ChainSignal.Crypto.MarketInfo.CoinGecko.Exceptions;
using ChainSignal.Crypto.MarketInfo.CoinGecko.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChainSignal.Crypto.MarketInfo.CoinGecko.Services
{
    public class CoinGeckoMarketInfoService : ICoinGeckoMarketInfoService
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _http;
        private readonly CoinGeckoOptions _options;
        public CoinGeckoMarketInfoService(HttpClient http, CoinGeckoOptions options)
        {
            _http = http;
            _options = options;

            _http.BaseAddress ??= new Uri(_options.BaseUrl, UriKind.Absolute);
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _http.Timeout = _options.Timeout;

            if (!_http.DefaultRequestHeaders.UserAgent.Any())
                _http.DefaultRequestHeaders.UserAgent.ParseAdd(_options.UserAgent);
        }

        public async Task<IEnumerable<CoinGeckoMarketItemDto>> GetMarketDataAsync(
            string vsCurrency = "usd",             
            int perPage = 25, 
            int page = 1, 
            CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrWhiteSpace(vsCurrency)) throw new ArgumentException("vsCurrency is required.", nameof(vsCurrency));
            if (perPage is < 1 or > 250) throw new ArgumentOutOfRangeException(nameof(perPage), "perPage must be between 1 and 250.");
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page), "page must be >= 1.");

            var uri = BuildMarketsUri(vsCurrency, perPage, page);

            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            if (!string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                request.Headers.TryAddWithoutValidation("x-cg-pro-api-key", _options.ApiKey);
            }

            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (response.StatusCode == (HttpStatusCode)429)
                throw new CoinGeckoRateLimitException("CoinGecko rate limit reached (HTTP 429).");

            if (!response.IsSuccessStatusCode)
            {
                var body = await SafeReadBodyAsync(response, cancellationToken);
                throw new CoinGeckoRequestException(
                    $"CoinGecko request failed (HTTP {(int)response.StatusCode} {response.ReasonPhrase}).",
                    response.StatusCode,
                    body);
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var items = await JsonSerializer.DeserializeAsync<List<CoinGeckoMarketItemDto>>(stream, JsonOptions, cancellationToken)
                        ?? new List<CoinGeckoMarketItemDto>();

            return items;
        }

        private string BuildMarketsUri(string vsCurrency, int perPage, int page)
        {
            var query =
                $"api/v3/coins/markets" +
                $"?vs_currency={Uri.EscapeDataString(vsCurrency)}" +
                $"&order=market_cap_desc" +
                $"&per_page={perPage}" +
                $"&page={page}" +
                $"&sparkline=false" +
                $"&price_change_percentage=24h";

            return query;
        }

        private static async Task<string?> SafeReadBodyAsync(HttpResponseMessage response, CancellationToken ct)
        {
            try { return await response.Content.ReadAsStringAsync(ct); }
            catch { return null; }
        }
    }
}
