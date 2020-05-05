using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace KickingBgJobs
{
    public class JobWatcher : BackgroundService
    {
        private readonly IJobQueue _jobQueue;

        public JobWatcher(IJobQueue jobQueue)
        {
            _jobQueue = jobQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (await _jobQueue.Reader.WaitToReadAsync(stoppingToken))
            {
                while (_jobQueue.Reader.TryRead(out var job))
                {

                }
            }
        }
    }
}
