using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AutofacSample.Service
{
    public class SQSPollService : BackgroundService
    {
        private readonly ILogger<SQSPollService> _logger;


        public SQSPollService(ILogger<SQSPollService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"GracePeriodManagerService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" GracePeriod background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"GracePeriod task doing background work.");

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogDebug($"GracePeriod background task is stopping.");
        }

        
    }
}