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
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string? ErrorMessage { get; set; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            ErrorMessage = null;
        }

        private Result(string errorMessage)
        {
            IsSuccess = false;
            Value = default(T);
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(errorMessage);
        }
    }


    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        private Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            ErrorMessage = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }
    }
}
