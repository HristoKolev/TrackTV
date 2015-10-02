﻿namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Data;
    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Web.Controllers;

    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(IRepository<ApplicationUser, string> users, IAppSettings appSettings)
            : base(users, appSettings)
        {
        }
    }
}