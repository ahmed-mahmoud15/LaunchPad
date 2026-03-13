using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public string? ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }

        private Result(bool isSuccess, T? value, string? error, int statusCode)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = error;
            StatusCode = statusCode;
        }

        public static Result<T> Ok(T value) => new(true, value, null, 200);
        public static Result<T> Created(T value) => new(true, value, null, 201);

        public static Result<T> NotFound(string message) => new(false, default, message, 404);
        public static Result<T> Conflict(string message) => new(false, default, message, 409);
        public static Result<T> BadRequest(string message) => new(false, default, message, 400);
        public static Result<T> Unauthorized(string message) => new(false, default, message, 401);
        public static Result<T> Forbidden(string message) => new(false, default, message, 403);
        public static Result<T> ServerError(string message) => new(false, default, message, 500);
    }


    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }
        public int StatusCode { get; private set; }

        private Result(bool isSuccess, string? error, int statusCode)
        {
            IsSuccess = isSuccess;
            ErrorMessage = error;
            StatusCode = statusCode;
        }

        public static Result Ok() => new(true, null, 200);
        public static Result NoContent() => new(true, null, 204);
        public static Result NotFound(string message) => new(false, message, 404);
        public static Result Conflict(string message) => new(false, message, 409);
        public static Result BadRequest(string message) => new(false, message, 400);
    }
}
