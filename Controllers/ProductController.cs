using Microsoft.AspNetCore.Mvc;
using lab07fabriciohancco.DTOs;
using lab07fabriciohancco.UseCases.Products;

namespace lab07fabriciohancco.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly CrearProductoUseCase _crearProductoUseCase;
    private readonly ObtenerResumenProductosUseCase _obtenerResumenProductosUseCase;

    public ProductController(
        CrearProductoUseCase crearProductoUseCase,
        ObtenerResumenProductosUseCase obtenerResumenProductosUseCase)
    {
        _crearProductoUseCase = crearProductoUseCase;
        _obtenerResumenProductosUseCase = obtenerResumenProductosUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
    {
        var result = await _crearProductoUseCase.ExecuteAsync(product);
        return Ok(new { message = "Producto creado exitosamente mediante Caso de Uso.", data = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _obtenerResumenProductosUseCase.ExecuteAsync();
        return Ok(products);
    }

    [HttpGet("error-test")]
    public IActionResult ThrowError()
    {
        throw new Exception("Excepción simulada en el servidor.");
    }
}
