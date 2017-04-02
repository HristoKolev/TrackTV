namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Security.Claims;

    public static class PrintipalExtensions
    {
        /// <summary>
        /// Returns the ProfileId of the current authenticated user.
        /// </summary>
        public static int GetProfileId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("There is no authenticated user.");
            }

            string value = user.FindFirst("ProfileId").Value;

            return int.Parse(value);
        }
    }
}