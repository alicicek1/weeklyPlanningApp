using System.Net;

namespace toDoApp.Api.Model.CommonResutlts
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        protected DataResult(T? data, bool success, string message, string errorCode,
            HttpStatusCode statusCode) : base(
            success, message, errorCode, statusCode)
        {
            Data = data;
        }

        protected DataResult(T? data, bool success, string message, HttpStatusCode statusCode) : base(
            success, message, statusCode)
        {
            Data = data;
        }

        public T? Data { get; }
    }
}