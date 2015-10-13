namespace TrackTV.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Services.VewModels.Shows;

    public class ShowsService
    {
        private const int PageSize = 24;

        public ShowsService(ShowManager showManager, IRepository<Genre> genres, IRepository<Network> networks)
        {
            this.ShowManager = showManager;

            this.Genres = genres;
            this.Networks = networks;
        }

        private IRepository<Genre> Genres { get; }

        private IRepository<Network> Networks { get; }

        private ShowManager ShowManager { get; }

        // done
        public ShowsByGenreVewModel GetByGenre(string genreUserFriendlyId)
        {
            Genre genre = this.GetGenreByUserFriendlyId(genreUserFriendlyId);

            if (genre == null)
            {
                return null;
            }

            IList<SimpleShowViewModel> running =
                this.ShowManager.GetRunningShowsByGenre(genre.Id).Take(PageSize).Project().To<SimpleShowViewModel>().ToList();

            IList<SimpleShowViewModel> ended =
                this.ShowManager.GetEndedShowsByGenre(genre.Id).Take(PageSize).Project().To<SimpleShowViewModel>().ToList();

            IList<GenreViewModel> genres = this.GetAllGenres().Project().To<GenreViewModel>().ToList();

            ShowsByGenreVewModel model = new ShowsByGenreVewModel
            {
                Running = running, 
                Ended = ended, 
                Genres = genres, 
                GenreName = genre.Name
            };

            return model;
        }

        // done
        public ShowsNetworkViewModel GetByNetwork(string networkUserFriendlyId, int? page)
        {
            Network network = this.GetNetworkByUserFriendlyId(networkUserFriendlyId);

            if (network == null)
            {
                return null;
            }

            IQueryable<Show> shows = this.ShowManager.GetShowsByNetwork(network.Id).OrderByDescending(show => show.Subscribers.Count);

            int count = shows.Count();

            ShowsNetworkViewModel model = new ShowsNetworkViewModel
            {
                Shows = shows.Project().To<SimpleShowViewModel>().Page(page, PageSize), 
                NetworkName = network.Name, 
                Count = count
            };

            return model;
        }

        // done
        public ShowsViewModel GetTopShows()
        {
            IList<SimpleShowViewModel> running =
                this.ShowManager.GetRunningShows().Take(PageSize).Project().To<SimpleShowViewModel>().ToList();
            IList<SimpleShowViewModel> ended = this.ShowManager.GetEndedShows().Take(PageSize).Project().To<SimpleShowViewModel>().ToList();

            IList<GenreViewModel> genres = this.GetAllGenres().Project().To<GenreViewModel>().ToList();

            ShowsViewModel model = new ShowsViewModel
            {
                Running = running, 
                Ended = ended, 
                Genres = genres
            };

            return model;
        }

        // done
        public ShowsSearchViewModel Search(string query, int? page)
        {
            IQueryable<Show> shows = this.ShowManager.SearchShow(query);

            int count = shows.Count();

            if (count == 0)
            {
                return new ShowsSearchViewModel
                {
                    Query = query, 
                    Count = 0
                };
            }

            ShowsSearchViewModel model = new ShowsSearchViewModel
            {
                Shows = shows.Project().To<SimpleShowViewModel>().Page(page, PageSize), 
                Query = query, 
                Count = count
            };

            return model;
        }

        private IQueryable<Genre> GetAllGenres()
        {
            return this.Genres.All();
        }

        private Genre GetGenreByUserFriendlyId(string userFriendlyId)
        {
            return this.Genres.All().FirstOrDefault(g => g.UserFriendlyId == userFriendlyId);
        }

        private Network GetNetworkByUserFriendlyId(string userFriendlyId)
        {
            return this.Networks.All().FirstOrDefault(n => n.UserFriendlyId == userFriendlyId);
        }
    }
}