using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace KickingBgJobs
{
    public class JobWatcher : BackgroundService
    {
        private readonly IJobQueue _jobQueue;
        private readonly IJobRunner _jobRunner;

        public JobWatcher(IJobQueue jobQueue, IJobRunner jobRunner)
        {
            _jobQueue = jobQueue;
            _jobRunner = jobRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (await _jobQueue.Reader.WaitToReadAsync(stoppingToken))
            {
                while (_jobQueue.Reader.TryRead(out var job))
                {
                    _jobRunner.Run(job);
                }
            }
        }
    }
}
