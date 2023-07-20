using System;

namespace Sample.Api.DTO
{
    public class ServiceResponse
    {
        public Error? Error { get; set; }
    }

    public class ServiceResponse<T> : ServiceResponse
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

