using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

    public interface IJobRunner
    {
        void Run(Job job);

        Task RunAsync(Job job);
    }


    public class JobRunner : IJobRunner
    {
        private readonly ILogger<JobRunner> _logger;

        public JobRunner(ILogger<JobRunner> logger)
        {
            _logger = logger;
        }
        public void Run(Job job)
        {
            _logger.LogInformation("The job {jobId} was ran", job.Id);
        }

        public Task RunAsync(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
