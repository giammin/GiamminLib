using System;

namespace GiamminLib.Patterns;

public class BiResult<TSuccess, TFail>
{
    private readonly TFail? _failResponse;
    private readonly TSuccess? _successResponse;

    public BiResult(TSuccess successResponse)
    {
        _successResponse = successResponse;
        _failResponse = default;
    }

    public BiResult(TFail failResponse)
    {
        _failResponse = failResponse;
        _successResponse = default;
    }

    public bool IsSuccess => _failResponse == null;
    public bool IsFailed => _failResponse != null;

    public TSuccess GetSuccessResult()
    {
        if (_successResponse == null)
        {
            throw new Exception("result failed");
        }
        return _successResponse;
    }
    public TFail GetFailResult()
    {
        if (_failResponse == null)
        {
            throw new Exception("result succeded");
        }
        return _failResponse;
    }

    public BiResult<TB, TFail> Map<TB>(Func<TSuccess, TB> mapFunc) =>
        !IsSuccess
            ? new BiResult<TB, TFail>(GetFailResult())
            : new BiResult<TB, TFail>(mapFunc(GetSuccessResult()));

    public TSuccess IfFail(Func<TFail, TSuccess> f) =>
        IsSuccess
            ? GetSuccessResult()
            : f(GetFailResult());


    public void IfSuccess(Action<TSuccess> f)
    {
        if (IsSuccess) f(GetSuccessResult());
    }
    public TMap? IfSuccessMap<TMap>(Func<TSuccess, TMap> f)
    {
        if (IsSuccess) return f(GetSuccessResult());
        return default(TMap);
    }
    public void IfFail(Action<TFail> f)
    {
        if (!IsSuccess) f(GetFailResult());
    }

    public TR Match<TR>(Func<TSuccess, TR> success, Func<TFail, TR> fail) =>
        IsSuccess
            ? success(GetSuccessResult())
            : fail(GetFailResult());
}