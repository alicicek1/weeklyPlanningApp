namespace toDoApp.Api.Models.Log;

public class CustomLoggingModel
{
    public string? CorrelationId { get; set; }
    public int StatusCode { get; set; }
    public string? HttpMethod { get; set; }
    public string? Path { get; set; }
    public string? ResponseBody { get; set; }
}