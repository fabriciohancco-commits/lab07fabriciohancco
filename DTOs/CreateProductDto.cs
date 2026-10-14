using System.ComponentModel.DataAnnotations;

namespace lab07fabriciohancco.DTOs;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public string? Description { get; set; }
}
