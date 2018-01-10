namespace TrackTv.Services.Shows
{
    using System;

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