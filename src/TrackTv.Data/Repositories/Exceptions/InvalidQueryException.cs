namespace TrackTV.Data.Repositories.Exceptions
{
    using System;

    public class InvalidQueryException : Exception
    {
        public InvalidQueryException()
        {
        }

        public InvalidQueryException(string message)
            : base(message)
        {
        }

        public InvalidQueryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}