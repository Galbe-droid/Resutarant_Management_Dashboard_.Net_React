using Sprache;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Common
{
    // Generic result class to encapsulate success, data, and error information
    public class Result<T>
    {
        public bool Success { get; set; }
        public ResultType Type { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public static Result<T> Ok(T data) => new Result<T> { Success = true, Type = ResultType.Success, Data = data };
        public static Result<T> Fail(string error) => new Result<T> { Success = false, Error = error };
        public static Result<T> Validation(string error) => new Result<T> { Success = false, Type = ResultType.Validation, Error = error };
        public static Result<T> Conflict(string error) => new Result<T> { Success = false, Type = ResultType.Conflict, Error = error };
        public static Result<T> NotFound(string error) => new Result<T> { Success = false, Type = ResultType.NotFound, Error = error };
        public static Result<T> Unauthorized(string error) => new Result<T> { Success = false, Type = ResultType.Unauthorized, Error = error };
        public static Result<T> Forbidden(string error) => new Result<T> { Success = false, Type = ResultType.Forbidden, Error = error }; 
    }
}
