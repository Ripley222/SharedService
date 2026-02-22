using System.Text.Json.Serialization;
using Shared.SharedKernel.Errors;

namespace Shared.SharedKernel;

public record Envelope
{
    public object? Result { get; }
    
    public ErrorList? Errors { get; }
    
    public DateTime TimeGenerated { get; }

    [JsonConstructor]
    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(object? result = null) => new(result, null);
    
    public static Envelope Error(ErrorList errorList) => new(null, errorList);
}

public record Envelope<T>
{
    public T? Result { get; }
    
    public ErrorList? Errors { get; }
    
    public DateTime TimeGenerated { get; }

    [JsonConstructor]
    private Envelope(T? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope<T> Ok(T? result = default) => new(result, null);
    
    public static Envelope<T> Error(ErrorList errorList) => new(default, errorList);
}