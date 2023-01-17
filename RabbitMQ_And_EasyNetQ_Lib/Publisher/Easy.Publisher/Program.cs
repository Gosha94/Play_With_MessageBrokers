using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasyNetQ;
using Shared.Contracts;

namespace Easy.Publisher
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
                .ConfigureServices(services =>
                    services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<InfinitelyPublicationService>();

                    services.AddSingleton<IBus>(
                        RabbitHutch.CreateBus(
                            "username=super_rabbit;" +
                            "password=super_rabbit;" +
                            "virtualHost=xo_game;" +
                            "host=localhost;" +
                            "publisherConfirms=true;" +
                            "timeout=10"));
                });

        private static void ConfigureContainer(HostBuilderContext hostContext, ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //builder
            //    .RegisterType<ClientRegistrationBehaviourProcess>()
            //    .Keyed<IProcess>(Config.DataFlowsConfiguration.ClientRegistrationDataFlow.ClientRegisteredQueue)
            //    .SingleInstance();
        }

    }
}