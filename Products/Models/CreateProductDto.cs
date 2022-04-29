using System.ComponentModel.DataAnnotations;

namespace Products.Models;

public class CreateProductDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public ProductStatus? Status { get; set; }

    public DateTime? AvailabilityStart { get; set; }

    public DateTime? AvailabilityEnd { get; set; }
}
