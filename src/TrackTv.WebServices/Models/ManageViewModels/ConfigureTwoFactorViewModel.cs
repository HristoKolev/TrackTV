namespace TrackTv.WebServices.Models.ManageViewModels
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ConfigureTwoFactorViewModel
    {
        public ICollection<SelectListItem> Providers { get; set; }

        public string SelectedProvider { get; set; }
    }
}