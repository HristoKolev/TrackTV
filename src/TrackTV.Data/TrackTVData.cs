namespace TrackTV.Data
{
    using System.Data.Entity;

    using TrackTV.Data.Common;
    using TrackTV.Data.Common.Repositories.Contracts;
    using TrackTV.Data.Contracts;
    using TrackTV.Models;

    public class TrackTVData : DataObject, ITrackTVData
    {
        public TrackTVData(DbContext context)
            : base(context)
        {
        }

        public TrackTVData()
            : this(new ApplicationDbContext())
        {
        }

        public IRepository<Episode, int> Episodes
        {
            get
            {
                return this.GetRepository<Episode, int>();
            }
        }

        public IRepository<Genre, int> Genres
        {
            get
            {
                return this.GetRepository<Genre, int>();
            }
        }

        public IRepository<Network, int> Networks
        {
            get
            {
                return this.GetRepository<Network, int>();
            }
        }

        public IRepository<Season, int> Seasons
        {
            get
            {
                return this.GetRepository<Season, int>();
            }
        }

        public IRepository<Show, int> Shows
        {
            get
            {
                return this.GetRepository<Show, int>();
            }
        }

        public IRepository<ApplicationUser, string> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser, string>();
            }
        }
    }
}