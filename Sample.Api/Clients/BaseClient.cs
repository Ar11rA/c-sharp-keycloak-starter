using System;
using Sample.Api.DTO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sample.Api.Clients
{
    public abstract class BaseClient
    {
        protected const string UserAgentHeaderValue = ".NET SDK";

        protected const string AcceptLanguageHeaderValue = "en-US";

        protected const string ApplicationJsonContentType = "application/json";

        protected HttpClient Client { get; }

        //JSON Serializer Configuration for the Payload content
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        protected BaseClient(HttpClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        //Invocation for each of the HTTP Methods

        protected async Task<ClientResponse<T>> GetAsync<T>(string relativeUrl)
          where T : class
        {
            var response = await Client.GetAsync(FormatFullApiUrl(relativeUrl));
            return new ClientResponse<T>(response);
        }

        protected async Task<ClientResponse<TOut>> PostAsync<TIn, TOut>(string relativeUrl, TIn item)
          where TOut : class
          where TIn : notnull
        {
            var response = await Client.PostAsync(FormatFullApiUrl(relativeUrl), GetHttpContent(item));
            return new ClientResponse<TOut>(response);
        }

        //PUT operation for Null and Non-Null Body Types

        protected async Task<ClientResponse<TOut>> PutAsync<TIn, TOut>(string relativeUrl, TIn item)
          where TOut : class
          where TIn : notnull
        {
            var response = await Client.PutAsync(FormatFullApiUrl(relativeUrl), GetHttpContent(item));
            return new ClientResponse<TOut>(response);
        }

        protected async Task<ClientResponse<T>> PutAsync<T>(string relativeUrl)
          where T : class
        {
            var response = await Client.PutAsync(FormatFullApiUrl(relativeUrl), null);
            return new ClientResponse<T>(response);
        }

        protected async Task<ClientResponse<T>> DeleteAsync<T>(string relativeUrl)
          where T : class
        {
            var response = await Client.DeleteAsync(FormatFullApiUrl(relativeUrl));
            return new ClientResponse<T>(response);
        }

        //Resolving full URL for API invocation
        private Uri FormatFullApiUrl(string relativeUrl)
        {
            if (Client.BaseAddress == null)
            {
                throw new NullReferenceException("HttpClient's BaseAddress can't be null.");
            }

            var fullUrl = Client.BaseAddress.ToString().TrimEnd('/') + "/" + relativeUrl.TrimStart('/');
            return new Uri(fullUrl, UriKind.Absolute);
        }

        //Generating HttpContent for the payload object
        private HttpContent GetHttpContent(object obj)
        {
            if (obj is MultipartFormDataContent content)
            {
                return content;
            }

            var json = JsonSerializer.Serialize(obj, JsonOptions);
            return new StringContent(json, Encoding.UTF8, ApplicationJsonContentType);
        }
    }
}
