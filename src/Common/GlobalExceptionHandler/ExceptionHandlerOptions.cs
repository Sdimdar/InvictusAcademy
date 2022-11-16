using GlobalExceptionHandler.Interfaces;

namespace GlobalExceptionHandler;

public sealed class ExceptionHandlerOptions
{
    internal Dictionary<Type, Type> Handlers { get; }

    public ExceptionHandlerOptions()
    {
        Handlers = new Dictionary<Type, Type>();
    }
    
    internal void Add(Type exceptionType, Type handlerType)
    {
        if (Handlers.TryAdd(exceptionType, handlerType)) return;
        Handlers.Remove(exceptionType);
        Handlers.Add(exceptionType, handlerType);
    }

    public void Add<TException, THandler>() where TException : Exception where THandler : IExceptionHandler
    {
        var exceptionType = typeof(TException);
        var handlerType = typeof(THandler);
        if (Handlers.TryAdd(exceptionType, handlerType)) return;
        Handlers.Remove(exceptionType);
        Handlers.Add(exceptionType, handlerType);
    }
}