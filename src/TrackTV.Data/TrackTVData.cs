namespace TrackTV.Data
{
    using System.Data.Entity;

    using NetInfrastructure.Core.DI;
    using NetInfrastructure.Data;
    using NetInfrastructure.Data.Repositories;

    using TrackTV.Data.Contracts;
    using TrackTV.Models;

    public class TrackTVData : BaseDataUnit, ITrackTVData
    {
        public TrackTVData(ITypeProvider provider)
            : base(provider)
        {
            this.Context = provider.Get<DbContext>();
        }

        public IRepository<Episode> Episodes => this.GetRepository<Episode>();

        public IRepository<Genre> Genres => this.GetRepository<Genre>();

        public IRepository<Network> Networks => this.GetRepository<Network>();

        public IRepository<Season> Seasons => this.GetRepository<Season>();

        public IRepository<Show> Shows => this.GetRepository<Show>();

        public IRepository<ApplicationUser, string> Users => this.GetRepository<ApplicationUser, string>();

        private DbContext Context { get; }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }
    }
}