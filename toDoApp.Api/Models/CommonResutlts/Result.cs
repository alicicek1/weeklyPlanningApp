using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public class Result : IResult
    {
        protected Result(bool success, string message, string errorCode,
            HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
            Success = success;
            ErrorCode = errorCode;
        }

        protected Result(bool success, string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
            Success = success;
        }

        public bool Success { get; }
        public string? Message { get; }
        public string? ErrorCode { get; }
        public HttpStatusCode? StatusCode { get; }
    }
}