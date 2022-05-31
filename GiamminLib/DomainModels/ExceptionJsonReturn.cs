using System;

namespace GiamminLib.DomainModels;

/// <summary>
/// utility object con cui ritornare un'eccezione in formato json
/// </summary>
public class ExceptionJsonReturn
{
    // ReSharper disable MemberCanBePrivate.Global
    public string Message { get; set; }
    public string? StackTrace { get; set; }
    public string Type { get; set; }
    public ExceptionJsonReturn? BaseException { get; set; }
    // ReSharper restore MemberCanBePrivate.Global

    public ExceptionJsonReturn(Exception exception)
    {
        Message = exception.Message;
        StackTrace = exception.StackTrace;
        Type = exception.GetType().Name;
        if (exception.InnerException != null)
        {
            BaseException = new ExceptionJsonReturn(exception.GetBaseException());
        }

    }
}