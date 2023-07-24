namespace Sample.Tests.Utils.DTO
{
    public sealed class ServiceResponse<T>
        where T : class
    {
        public T? Data { get; set; }
        public Error? Error { get; set; }

        public ServiceResponse()
        {
        }

        public ServiceResponse(T data, Error? error = null)
        {
            Data = data; //Current Class Member
            Error = error;
        }
    }
}