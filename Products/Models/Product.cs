using System.Text.Json.Serialization;

namespace Products.Models;

public class Product
{
    public string Name { get; set; }

    public string Description { get; set; }

    [JsonPropertyName("status")]
    public ProductStatus Status { get; set; }

    public DateTime? AvailabilityStart { get; set; }

    public DateTime? AvailabilityEnd { get; set; }
}
