using lab07fabriciohancco.Middleware;
using lab07fabriciohancco.UseCases.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<CrearProductoUseCase>();
builder.Services.AddScoped<ObtenerResumenProductosUseCase>();

var app = builder.Build();

app.UseRouting();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RoleBasedAccessMiddleware>();
app.UseMiddleware<ParameterValidationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
