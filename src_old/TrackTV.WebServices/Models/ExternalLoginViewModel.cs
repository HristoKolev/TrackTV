namespace TrackTV.WebServices.Models
{
    // Models returned by AccountController actions.
    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string State { get; set; }

        public string Url { get; set; }
    }
}