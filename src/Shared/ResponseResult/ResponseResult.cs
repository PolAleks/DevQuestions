namespace Shared.ResponseResult;

public class ResponseResult
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected ResponseResult(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    protected ResponseResult()
    {
        IsSuccess = true;
        Error = Error.NONE;
    }

    public static ResponseResult Success() => new();

    public static ResponseResult Failure(Error error) => new(error);    
}