namespace TrackTv.WebServices.Infrastructure
{
    using System.Security.Claims;

    public static class PrincipalExtensions
    {
        /// <summary>
        /// Returns the ProfileId of the current authenticated user.
        /// </summary>
        public static int GetProfileId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return default;
            }

            string value = user.FindFirst("ProfileId").Value;

            return int.Parse(value);
        }
    }
}