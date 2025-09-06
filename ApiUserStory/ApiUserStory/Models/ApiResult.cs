public class ApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }

    public static ApiResult<T> SuccessResult(string message, T? data = default)
    {
        return new ApiResult<T> { Success = true, Message = message, Data = data };
    }

    public static ApiResult<T> Failure(string message)
    {
        return new ApiResult<T> { Success = false, Message = message };
    }
}