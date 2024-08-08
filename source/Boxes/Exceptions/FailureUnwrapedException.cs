namespace Boxes.Exceptions;

public sealed class FailureUnwrapedException : Exception
{
    public object? ErrorData => Data.Contains("ErrorData") ? Data["ErrorData"] : null;

    public FailureUnwrapedException(string message) : base($"A failure was unwrapped without handling it: {message}")
    {
    }

    public FailureUnwrapedException(string message, Exception innerException) : base($"A failure was unwrapped without handling it: {message}", innerException)
    {
    }

    public FailureUnwrapedException(string message, object data) : base($"A failure was unwrapped without handling it: {message}")
    {
        Data["ErrorData"] = data;
    }
}
