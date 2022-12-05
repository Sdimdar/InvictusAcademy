namespace Payment.Domain.Exceptions;

public class ValidationDataException : ApplicationException
{
    public ValidationDataException() { }
    public ValidationDataException(string message) : base(message) { }
}