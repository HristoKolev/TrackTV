﻿namespace TrackTv.Services.Exceptions
{
    using System;

    public class ProfileNotFoundException : Exception
    {
        public ProfileNotFoundException()
        {
        }

        public ProfileNotFoundException(string message)
            : base(message)
        {
        }

        public ProfileNotFoundException(int profileId)
            : this($"There is no profile with id {profileId}")
        {
        }

        public ProfileNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}