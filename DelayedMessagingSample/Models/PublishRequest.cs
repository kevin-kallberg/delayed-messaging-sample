using ServiceStack;

namespace DelayedMessagingSample.Models
{
    [Route("/publish")]
    public class PublishRequest : IReturnVoid
    {
        public double MillisecondDelay { get; set; }
    }
}