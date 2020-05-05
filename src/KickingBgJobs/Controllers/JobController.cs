using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KickingBgJobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobQueue _jobQueue;

        public JobController(ILogger<JobController> logger, IJobQueue jobQueue)
        {
            _logger = logger;
            _jobQueue = jobQueue;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_jobQueue.Writer.TryWrite(new Job()))
            {
                return Ok();
            }
            else
            {
                return StatusCode(503);
            }
        }
    }
}
