using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Utils;

namespace Products.Controllers;

[ApiController]
[Route("sdk/products")]
public class ProductsSdkController : ControllerBase
{
    private readonly ILogger<ProductsSdkController> _logger;
    private readonly string _daprStoreName;

    public ProductsSdkController(ILogger<ProductsSdkController> logger)
    {
        _logger = logger;
        _daprStoreName = Environment.GetEnvironmentVariable(Constants.DaprStoreNameVariable) ?? Constants.MongoStoreName;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllProducts()
    {
        using var client = new DaprClientBuilder().UseJsonSerializationOptions(Constants.SerializerOptions).Build();

        var stateQueryResponse = await client.QueryStateAsync<Product>(_daprStoreName, "{}");
        var products = stateQueryResponse.Results.Select(stateResponse => stateResponse.Data).ToList();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        using var client = new DaprClientBuilder().UseJsonSerializationOptions(Constants.SerializerOptions).Build();

        var state = new Product()
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Status = createProductDto.Status.Value,
            AvailabilityStart = createProductDto.AvailabilityStart,
            AvailabilityEnd = createProductDto.AvailabilityEnd,
        };
        
        await client.SaveStateAsync(_daprStoreName, state.Name, state);
        return Ok(state);
    }
}
