namespace FreshUp.Domain.Exceptions;

public class CustomException : Exception
{
    public int StatusCode { get; set; }

    public CustomException(int statusCode, string message) : base(message)
    {
        this.StatusCode = statusCode;
    }

    public CustomException(int statusCode, string message, Exception innerExceptio) : base(message, innerExceptio) 
    {
        this.StatusCode = statusCode;
    }
}
