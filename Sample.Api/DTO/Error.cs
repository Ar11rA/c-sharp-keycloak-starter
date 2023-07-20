using System;
namespace Sample.Api.DTO
{
    public sealed class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }

        public string Target { get; set; } = string.Empty;

        public List<Error> Details { get; set; } = new();
    }
}

