namespace TrackTV.WebServices.Models
{
    using System.Collections.Generic;

    public class ManageInfoViewModel
    {
        public string Email { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }

        public string LocalLoginProvider { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }
    }
}