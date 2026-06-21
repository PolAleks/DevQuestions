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

public class ResponseResult<TValue> : ResponseResult
{
    private readonly TValue _value = default!;

    public TValue Value => IsSuccess ? _value : throw new ApplicationException("Result is not success");

    private ResponseResult(TValue value) => _value = value;

    private ResponseResult(Error error) : base(error) { }

    public static ResponseResult<TValue> Success(TValue value) => new(value);

    public static new ResponseResult<TValue> Failure(Error error) => new(error);

    public static implicit operator ResponseResult<TValue>(TValue value) => Success(value);

    public static implicit operator ResponseResult<TValue>(Error error) => Failure(error);
}