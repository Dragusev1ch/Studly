namespace Studly.BLL.Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public string MessageForUser { get; set; }

    public NotFoundException(string exceptionMessage, string messageForUser) : base(exceptionMessage)
    {
        MessageForUser = messageForUser;
    }
}