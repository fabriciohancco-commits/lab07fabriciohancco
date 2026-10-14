using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace lab07fabriciohancco.Middleware;

public class ParameterValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ParameterValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            var model = await DeserializeRequestBody(context);
            if (model == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Cuerpo de la solicitud inválido o vacío.");
                return;
            }

            var validationErrors = ValidateModel(model);
            if (validationErrors.Any())
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(string.Join("\n", validationErrors));
                return;
            }
        }

        await _next(context);
    }

    private async Task<object?> DeserializeRequestBody(HttpContext context)
    {
        var contentType = context.Request.ContentType;
        if (contentType != null && contentType.Contains("application/json"))
        {
            context.Request.EnableBuffering();

            var dtoType = context.GetEndpoint()?.Metadata?.OfType<ControllerActionDescriptor>()
                .FirstOrDefault()?.MethodInfo?.GetParameters().FirstOrDefault()?.ParameterType;

            if (dtoType != null)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var model = await JsonSerializer.DeserializeAsync(context.Request.Body, dtoType, options);
                context.Request.Body.Position = 0;
                return model;
            }
        }

        return null;
    }

    private List<string> ValidateModel(object model)
    {
        var errors = new List<string>();
        var properties = model.GetType().GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(model);
            if (value == null)
            {
                errors.Add($"El parámetro '{property.Name}' es obligatorio.");
            }
        }

        return errors;
    }
}
