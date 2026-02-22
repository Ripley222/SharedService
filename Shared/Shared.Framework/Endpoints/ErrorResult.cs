using Microsoft.AspNetCore.Http;
using Shared.Framework.Extensions;
using Shared.SharedKernel;
using Shared.SharedKernel.Errors;

namespace Shared.Framework.Endpoints;

public sealed class ErrorResult : IResult
{
    private readonly ErrorList _errors;
    
    public ErrorResult(ErrorList errors)
    {
        _errors = errors;
    }
    
    public Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var distinctErrorTypes = _errors
            .Select(e => e.ErrorType)
            .Distinct()
            .ToList();
        
        int statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : _errors.First().ErrorType.GetStatusCode();
        
        Envelope envelope = Envelope.Error(_errors);
        httpContext.Response.StatusCode = statusCode;
        
        return httpContext.Response.WriteAsJsonAsync(envelope);
    }
}