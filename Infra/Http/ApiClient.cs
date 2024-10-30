using System.Net;
using System.Net.Http.Headers;
using Desafio.Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Infra
{
    public class ApiClient : IApiClient
    {
        private string baseUrl = "";
        private Dictionary<string, string> headers = new Dictionary<string, string>();
        private bool useDefaultCredentials = false;

        public ApiClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        private HttpClientHandler GetHandler()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            clientHandler.UseDefaultCredentials = useDefaultCredentials;

            return clientHandler;
        }

        public ApiClient AddHeader(string key, string value)
        {
            headers.Add(key, value);
            return this;
        }

        public async Task<T> Get<T>(string endpoint) where T : class
        {
            using (HttpClient client = new HttpClient(GetHandler()))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                headers.ForEach((key, value) => client.DefaultRequestHeaders.Add(key, value));
                try
                {
                    var result = client.GetAsync(endpoint).Result;
                    result.EnsureSuccessStatusCode();

                    return await result.Content.ReadAsAsync<T>();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<T> Get<T>(string endpoint, object id) where T : class
        {
            using (HttpClient client = new HttpClient(GetHandler()))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                headers.ForEach((key, value) => client.DefaultRequestHeaders.Add(key, value));

                var result = client.GetAsync(endpoint + "/" + id).Result;
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<T>();
            }
        }
    }
}