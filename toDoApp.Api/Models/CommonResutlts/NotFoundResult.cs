using System.Net;

namespace toDoApp.Api.Model.CommonResutlts;

public class NotFoundResult<T> : DataResult<T>
{
    public NotFoundResult(string errorCode) : base(default, true, "Not found.", errorCode,
        HttpStatusCode.OK)
    {
    }
}