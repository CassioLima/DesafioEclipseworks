namespace Infra
{
    public interface IApiClient
    {
        ApiClient AddHeader(string key, string value);
        Task<T> Get<T>(string endpoint) where T : class;
        Task<T> Get<T>(string endpoint, object id) where T : class;
    }
}