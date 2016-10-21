namespace TrackTv.Models.Contracts
{
    public interface ITvDbRecord
    {
        long LastUpdated { get; set; }

        int TvDbId { get; set; }
    }
}