using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Shared.SharedKernel.Errors;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Shared.Framework.Endpoints;

public sealed class EndpointResult<TValue> : IResult
{
    private readonly IResult _result;

    public EndpointResult(Result<TValue, ErrorList> result)
    {
        _result = result.IsSuccess
            ? new SuccessResult<TValue>(result.Value)
            : new ErrorResult(result.Error);
    }

    public EndpointResult(UnitResult<ErrorList> result)
    {
        _result = result.IsSuccess
            ? new SuccessResult<bool>(true)
            : new ErrorResult(result.Error);
    }

    public Task ExecuteAsync(HttpContext httpContext) => _result.ExecuteAsync(httpContext);

    public static implicit operator EndpointResult<TValue>(Result<TValue, ErrorList> result) => new(result);

    public static EndpointResult<TValue> ToEndpointResult(Result<TValue, ErrorList> result) => new(result);
}