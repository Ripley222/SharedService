using Microsoft.AspNetCore.Builder;
using Shared.Framework.Middlewares;

namespace Shared.Framework.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}