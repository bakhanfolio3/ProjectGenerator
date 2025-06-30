using ProjectName.Application.Features.Users;
using ProjectName.Application.Abstraction.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Application.Common.Responses;

public class Result : IResult
{
    private string _message = string.Empty; 

    //public bool Failed => !Succeeded;

    public List<string> Errors { get; set; } = new List<string>();

    public string Message 
    { 
        get 
        {
            return Succeeded ? _message : (Errors.Count > 0 ? Errors.First() : string.Empty); 
        } 
        set 
        { 
            Errors.Clear();
            _message = value;
            if (!Succeeded)
                Errors.Add(value);
        } 
    } 

    public bool Succeeded { get; set; }

    public static IResult Fail()
    {
        return new Result
        {
            Succeeded = false
        };
    }

    public static IResult Fail(List<string> errors)
    {
        return new Result
        {
            Succeeded = false,
            Errors = errors
        };
    }

    public static IResult Fail(string message)
    {
        return Fail(new List<string> { message });
    }

    public static Task<IResult> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public static Task<IResult> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public static Task<IResult> FailAsync(List<string> errors)
    {
        return Task.FromResult(Fail(errors));
    }

    public static IResult Success()
    {
        return new Result
        {
            Succeeded = true
        };
    }

    public static IResult Success(string message)
    {
        return new Result
        {
            Succeeded = true,
            Message = message
        };
    }

    public static Task<IResult> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<IResult> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static IResult NotFound()
    {
        return Fail("Not Found");
    }

    public static IResult Invalid(List<string> errors)
    {
        return Fail(errors);
    }

}

public class Result<T> : Result, IResult<T>
{
    public T Data { get; set; }

    public new static IResult<T> Fail()
    {
        return new Result<T>
        {
            Succeeded = false
        };
    }

    public new static IResult<T> Fail(string message)
    {
        return new Result<T>
        {
            Succeeded = false,
            Message = message
        };
    }

    public new static Task<IResult<T>> FailAsync()
    {
        return Task.FromResult(Fail());
    }

    public new static Task<IResult<T>> FailAsync(string message)
    {
        return Task.FromResult(Fail(message));
    }

    public new static IResult<T> Success()
    {
        return new Result<T>
        {
            Succeeded = true
        };
    }

    public new static IResult<T> Success(string message)
    {
        return new Result<T>
        {
            Succeeded = true,
            Message = message
        };
    }

    public static IResult<T> Success(T data)
    {
        return new Result<T>
        {
            Succeeded = true,
            Data = data
        };
    }

    public static IResult<T> Success(T data, string message)
    {
        return new Result<T>
        {
            Succeeded = true,
            Data = data,
            Message = message
        };
    }

    public new static Task<IResult<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public new static Task<IResult<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<IResult<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<IResult<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }

    public new static IResult<T> NotFound()
    {
        return Fail("Not Found");
    }

    public static async Task<IResult<T>> NotFoundAsync()
    {
        return await FailAsync("Not Found");
    }
}
