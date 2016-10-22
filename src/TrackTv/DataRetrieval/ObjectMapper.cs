namespace TrackTv.DataRetrieval
{
    using System;
    using System.Globalization;

    using TrackTv.Models;
    using TrackTv.Models.Enums;
    using TrackTv.Models.Joint;

    using TvDbSharper.Clients.Episodes.Json;
    using TvDbSharper.Clients.Series.Json;
    using TvDbSharper.Clients.Updates;

    using Actor = TrackTv.Models.Actor;
    using ActorData = TvDbSharper.Clients.Series.Json.Actor;

    public class ObjectMapper
    {
        public void MapToEpisode(Episode episode, EpisodeRecord data)
        {
            episode.Title = data.EpisodeName;
            episode.Description = data.Overview;
            episode.ImdbId = data.ImdbId;
            episode.Number = data.AiredEpisodeNumber.Value;
            episode.SeasonNumber = data.AiredSeason.Value;
            episode.TvDbId = data.Id;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                episode.FirstAired = ParseFirstAired(data.FirstAired);
            }

            long? lastUpdated = data.LastUpdated;
            episode.LastUpdated = lastUpdated.ToDateTime();
        }

        public void MapToShow(Show show, Series data)
        {
            show.TvDbId = data.Id;
            show.Name = data.SeriesName;
            show.Banner = data.Banner;
            show.ImdbId = data.ImdbId;
            show.Description = data.Overview;

            long? lastUpdated = data.LastUpdated;
            show.LastUpdated = lastUpdated.ToDateTime();

            AirDay airDay;
            Enum.TryParse(data.AirsDayOfWeek, out airDay);
            show.AirDay = airDay;

            ShowStatus status;
            Enum.TryParse(data.Status, out status);
            show.Status = status;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                show.FirstAired = ParseFirstAired(data.FirstAired);
            }

            if (!string.IsNullOrWhiteSpace(data.AirsTime))
            {
                show.AirTime = ParseAirTime(data.AirsTime);
            }
        }

        public void UpdateActor(Actor actor, ActorData data)
        {
            var lastUpdated = DateTime.Parse(data.LastUpdated);

            if (lastUpdated > actor.LastUpdated)
            {
                actor.LastUpdated = lastUpdated;
                actor.Name = data.Name;
                actor.Image = data.Image;
            }
        }

        public void UpdateShowActorRelationship(ShowsActors showsActors, ActorData data)
        {
            showsActors.Role = data.Role;
        }

        private static DateTime? ParseAirTime(string value)
        {
            value = value.Trim();

            if (!value.ToLower().EndsWith("am") && !value.ToLower().EndsWith("pm"))
            {
                return null;
            }

            string abbreviation = value.Substring(value.Length - 2, 2).ToLower();

            string hoursAndMinutes = value.Remove(value.Length - 2, 2).Trim();

            if (!hoursAndMinutes.Contains(":"))
            {
                return null;
            }

            string stringHours = hoursAndMinutes.Split(':')[0];

            int hours;

            if (!int.TryParse(stringHours, out hours))
            {
                return null;
            }

            if ((hours < 1) || (hours > 12))
            {
                return null;
            }

            string stringMinutes = hoursAndMinutes.Replace(stringHours + ":", string.Empty);

            int minutes;

            if (!int.TryParse(stringMinutes, out minutes))
            {
                return null;
            }

            if ((minutes < 0) || (minutes > 59))
            {
                return null;
            }

            var formattableString = $"0001-01-01 {stringHours.PadLeft(2, '0')}:{stringMinutes.PadLeft(2, '0')} {abbreviation}";

            return DateTime.ParseExact(formattableString, "yyyy-MM-dd hh:mm tt", CultureInfo.InvariantCulture);
        }

        private static DateTime ParseFirstAired(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}