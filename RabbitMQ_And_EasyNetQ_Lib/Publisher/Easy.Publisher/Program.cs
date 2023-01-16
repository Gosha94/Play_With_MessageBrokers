using EasyNetQ;
using Shared.Contracts;

namespace Easy.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var input = String.Empty;
                Console.WriteLine("Enter a message. 'Quit' to quit.");
                while ((input = Console.ReadLine()) != "Quit")
                {
                    bus.PubSub.Publish(new MessageDto { Text = input });
                    Console.WriteLine("Message published!");
                }
            }
        }
    }
}