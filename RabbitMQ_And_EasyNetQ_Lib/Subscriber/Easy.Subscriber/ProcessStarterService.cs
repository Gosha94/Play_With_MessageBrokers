using EasyNetQ;
using Shared.Contracts;

namespace Easy.Subscriber
{
    public class ProcessStarterService : IHostedService
    {

        private readonly IBus _messageBus;

        public ProcessStarterService(IBus messageBus)
        {
            _messageBus = messageBus;
        }

        public Task StartAsync(CancellationToken cancellationToken) =>
            _messageBus.PubSub.SubscribeAsync<MessageDto>("subscribe_async_test",
                message => SomeWork()
                    .ContinueWith(task =>
                        Console.WriteLine($"Received MessageDto with text: {message.Text}"), cancellationToken));

        private async Task SomeWork()
        {
            await Task.Delay(2000);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
