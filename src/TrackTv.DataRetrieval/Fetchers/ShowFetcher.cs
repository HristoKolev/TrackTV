namespace TrackTv.DataRetrieval.Fetchers
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Data.Models.Enums;
    using TrackTv.DataRetrieval.Data;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ShowFetcher : IShowFetcher
    {
        public ShowFetcher(INetworkRepository networkRepository)
        {
            this.NetworkRepository = networkRepository;

            this.DateParser = new DateParser();
        }

        private DateParser DateParser { get; }

        private INetworkRepository NetworkRepository { get; }

        public Task PopulateShowAsync(Show show, Series data)
        {
            this.MapToShow(show, data);

            return this.AddNetworkAsync(show, data.Network);
        }

        private async Task AddNetworkAsync(Show show, string networkName)
        {
            if (!show.HasNetwork() || show.Network.NetworkName != networkName)
            {
                var existingNetwork = await this.NetworkRepository.GetNetworkByNameAsync(networkName).ConfigureAwait(false);

                show.Network = existingNetwork ?? new Network(networkName);
            }
        }

        private void MapToShow(Show show, Series data)
        {
            show.TheTvDbId = data.Id;
            show.ShowName = data.SeriesName;
            show.ShowBanner = data.Banner;
            show.ImdbId = data.ImdbId;
            show.ShowDescription = data.Overview;

            show.LastUpdated = data.LastUpdated.ToDateTime();

            AirDay airDay;
            Enum.TryParse(data.AirsDayOfWeek, out airDay);
            show.AirDay = airDay;

            ShowStatus status;
            Enum.TryParse(data.Status, out status);
            show.ShowStatus = status;

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