namespace lab07fabriciohancco.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            var errorResponse = new { message = "Ocurrió un error interno", details = ex.Message };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
