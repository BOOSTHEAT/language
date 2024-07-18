using System;
using System.Threading;

namespace ImpliciX.Language.Core;

public static class SideEffect
{
       
    public static void TryRun(Action action, Action<Exception> errorFunc )
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            errorFunc(e);
        }
    }

    public static Result<T> TryRun<T>(Func<T> f, Func<Exception, Error> errorFunc, RetryPolicy retryPolicy = null,
        Action<Exception, int, int> logFunction = null, Action<Exception> onException=null)
        => _tryRun(f, e => errorFunc(e), logFunction ?? DefaultLogErrorFunction, onException ?? DefaultOnException, retryPolicy);
        
    public static Result<T> TryRun<T>(Func<T> f, Func<Exception,T> errorFunc, RetryPolicy retryPolicy = null,
        Action<Exception, int, int> logFunction = null, Action<Exception> onException=null)
        => _tryRun(f, e => errorFunc(e), logFunction ?? DefaultLogErrorFunction, onException ?? DefaultOnException, retryPolicy);

    public static Result<T> TryRun<T>(Func<T> f, Func<Error> errorFunc, RetryPolicy retryPolicy = null,
        Action<Exception, int, int> logFunction = null,Action<Exception> onException=null)
        => _tryRun(f, _ => Result<T>.Create(errorFunc()), logFunction ?? DefaultLogErrorFunction, onException ?? DefaultOnException, retryPolicy);

    public static Result<T> TryRunOrDefault<T>(Func<T> f, Func<Exception, T> fDefault,
        RetryPolicy retryPolicy = null, Action<Exception, int, int> logFunction = null, Action<Exception> onException=null)
        => _tryRun(f, e => fDefault(e), logFunction ?? DefaultLogErrorFunction, onException ?? DefaultOnException,retryPolicy);

    private static Result<T> _tryRun<T>(Func<T> f, Func<Exception, Result<T>> returnFunc,
        Action<Exception, int, int> logFunction, Action<Exception> onException, RetryPolicy retryPolicy)
    {
        retryPolicy ??= RetryPolicy.Create();
        for (var @try = 0;; @try++)
        {
            try
            {
                return f();
            }
            catch (Exception e)
            {
                onException(e);
                if (@try >= retryPolicy.RetryNumber)
                {
                    logFunction(e, @try + 1, retryPolicy.RetryNumber);
                    return returnFunc(e);
                }
                if (retryPolicy.RetrySleepTime > 0) Thread.Sleep(retryPolicy.RetrySleepTime);
            }
        }
    }

    private static void DefaultLogErrorFunction(Exception exception, int tryNumber, int totalRetries)
    {
        Log.Error(exception, "[Try {@Try_Number}/{@Total_TryNumber}]: {@Exception_Message}", tryNumber, totalRetries + 1, exception.ToString());
    }
        
    private static void DefaultOnException(Exception exception) { }

    public static Result<T> SafeCast<T>(object source)
    {
        if (source is T sourceAsT)
        {
            return Result<T>.Create(sourceAsT);
        }
        else
        {
            var error = new Error("Invalid cast", $"{source.GetType().Name} cannot be cast into {typeof(T).Name}");
            Log.Verbose(error.Message);
            return Result<T>.Create(error);
        }
    }

    public class RetryPolicy
    {
        public readonly int RetryNumber;
        public readonly int RetrySleepTime;

        private RetryPolicy(int retryNumber, int retrySleepTime)
        {
            RetryNumber = retryNumber;
            RetrySleepTime = retrySleepTime;
        }

        public static RetryPolicy Create(int retryNumber = 0, int retrySleepTime = 0) =>
            new RetryPolicy(retryNumber, retrySleepTime);
    }
        
}