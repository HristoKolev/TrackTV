namespace TrackTV.DataRetrieval.Fetchers
{
    using System;
    using System.Threading.Tasks;

    using TrackTV.Data.Repositories.Contracts;
    using TrackTV.DataRetrieval.Fetchers.Contracts;

    using TrackTv.Models;
    using TrackTv.Models.Enums;

    using TvDbSharper.Clients.Series.Json;
    using TvDbSharper.Clients.Updates;

    public class ShowFetcher : IShowFetcher
    {
        public ShowFetcher(INetworkRepository networkRepository)
        {
            this.NetworkRepository = networkRepository;

            this.DateParser = new DateParser();
        }

        private DateParser DateParser { get; }

        private INetworkRepository NetworkRepository { get; }

        public async Task PopulateShowAsync(Show show, Series data)
        {
            this.MapToShow(show, data);

            await this.AddNetworkAsync(show, data.Network);
        }

        private async Task AddNetworkAsync(Show show, string networkName)
        {
            if (!show.HasNetwork() || (show.Network.Name != networkName))
            {
                var existingNetwork = await this.NetworkRepository.GetNetworkByNameAsync(networkName);

                show.Network = existingNetwork ?? new Network(networkName);
            }
        }

        private void MapToShow(Show show, Series data)
        {
            show.TheTvDbId = data.Id;
            show.Name = data.SeriesName;
            show.Banner = data.Banner;
            show.ImdbId = data.ImdbId;
            show.Description = data.Overview;

            show.LastUpdated = data.LastUpdated.ToDateTime();

            AirDay airDay;
            Enum.TryParse(data.AirsDayOfWeek, out airDay);
            show.AirDay = airDay;

            ShowStatus status;
            Enum.TryParse(data.Status, out status);
            show.Status = status;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                show.FirstAired = this.DateParser.ParseFirstAired(data.FirstAired);
            }

            if (!string.IsNullOrWhiteSpace(data.AirsTime))
            {
                show.AirTime = this.DateParser.ParseAirTime(data.AirsTime);
            }
        }
    }
}