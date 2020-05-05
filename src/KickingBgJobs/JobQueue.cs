using System.Threading.Channels;

namespace KickingBgJobs
{

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
}