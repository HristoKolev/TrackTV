namespace TrackTV.Logic.Fetchers
{
    using System.Collections.Generic;

    using TrackTV.Logic.Models;
    using TrackTV.Models;

    public interface IFetcher
    {
        Show AddShow(int id);

        IList<ShowSample> GetSamples(string showName);

        void UpdateShow(Show id);
    }
}