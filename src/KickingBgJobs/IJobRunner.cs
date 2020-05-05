using System.Threading.Tasks;

namespace KickingBgJobs
{
    public interface IJobRunner
    {
        void Run(Job job);

        Task RunAsync(Job job);
    }
}
