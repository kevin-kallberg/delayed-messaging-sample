using DelayedMessagingSample.Messages;
using DelayedMessagingSample.Models;
using EasyNetQ;
using EasyNetQ.Scheduling;
using ServiceStack;
using System;

namespace DelayedMessagingSample.Services
{
    public class PublishService : Service
    {
        public void Any(PublishRequest request)
        {
            var message = new DelayedMessage { PublishTime = DateTime.UtcNow };

            var bus = HostContext.Container.Resolve<IBus>();
            bus.FuturePublish(TimeSpan.FromMilliseconds(request.MillisecondDelay), message);
        }
    }
}