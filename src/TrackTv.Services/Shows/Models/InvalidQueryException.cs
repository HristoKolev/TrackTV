namespace TrackTv.Services.Shows.Models
{
    using System;

    using TrackTv.Services.Exceptions;

    public class InvalidQueryException : Exception
    {
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