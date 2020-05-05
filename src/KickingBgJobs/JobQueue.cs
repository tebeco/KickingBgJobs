using System.Threading.Channels;

namespace KickingBgJobs
{
    public interface IJobQueue
    {
        ChannelReader<Job> Reader { get; }
        ChannelWriter<Job> Writer { get; }

    }

    public class JobQueue : IJobQueue
    {

        private readonly Channel<Job> _channel;

        public JobQueue()
        {
            _channel = Channel.CreateBounded<Job>(10);
        }

        public ChannelReader<Job> Reader => _channel.Reader;
        public ChannelWriter<Job> Writer => _channel.Writer;

    }

    public class Job
    {
        public Job(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}