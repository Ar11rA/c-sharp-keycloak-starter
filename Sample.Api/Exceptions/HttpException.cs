namespace Sample.Api.Exceptions;

public class HttpException : Exception
{
    public int Code { get; set; }

    public HttpException(int code, string message) : base(message)
    {
        Code = code;
    }
}
