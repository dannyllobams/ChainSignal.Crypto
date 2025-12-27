namespace ChainSignal.Crypto.AI.Domain.Model
{
    public sealed class MarketReport
    {
        public required string Title { get; init; }
        public required string DateUtc { get; init; }
        public required string Summary { get; init; }
        public required List<string> KeyTakeaways { get; init; }
        public required List<AssetHighlight> Highlights { get; init; }
        public required string Disclaimer { get; init; }
    }
}
