using lab07fabriciohancco.DTOs;
using lab07fabriciohancco.Domain.Entities;

namespace lab07fabriciohancco.UseCases.Products;

public class CrearProductoUseCase
{
    public Task<Product> ExecuteAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = new Random().Next(1, 1000),
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description
        };

        return Task.FromResult(product);
    }
}
