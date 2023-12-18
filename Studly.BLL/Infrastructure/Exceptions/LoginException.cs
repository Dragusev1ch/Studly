namespace Studly.BLL.Infrastructure.Exceptions;

public class LoginException : Exception
{
    public string MessageForUser { get; set; }

    public LoginException(string exceptionMessage, string messageForUser) : base(exceptionMessage)
    {
        MessageForUser = messageForUser;
    }
}