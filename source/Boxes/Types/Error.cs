namespace Boxes.Types;

public abstract record Error(string Message)
{
    public static implicit operator Error(string message) => new SimpleError(message);
    public static implicit operator Error(Exception exception) => new ExceptionalError(exception.Message, exception);
}

public sealed record ExceptionalError(string Message, Exception Exception) : Error(Message);
public sealed record SimpleError(string Message) : Error(Message);
public abstract record DetailedError(string Message, object Data) : Error(Message);
