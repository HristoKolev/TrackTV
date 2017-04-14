namespace TrackTv.Services.Subscription.Models
{
    using System;

    using TrackTv.Services.Exceptions;

    [ExposeError]
    public class SubscriptionException : Exception
    {
        public SubscriptionException(string message)
            : base(message)
        {
        }

        public SubscriptionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}