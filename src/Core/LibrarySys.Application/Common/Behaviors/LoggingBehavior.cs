using LibrarySys.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LibrarySys.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var requestName = typeof(TRequest).Name;
            var stopWatch = Stopwatch.StartNew();

            _logger.LogInformation("Processing {RequestName} at {TimeSpan}", requestName,DateTime.UtcNow);

            try
            {
                var response = await next();

                stopWatch.Stop();

                _logger.LogInformation("Completed {RequestName} in {ElapsedMilliseconds}"
                    ,requestName,stopWatch.ElapsedMilliseconds);
                //throw new Exception();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing {RequestName}", requestName);
                throw new GlobalException();
            }


        }
    }
}
