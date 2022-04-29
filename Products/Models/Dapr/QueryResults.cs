using System.Text.Json.Serialization;

namespace Products.Models.Dapr;

public class QueryResults<T>
{
    [JsonPropertyName("results")]
    public List<QueryResult<T>> Results { get; set; }
}
