namespace ChainSignal.Crypto.AI.API.Prompts
{
    public interface IPromptCatalog
    {
        PromptDefinition Get(string name, int version);
    }
}
