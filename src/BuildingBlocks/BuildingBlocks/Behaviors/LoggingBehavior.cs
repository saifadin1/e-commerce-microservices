using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> 
where TRequest : notnull, IRequest<TResponse>
where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request = {Request} - {Response} - {RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();
        var respone = await next();
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] the request {request} took {timeTaken} seconds}" ,
                typeof(TRequest), timeTaken.Seconds);
        }
        
        logger.LogInformation("[END] Handle request = {Request} - {Response} - {RequestData} - {TimeTaken}",
            typeof(TRequest).Name, typeof(TResponse).Name, request, timeTaken);
        return respone;
    }
}