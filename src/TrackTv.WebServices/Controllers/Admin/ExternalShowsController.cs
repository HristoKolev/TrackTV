namespace TrackTv.WebServices.Controllers.Admin
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.DataRetrieval;
    using TrackTv.DataRetrieval.Services;
    using TrackTv.WebServices.Infrastructure;

    [Authorize(Roles = AppRoles.Admin)]
    [Route("api/admin/[controller]")]
    public class ExternalShowsController : Controller
    {
        public ExternalShowsController(IExternalShowsService externalShowsService, IFetcher fetcher)
        {
            this.ExternalShowsService = externalShowsService;
            this.Fetcher = fetcher;
        }

        private IExternalShowsService ExternalShowsService { get; }

        private IFetcher Fetcher { get; }

        [ServiceFilter(typeof(InTransactionFilterAttribute))]
        [HttpPut("[action]/{seriesId}")]
        public async Task<IActionResult> Add(int seriesId)
        {
            await this.Fetcher
                      .AddShowAsync(seriesId)
                      .ConfigureAwait(false);

            return this.Success();
        }

        [HttpGet("[action]/{imdbId}")]
        public async Task<IActionResult> ByImdbId(string imdbId)
        {
            return this.Success(await this.ExternalShowsService
                                          .GetShowsByImdbIdAsync(imdbId)
                                          .ConfigureAwait(false));
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> Get(string query)
        {
            return this.Success(await this.ExternalShowsService
                                          .GetShowsByNameAsync(query)
                                          .ConfigureAwait(false));
        }
    }
}