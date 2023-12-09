using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public interface IResult
    {
        bool Success { get; }
        string? Message { get; }
        string? ErrorCode { get; }
        HttpStatusCode? StatusCode { get; }
    }
}