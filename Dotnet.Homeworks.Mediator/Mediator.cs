using System.Collections.Concurrent;

namespace Dotnet.Homeworks.Mediator;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    private readonly ConcurrentDictionary<Type, Type> _requestHandlers = new();

    public void Register(Type requestType, Type handlerType)
    {
        _requestHandlers.TryAdd(requestType, handlerType);
    }
    
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        var handler = _requestHandlers.GetValueOrDefault(request.GetType());
        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for '{request.GetType().Name}' not found.");
        }

        return await ((IRequestHandler<IRequest<TResponse>, TResponse>) handler)
            .Handle(request, cancellationToken);
    }

    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        var handler = _requestHandlers.GetValueOrDefault(request.GetType());
        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for '{request.GetType().Name}' not found.");
        }

        await ((IRequestHandler<IRequest>) handler).Handle(request, cancellationToken);
    }

    public async Task<dynamic?> Send(dynamic request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request is not IRequest<dynamic> requestTyped)
        {
            throw new ArgumentNullException(nameof(request), "Invalid request type");
        }
        
        var handler = _requestHandlers.GetValueOrDefault(requestTyped.GetType());
        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for '{request.GetType().Name}' not found.");
        }

        return await ((IRequestHandler<IRequest<dynamic>, dynamic>) handler)
            .Handle(request, cancellationToken);
    }
}