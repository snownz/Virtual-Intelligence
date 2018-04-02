namespace VI.Test.StructuredRules.Tools
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string json);

        string Serialize<T>(T obj);
    }
}
