namespace TrackTV.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using TrackTV.Data.Contracts;
    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Web.ViewModels.Shows;

    public class ShowsController : BaseController
    {
        private const int PageSize = 24;

        public ShowsController(ITrackTVData data)
            : base(data)
        {
        }

        public ActionResult ByGenre(string stringId)
        {
            GenreManager genreManager = new GenreManager(this.Data);

            Genre genre = genreManager.GetByStringId(stringId);

            if (genre == null)
            {
                return this.NotFound();
            }

            ShowManager showManager = new ShowManager(this.Data);

            IList<SimpleShowViewModel> running = showManager.GetRunningShowsByGenre(genre.Id).Take(PageSize).Project().To<SimpleShowViewModel>().ToList();

            IList<SimpleShowViewModel> ended = showManager.GetEndedShowsByGenre(genre.Id).Take(PageSize).Project().To<SimpleShowViewModel>().ToList();

            IList<GenreViewModel> genres = genreManager.GetAllGenres().Project().To<GenreViewModel>().ToList();

            ShowsByGenreVewModel model = new ShowsByGenreVewModel
            {
                Running = running,
                Ended = ended,
                Genres = genres,
                GenreName = genre.Name
            };

            return this.View(model);
        }

        public ActionResult ByNetwork(string stringId, int? page)
        {
            NetworkManager networkManager = new NetworkManager(this.Data);

            Network network = networkManager.GetByStringId(stringId);

            if (network == null)
            {
                return this.NotFound();
            }

            ShowManager showManager = new ShowManager(this.Data);

            IQueryable<Show> shows = showManager.GetShowsByNetwork(network.Id);

            int count = shows.Count();

            if (page.HasValue)
            {
                shows = shows.Skip((page.Value - 1) * PageSize);
            }

            shows = shows.Take(PageSize);

            ShowsNetworkViewModel model = new ShowsNetworkViewModel
            {
                Shows = shows.Project().To<SimpleShowViewModel>().ToList(),
                NetworkName = network.Name
            };

            if (page.HasValue)
            {
                model.CurrentPage = page.Value;
            }
            else
            {
                model.CurrentPage = 1;
            }

            model.TotalPages = count / PageSize;

            if ((count % PageSize) > 0)
            {
                model.TotalPages++;
            }

            return this.View(model);
        }

        public ActionResult Index()
        {
            ShowManager showManager = new ShowManager(this.Data);

            IList<SimpleShowViewModel> running = showManager.GetRunningShows().Take(PageSize).Project().To<SimpleShowViewModel>().ToList();
            IList<SimpleShowViewModel> ended = showManager.GetEndedShows().Take(PageSize).Project().To<SimpleShowViewModel>().ToList();

            GenreManager genreManager = new GenreManager(this.Data);

            IList<GenreViewModel> genres = genreManager.GetAllGenres().Project().To<GenreViewModel>().ToList();

            ShowsViewModel model = new ShowsViewModel
            {
                Running = running,
                Ended = ended,
                Genres = genres
            };

            return this.View(model);
        }

        public ActionResult Search(string query, int? page)
        {
            if (query.Trim() == string.Empty)
            {
                return this.RedirectToAction("Index");
            }

            ShowManager showManager = new ShowManager(this.Data);

            IQueryable<Show> shows = showManager.SearchShow(query);

            int count = shows.Count();

            if (count == 0)
            {
                return this.View(
                    "NoSearchResults",
                    new ShowsSearchViewModel
                    {
                        Query = query
                    });
            }

            if (page.HasValue)
            {
                shows = shows.Skip((page.Value - 1) * PageSize);
            }

            shows = shows.Take(PageSize);

            ShowsSearchViewModel model = new ShowsSearchViewModel
            {
                Shows = shows.Project().To<SimpleShowViewModel>().ToList(),
                Query = query,
                ActionName = MethodBase.GetCurrentMethod().Name,
                ControllerName = MethodBase.GetCurrentMethod().DeclaringType.Name
            };

            if (page.HasValue)
            {
                model.CurrentPage = page.Value;
            }
            else
            {
                model.CurrentPage = 1;
            }

            model.TotalPages = count / PageSize;

            if ((count % PageSize) > 0)
            {
                model.TotalPages++;
            }

            return this.View(model);
        }
    }
}