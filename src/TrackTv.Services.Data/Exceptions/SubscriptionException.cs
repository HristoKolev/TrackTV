namespace TrackTv.Services.Data.Exceptions
{
    using System;

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