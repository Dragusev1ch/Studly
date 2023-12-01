using System.Text.Json;

namespace Studly.BLL.DTO;

public class ErrorDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ExceptionMessage { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}