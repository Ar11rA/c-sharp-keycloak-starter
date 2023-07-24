using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sample.Tests.Utils.DTO;

namespace Sample.Tests.Utils.Clients.Common
{
    public class ClientResponse<T>
        where T : class
    {
        public HttpStatusCode StatusCode { get; }

        public bool Successful { get; }

        public T Data => _content.Value.Data;

        public Error Error => _content.Value.Error;

        //Response from Service
        private readonly Lazy<ServiceResponse<T>> _content;

        //Configuring JsonSerializer for the Response Content
        private static readonly JsonSerializerOptions Settings = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        //Form Error Response with Status Code and Error Details
        public ClientResponse(HttpStatusCode statusCode, Error error)
        {
            StatusCode = statusCode;
            Successful = false;
            _content = new Lazy<ServiceResponse<T>>(() => new ServiceResponse<T> { Error = error });
        }

        //Form Success Response with Status Code and Data
        public ClientResponse(HttpStatusCode statusCode, T? data = null)
        {
            StatusCode = statusCode;
            Successful = true;
            _content = new Lazy<ServiceResponse<T>>(() => new ServiceResponse<T> { Data = data });
        }

        //Form Client Response with HttpResponse returned by other services
        public ClientResponse(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode;
            Successful = response.IsSuccessStatusCode
                         || response.StatusCode == HttpStatusCode.SeeOther
                         || response.StatusCode == HttpStatusCode.NotModified;

            using (response.Content)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                _content = new Lazy<ServiceResponse<T>>(() =>
                {
                    if (TryParseResponse(json, out var result))
                    {
                        return result!;
                    }

                    return new ServiceResponse<T> { Error = new Error(ErrorCodes.UnreadableResponse.Code, json) };
                });
            }
        }

        //Helper method to parse the Service response content
        private static bool TryParseResponse(string content, out ServiceResponse<T>? result)
        {
            try
            {
                result = string.IsNullOrEmpty(content)
                    ? new ServiceResponse<T>()
                    : JsonSerializer.Deserialize<ServiceResponse<T>>(content, Settings);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}