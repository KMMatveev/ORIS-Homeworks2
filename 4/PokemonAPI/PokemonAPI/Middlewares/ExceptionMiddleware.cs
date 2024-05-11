using PokemonAPI.Handlers;

namespace PokemonAPI.Middlewares;

/// <summary>
/// Middleware which responsible for Exception Handling
/// </summary>
public class ExceptionMiddleware: IMiddleware
{
    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await ExceptionHandler.HandleAsync(exception, context);
        }
    }
}