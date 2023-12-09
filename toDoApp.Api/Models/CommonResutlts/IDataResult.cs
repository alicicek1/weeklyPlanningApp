namespace toDoApp.Api.Model.CommonResutlts
{
    public interface IDataResult<out T> : IResult
    {
        T? Data { get; }
    }
}