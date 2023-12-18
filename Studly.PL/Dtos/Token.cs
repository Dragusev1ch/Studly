namespace Studly.PL.Dtos;

public class Token
{
    public Token()
    {
    }

    public Token(string token)
    {
        Value = token;
    }

    public string Value { get; set; } = string.Empty;
}