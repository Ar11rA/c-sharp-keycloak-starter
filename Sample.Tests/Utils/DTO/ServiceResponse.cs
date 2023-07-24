namespace Sample.Tests.Utils.DTO
{
    public class ServiceErrorResponse
    {
        public Error? Error { get; set; }
    }

    public sealed class ServiceResponse<T> : ServiceErrorResponse
        where T : class
    {
        public T? Data { get; set; }

        public ServiceResponse()
        {
        }

        public ServiceResponse(T data, Error? error = null)
        {
            Data = data; //Current Class Member
            Error = error; //Base Class Member
        }
    }
}