using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.SharedKernel;
using Shared.SharedKernel.Errors;

namespace Shared.Framework.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this ErrorList errorList)
    {
        if (!errorList.Any())
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        
        var distinctErrorTypes = errorList
            .Select(e => e.ErrorType)
            .Distinct()
            .ToList();
        
        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : distinctErrorTypes.First().GetStatusCode();

        var envelope = Envelope.Error(errorList);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
}