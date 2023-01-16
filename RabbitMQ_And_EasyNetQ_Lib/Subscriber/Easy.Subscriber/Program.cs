using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasyNetQ;
using System.Reflection;

namespace Easy.Subscriber
{
    internal class Program
    {

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
                .ConfigureServices(services => services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ProcessStarterService>();
                });

        private static void ConfigureContainer(HostBuilderContext hostContext, ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.Register<IBus>((context) =>
                RabbitHutch.CreateBus(
                    "username=super_rabbit;" +
                    "password=super_rabbit;" +
                    "virtualHost=xo_game;" +
                    "host=localhost"))
                .SingleInstance();

            //builder
            //    .RegisterType<ClientRegistrationBehaviourProcess>()
            //    .Keyed<IProcess>(Config.DataFlowsConfiguration.ClientRegistrationDataFlow.ClientRegisteredQueue)
            //    .SingleInstance();
        }



            //        using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //            {
            //                bus.PubSub.Subscribe<MessageDto>("test", HandleTextMessage);
            //                Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //                Console.ReadLine();
            //            }

            //static void HandleTextMessage(MessageDto textMessage)
            //        {
            //            Console.ForegroundColor = ConsoleColor.Red;
            //            Console.WriteLine("Got message: {0}", textMessage.Text);
            //            Console.ResetColor();
            //        }



        }
}