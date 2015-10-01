namespace TrackTV.Logic.Fetchers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using TrackTV.Data;
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

        private readonly ITrackTVData data;

        private readonly TVDB tvdbConnection;

        private WebClient webClient;

        public Fetcher(ITrackTVData data)
        {
            this.data = data;
            this.tvdbConnection = new TVDB(ApplicationSettings.ApiKey);
        }

        public Show AddShow(int id)
        {
            TVDBSharp.Models.Show fetchedShow = this.tvdbConnection.GetShow(id);

            Show show = new Show();

            this.MapShow(show, fetchedShow);

            this.data.Shows.Add(show);
            this.data.Shows.SaveChanges();

            CalculateLastAndNextEpisodes(show);
            this.data.SaveChanges();

            return show;
        }

        public IList<ShowSample> GetSamples(string showName)
        {
            List<ShowSample> samples = new List<ShowSample>();

            const int NumberOfResults = 3;

            List<TVDBSharp.Models.Show> shows = this.tvdbConnection.Search(showName, NumberOfResults);

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
            TVDBSharp.Models.Show fetchedShow = this.tvdbConnection.GetShow(show.TvDbId);

            this.MapShow(show, fetchedShow);

            this.data.Shows.Update(show);
            this.data.Shows.SaveChanges();

            CalculateLastAndNextEpisodes(show);
            this.data.SaveChanges();
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
                    throw new ArgumentOutOfRangeException("language");
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

        private static string GetStringId(string genreName)
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

            if (this.webClient == null)
            {
                this.webClient = new WebClient();
            }

            string directory = string.Format(@"/{0}/{1}/{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (!Directory.Exists(rootDirectory + directory))
            {
                Directory.CreateDirectory(rootDirectory + directory);
            }

            string path = string.Format(@"{0}/{1}{2}", directory, prefix, image.Segments.Last());

            this.webClient.DownloadFile(image, rootDirectory + path);

            return path;
        }

        private Genre GetOrCreateGenre(string genreName)
        {
            Genre genre = this.data.Genres.All().FirstOrDefault(g => g.Name == genreName);

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName,
                    StringId = GetStringId(genreName)
                };

                this.data.Genres.Add(genre);
            }

            return genre;
        }

        private Network GetOrCreateNetwork(string networkName)
        {
            Network network = this.data.Networks.All().FirstOrDefault(net => net.Name == networkName);

            if (network == null)
            {
                network = new Network
                {
                    Name = networkName,
                    StringId = GetStringId(networkName)
                };

                this.data.Networks.Add(network);
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
            show.BannerBig = this.GetImage("big_", fetchedShow.Banner, ApplicationSettings.BannerDirectory);
            show.PosterBig = this.GetImage("big_", fetchedShow.Poster, ApplicationSettings.PosterDirectory);
            show.StringId = GetStringId(fetchedShow.Name);

            show.Network.Shows.Add(show);

            SetSeasons(show, fetchedShow);
        }
    }
}