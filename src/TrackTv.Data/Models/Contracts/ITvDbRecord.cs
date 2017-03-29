namespace TrackTv.Data.Models.Contracts
{
    using System;

    public interface ITvDbRecord
    {
        DateTime LastUpdated { get; set; }

        int TheTvDbId { get; set; }
    }
}