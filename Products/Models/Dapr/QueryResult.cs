using System.Text.Json.Serialization;

namespace Products.Models.Dapr;

public class QueryResult<T>
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("data")]
    public T Data { get; set; }
}
