using System.Net;

namespace ChainSignal.Crypto.MarketInfo.CoinGecko.Exceptions
{
    public sealed class CoinGeckoRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? ResponseBody { get; }

        public CoinGeckoRequestException(string message, HttpStatusCode statusCode, string? responseBody, Exception? inner = null)
            : base(message, inner)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }
}
