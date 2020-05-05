using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KickingBgJobs
{
    public class FakeAndUglyCron : BackgroundService
    {
        private readonly IJobQueue _jobQueue;
        private readonly ILogger<FakeAndUglyCron> _logger;

        public FakeAndUglyCron(IJobQueue jobQueue, ILogger<FakeAndUglyCron> logger)
        {
            _jobQueue = jobQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                var job = new Job();
                _logger.LogInformation("Queuing Job {jobId}", job.Id);
                _jobQueue.Writer.TryWrite(job);
            }
        }
    }
}
