namespace Boxes.Exceptions;

public sealed class NoneValueException : Exception
{
    public NoneValueException() : base("Maybe was None during conversion to Result") { }
}
