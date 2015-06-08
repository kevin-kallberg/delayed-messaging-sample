using System;
using EasyNetQ;

namespace DelayedMessagingSample.Messages
{
    [Queue("DelayedMessageSampleQueue", ExchangeName = "DelayedMessageSampleExchange")]
    public class DelayedMessage
    {
        public DateTime PublishTime { get; set; }
        public DateTime SentTime { get; set; }
    }
}