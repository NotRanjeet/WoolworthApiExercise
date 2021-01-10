using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Woolworth.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            string userName = string.Empty;
            //TODO: In real world get current user details
            //From service for current user provider and inject the details in logging messages
            _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, "DummyUserId", userName, request);
        }
    }
}
