namespace TrackTv.Services.Show.Models
{
    using System;

    using TrackTv.Services.Exceptions;

    public class ShowNotFoundException : Exception
    {
        public ShowNotFoundException()
        {
        }

        public ShowNotFoundException(int showId)
            : this($"There is no show with id {showId}")
        {
        }

        public ShowNotFoundException(string message)
            : base(message)
        {
        }

        public ShowNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}