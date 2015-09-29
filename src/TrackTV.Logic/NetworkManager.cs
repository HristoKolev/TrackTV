namespace TrackTV.Logic
{
    using System.Linq;

    using TrackTV.Data.Contracts;
    using TrackTV.Models;

    public class NetworkManager
    {
        private readonly ITrackTVData data;

        public NetworkManager(ITrackTVData data)
        {
            this.data = data;
        }

        public Network GetByStringId(string stringId)
        {
            return this.data.Networks.All().FirstOrDefault(n => n.StringId == stringId);
        }
    }
}