namespace toDoApp.Command.Client;

public interface IHttpClient
{
    Task<T?> GetAsync<T>(string url);
}