using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Woolworth.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                _logger.LogWarning("CleanArchitecture Long Running Request({ElapsedMilliseconds} milliseconds) {@Request}",
                     elapsedMilliseconds, request);
            }

            return response;
        }
    }
}
