using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KickingBgJobs
{
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
