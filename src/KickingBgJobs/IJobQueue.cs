using System.Threading.Channels;

namespace KickingBgJobs
{
    public interface IJobQueue
    {
        ChannelReader<Job> Reader { get; }
        ChannelWriter<Job> Writer { get; }

    }
}