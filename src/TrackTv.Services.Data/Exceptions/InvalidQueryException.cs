﻿namespace TrackTv.Services.Data.Exceptions
{
    using System;

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