namespace TrackTv.Services.Shows.Models
{
    using System;

    using TrackTv.Services.Exceptions;

    [ExposeError]
    public class GenreNotFoundException : Exception
    {
        public GenreNotFoundException()
        {
        }

        public GenreNotFoundException(string message)
            : base(message)
        {
        }

        public GenreNotFoundException(int genreId)
            : this($"There is no genre with id {genreId}")
        {
        }

        public GenreNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}