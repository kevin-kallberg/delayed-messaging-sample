using DelayedMessagingSample.Messages;
using ServiceStack;
using System;
using System.Diagnostics;

namespace DelayedMessagingSample.Services
{
    public class DelayedMessageService : Service
    {
        // This is the queue consumer for the DelayedMessage queue.
        public void Any(DelayedMessage message)
        {
            message.SentTime = DateTime.UtcNow;

            Debug.WriteLine("{0}New Message Recieved!{0}\tPublished: {1}{0}\tSent: {2}{0}", 
                Environment.NewLine, 
                message.PublishTime, 
                message.SentTime
            );
        }
    }
}