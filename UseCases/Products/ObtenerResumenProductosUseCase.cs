using lab07fabriciohancco.Domain.Entities;

namespace lab07fabriciohancco.UseCases.Products;

public class ObtenerResumenProductosUseCase
{
    public Task<List<Product>> ExecuteAsync()
    {
        var list = new List<Product>
        {
            new Product { Id = 1, Name = "Teclado Mecánico", Price = 150.00m, Description = "Teclado RGB" },
            new Product { Id = 2, Name = "Mouse Gamer", Price = 80.00m, Description = "Mouse ergonómico" }
        };

        return Task.FromResult(list);
    }
}
