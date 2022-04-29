using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Models.Dapr;
using Products.Utils;

namespace Products.Controllers;

[ApiController]
[Route("plain/products")]
public class ProductsController : ControllerBase
{
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<ProductsController> _logger;
    private readonly string _daprStoreName;

    public ProductsController(IHttpClientFactory factory, ILogger<ProductsController> logger)
    {
        _factory = factory;
        _logger = logger;
        _daprStoreName = Environment.GetEnvironmentVariable(Constants.DaprStoreNameVariable) ?? Constants.MongoStoreName;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllProducts()
    {
        using var client = _factory.CreateClient();

        var content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
        var result = await client.PostAsync($"http://localhost:3500/v1.0-alpha1/state/{_daprStoreName}/query", content);

        result.EnsureSuccessStatusCode();

        var stateQueryResponseString = await result.Content.ReadAsStringAsync();

        var stateQueryResponse = JsonSerializer.Deserialize<QueryResults<Product>>(stateQueryResponseString, Constants.SerializerOptions);
        var q = stateQueryResponse.Results.Select(a => a.Data).ToList();
        return Ok(q);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        var newProduct = new Product()
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Status = createProductDto.Status.Value,
            AvailabilityStart = createProductDto.AvailabilityStart,
            AvailabilityEnd = createProductDto.AvailabilityEnd,
        };

        var state = new CreateState<Product>()
        {
            Key = newProduct.Name,
            Value = newProduct,
        };

        var statesList = new List<CreateState<Product>>()
        {
            state,
        };

        using var client = _factory.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize<List<CreateState<Product>>>(statesList, Constants.SerializerOptions), System.Text.Encoding.UTF8, "application/json");
        var result = await client.PostAsync($"http://localhost:3500/v1.0/state/{_daprStoreName}", content);
        result.EnsureSuccessStatusCode();
        return Ok(state);
    }
}
