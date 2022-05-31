using System;
using System.Collections.Generic;

namespace GiamminLib.Patterns
{
    public abstract class ResultBase
    {
        public bool Success { get; set; }
    }
    public abstract class ResultBase<T>:ResultBase
    {
        protected ResultBase(T? data = default)
        {
            Data = data;
        }

        public T? Data { get; set; }
    }

    public class SuccessResult : ResultBase
    {
        public SuccessResult()
        {
            Success = true;
        }
    }
    public class SuccessResult<T> : ResultBase<T>
    {
        public SuccessResult(T? data = default) : base(data)
        {
            Success = true;
        }

        public T Get() => Data ?? throw new InvalidOperationException("Data cannot be null in SuccessResult<T>");
        public static implicit operator SuccessResult(SuccessResult<T> successResult) => new();
    }

    public class ErrorResult : ResultBase
    {
        public string Message { get; set; }
        public List<ResultErrorDetail> Errors { get; set; } = new();
        public ErrorResult(string message, IEnumerable<ResultErrorDetail>? errors = null)
        {
            Message = message;
            if (errors != null)
            {
                Errors = new List<ResultErrorDetail>(errors);
            }
        }
    }
    public class ErrorResult<T> : ResultBase<T>
    {
        public string Message { get; set; }
        public List<ResultErrorDetail> Errors { get; set; } = new();
        public ErrorResult(string message, IEnumerable<ResultErrorDetail>? errors = null)
        {
            Message = message;
            if (errors != null)
            {
                Errors = new List<ResultErrorDetail>(errors);
            }
        }
        public static implicit operator ErrorResult(ErrorResult<T> errorResult) => new(errorResult.Message, errorResult.Errors);
    }
    public class GenericResult<T> : ResultBase<T>
    {
        public string? Message { get; set; }
        public List<ResultErrorDetail> Errors { get; set; } = new();
        public static implicit operator GenericResult(GenericResult<T> genericResult) =>
            new()
            {
                Message = genericResult.Message,
                Errors = genericResult.Errors,
                Success = genericResult.Success
            };
    }
    public class GenericResult : ResultBase
    {
        public string? Message { get; set; }
        public List<ResultErrorDetail> Errors { get; set; } = new();
    }

    public record ResultErrorDetail(string Key, string Details);
}
