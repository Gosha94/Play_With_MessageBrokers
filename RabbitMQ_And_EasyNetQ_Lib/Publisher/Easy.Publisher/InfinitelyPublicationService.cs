using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Shared.Contracts;

namespace Easy.Publisher
{
    public class InfinitelyPublicationService : IHostedService
    {
        private readonly IBus _messageBus;
        private readonly Random _random;

        public InfinitelyPublicationService(IBus messageBus)
        {
            _messageBus = messageBus;
            _random = new Random();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = new MessageDto
                {
                    Text = _random.Next(int.MinValue, int.MaxValue).ToString(),
                };

                await Task.Delay(5000);
                await _messageBus.PubSub.PublishAsync(message, cancellationToken: cancellationToken).ConfigureAwait(false);

                Console.WriteLine($"Опубликовано сообщение MessageDto c текстом: {message.Text}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
