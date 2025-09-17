using System.Net;

namespace OKR.Domain.Bases;
public class ErrorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public string EventType { get; set; } = string.Empty;
}
