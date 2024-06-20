namespace Application.Interfaces;

public interface IAPiClient
{
    Task<object> Get(string url);
    Task<HttpResponseMessage> Post(string url, object data);
}