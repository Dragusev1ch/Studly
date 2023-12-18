namespace Studly.BLL.Infrastructure.Exceptions;

public class ValidationException : Exception
{
    public string MessageForUser { get; protected set; }

    public ValidationException(string exceptionMessage, string messageForUser) : base(exceptionMessage)
    {
        MessageForUser = messageForUser;
    }
}