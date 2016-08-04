﻿namespace TrackTV.Logic.Fetchers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic.Models;
    using TrackTV.Models;

    using TVDBSharp;
    using TVDBSharp.Models.Enums;

    public class Fetcher : IFetcher
    {
        private static readonly string[] ExceptableExtensions =
        {
            ".jpg", 
            ".jpeg", 
            ".png"
        };

        public Fetcher(IRepository<Show> shows, IRepository<Genre> genres, IRepository<Network> networks, IAppSettings appSettings)
        {
            this.Shows = shows;
            this.Genres = genres;
            this.Networks = networks;
            this.AppSettings = appSettings;

            this.TvdbConnection = new TVDB(this.AppSettings.ApiKey);
        }

        public IAppSettings AppSettings { get; set; }

        private IRepository<Genre> Genres { get; }

        private IRepository<Network> Networks { get; }

        private IRepository<Show> Shows { get; }

        private TVDB TvdbConnection { get; }

        private WebClient WebClient { get; set; }

        public Show AddShow(int id)
        {
            TVDBSharp.Models.Show fetchedShow = this.TvdbConnection.GetShow(id);

            Show show = new Show();

            this.MapShow(show, fetchedShow);

            this.Shows.Add(show);
            this.Shows.SaveChanges();

            CalculateLastAndNextEpisodes(show);
            this.Shows.SaveChanges();

            return show;
        }

        public IList<ShowSample> GetSamples(string showName)
        {
            List<ShowSample> samples = new List<ShowSample>();

            const int NumberOfResults = 3;

            List<TVDBSharp.Models.Show> shows = this.TvdbConnection.Search(showName, NumberOfResults);

            foreach (TVDBSharp.Models.Show show in shows)
            {
                ShowSample model = new ShowSample
                {
                    Banner = show.Banner.ToString(), 
                    Description = show.Description, 
                    Id = show.Id, 
                    Name = show.Name
                };

                samples.Add(model);
            }

            return samples;
        }

        public void UpdateShow(Show show)
        {
            TVDBSharp.Models.Show fetchedShow = this.TvdbConnection.GetShow(show.TvDbId);

            this.MapShow(show, fetchedShow);

            this.Shows.Update(show);
            this.Shows.SaveChanges();

            CalculateLastAndNextEpisodes(show);
            this.Shows.SaveChanges();
        }

        private static void CalculateLastAndNextEpisodes(Show show)
        {
            IEnumerable<Episode> episodes = show.Seasons.SelectMany(season => season.Episodes);

            Episode lastEpisode =
                episodes.Where(episode => episode.FirstAired < DateTime.Now)
                        .OrderByDescending(episode => episode.FirstAired)
                        .FirstOrDefault();
            Episode nextEpisode =
                episodes.Where(episode => episode.FirstAired > DateTime.Now).OrderBy(episode => episode.FirstAired).FirstOrDefault();

            if (lastEpisode != null)
            {
                show.LastEpisodeId = lastEpisode.Id;
            }
            else
            {
                show.LastEpisodeId = null;
            }

            if (nextEpisode != null)
            {
                show.NextEpisodeId = nextEpisode.Id;
            }
            else
            {
                show.NextEpisodeId = null;
            }
        }

        private static AirDay? GetAirDay(Frequency? airDay)
        {
            string day = airDay.ToString();

            AirDay value;

            if (Enum.TryParse(day, false, out value))
            {
                return value;
            }

            if (airDay == null)
            {
                return null;
            }

            throw new ArgumentException("The AirDay cannot be parsed.");
        }

        private static string GetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

            return description;
        }

        private static string GetLanguage(string language)
        {
            switch (language)
            {
                case "en" :
                    return "English";

                default :
                    throw new ArgumentOutOfRangeException(nameof(language));
            }
        }

        private static ShowStatus GetStatus(Status status)
        {
            string stringStatus = status.ToString();

            ShowStatus value;

            if (Enum.TryParse(stringStatus, false, out value))
            {
                return value;
            }

            throw new ArgumentException("The ShowStatus cannot be parsed.");
        }

        private static string GetUserFriendlyId(string genreName)
        {
            string id = genreName.ToLower();

            StringBuilder builder = new StringBuilder();

            foreach (char c in id)
            {
                if (IsLatinLetterOrDigit(c))
                {
                    builder.Append(c);
                }
                else
                {
                    builder.Append("-");
                }
            }

            id = builder.ToString();

            while (id.Contains("--"))
            {
                id = id.Replace("--", "-");
            }

            id = id.Trim('-');

            return id;
        }

        private static bool IsLatinLetterOrDigit(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9');
        }

        private static void MapEpisode(Episode episode, TVDBSharp.Models.Episode episodeModel)
        {
            episode.ImdbId = episodeModel.ImdbId;
            episode.TvDbId = episodeModel.Id;
            episode.FirstAired = episodeModel.FirstAired;
            episode.Description = episodeModel.Description;
            episode.LastUpdated = episodeModel.LastUpdated;
            episode.Number = episodeModel.EpisodeNumber;
            episode.Title = episodeModel.Title;
        }

        private static void SetSeasons(Show show, TVDBSharp.Models.Show fetchedShow)
        {
            ICollection<Season> seasons = show.Seasons;

            foreach (TVDBSharp.Models.Episode episodeModel in fetchedShow.Episodes)
            {
                int seasonNumber = episodeModel.SeasonNumber;

                Season season = seasons.FirstOrDefault(s => s.Number == seasonNumber);

                if (season == null)
                {
                    season = new Season
                    {
                        Number = seasonNumber
                    };

                    seasons.Add(season);
                }

                TVDBSharp.Models.Episode model = episodeModel;

                Episode episode = season.Episodes.FirstOrDefault(e => e.Number == model.EpisodeNumber);

                if (episode == null)
                {
                    episode = new Episode();
                }

                MapEpisode(episode, episodeModel);

                episode.Season = season;
                season.Episodes.Add(episode);

                show.Seasons.Add(season);
                season.Show = show;
            }
        }

        private void AddGenres(ICollection<Genre> genreList, IEnumerable<string> genreNames)
        {
            foreach (string genreName in genreNames)
            {
                Genre genre = this.GetOrCreateGenre(genreName);

                if (!genreList.Contains(genre))
                {
                    genreList.Add(genre);
                }
            }
        }

        private string GetImage(string prefix, Uri image, string rootDirectory)
        {
            if (image == null)
            {
                return null;
            }

            if (!ExceptableExtensions.Contains(Path.GetExtension(image.ToString())))
            {
                return null;
            }

            if (this.WebClient == null)
            {
                this.WebClient = new WebClient();
            }

            string directory = $@"/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";

            if (!Directory.Exists(rootDirectory + directory))
            {
                Directory.CreateDirectory(rootDirectory + directory);
            }

            string path = $@"{directory}/{prefix}{image.Segments.Last()}";

            this.WebClient.DownloadFile(image, rootDirectory + path);

            return path;
        }

        private Genre GetOrCreateGenre(string genreName)
        {
            Genre genre = this.Genres.All().FirstOrDefault(g => g.Name == genreName);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName, 
                    UserFriendlyId = GetUserFriendlyId(genreName)
                };

                this.Genres.Add(genre);
            }

            return genre;
        }

        private Network GetOrCreateNetwork(string networkName)
        {
            Network network = this.Networks.All().FirstOrDefault(net => net.Name == networkName);

            if (network == null)
            {
                network = new Network
                {
                    Name = networkName, 
                    UserFriendlyId = GetUserFriendlyId(networkName)
                };

                this.Networks.Add(network);
            }

            return network;
        }

        private void MapShow(Show show, TVDBSharp.Models.Show fetchedShow)
        {
            show.AirTime = fetchedShow.AirTime;
            show.Runtime = fetchedShow.Runtime;
            show.Description = GetDescription(fetchedShow.Description);
            show.Network = this.GetOrCreateNetwork(fetchedShow.Network);
            show.TvDbId = fetchedShow.Id;
            show.ImdbId = fetchedShow.ImdbId;
            show.Language = GetLanguage(fetchedShow.Language);
            show.AirDay = GetAirDay(fetchedShow.AirDay);
            show.FirstAired = fetchedShow.FirstAired;
            this.AddGenres(show.Genres, fetchedShow.Genres);
            show.LastUpdated = fetchedShow.LastUpdated;
            show.Name = fetchedShow.Name;
            show.Status = GetStatus(fetchedShow.Status);
            show.BannerBig = this.GetImage("big_", fetchedShow.Banner, this.AppSettings.BannerDirectory);
            show.PosterBig = this.GetImage("big_", fetchedShow.Poster, this.AppSettings.PosterDirectory);
            show.UserFriendlyId = GetUserFriendlyId(fetchedShow.Name);

            show.Network.Shows.Add(show);

            SetSeasons(show, fetchedShow);
        }
    }
}