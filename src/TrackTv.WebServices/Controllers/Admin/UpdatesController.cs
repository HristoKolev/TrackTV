﻿namespace TrackTv.WebServices.Controllers.Admin
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.DataRetrieval;
    using TrackTv.WebServices.Infrastructure;

    [Authorize(Roles = AppRoles.Admin)]
    [Route("api/admin/[controller]")]
    [ServiceFilter(typeof(InTransactionFilter))]
    public class UpdatesController : Controller
    {
        public UpdatesController(Fetcher fetcher)
        {
            this.Fetcher = fetcher;
        }

        private Fetcher Fetcher { get; }

        [HttpPut("[action]/{date}")]
        public async Task<IActionResult> AllRecords(DateTime date)
        {
            await this.Fetcher.UpdateAllRecordsAsync(date).ConfigureAwait(false);

            return this.Success();
        }

        [HttpPut("[action]/{showId}")]
        public async Task<IActionResult> Show(int showId)
        {
            await this.Fetcher.UpdateShowAsync(showId).ConfigureAwait(false);

            return this.Success();
        }
    }
}