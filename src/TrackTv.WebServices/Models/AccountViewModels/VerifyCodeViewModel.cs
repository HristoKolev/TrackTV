﻿namespace TrackTv.WebServices.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class VerifyCodeViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Provider { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}