using DelayedMessagingSample.Messages;
using DelayedMessagingSample.Services;
using EasyNetQ;
using EasyNetQ.Scheduling;
using Funq;
using ServiceStack;
using System;

namespace DelayedMessagingSample
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes.
        /// </summary>
        public AppHost()
            : base("DelayedMessagingSample", typeof(AppHost).Assembly)
        {
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            var connectionConfig = new ConnectionConfiguration
            {
                AMQPConnectionString = new Uri("amqp://localhost:5672/"),
                UserName = "guest",
                Password = "guest"
            };
            container.Register(RabbitHutch.CreateBus(connectionConfig,
                x => x.Register<IScheduler, DelayedExchangeScheduler>()));

            var bus = container.Resolve<IBus>();
            bus.Subscribe<DelayedMessage>(String.Empty, msg =>
            {
                var message = new ServiceStack.Messaging.Message<DelayedMessage>(msg);
                HostContext.ServiceController.ExecuteMessage(message);
            });

            this.RegisterService<PublishService>("/");
            this.RegisterService<DelayedMessageService>("/");
        }
    }
}