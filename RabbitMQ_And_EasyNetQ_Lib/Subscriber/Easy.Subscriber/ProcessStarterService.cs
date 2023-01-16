using EasyNetQ;

namespace Easy.Subscriber
{
    public class ProcessStarterService : IHostedService
    {

        private readonly IBus _messageBus;

        public ProcessStarterService(IBus messageBus)
        {
            _messageBus = messageBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
