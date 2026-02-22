using Microsoft.AspNetCore.Http;
using Shared.SharedKernel.Errors;

namespace Shared.Framework.Extensions;

public static class EnumExtensions
{
    public static int GetStatusCode(this ErrorType errorType)
    {
        int code = errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
        return code;
    } 
}