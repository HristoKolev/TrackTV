namespace TrackTV.Data.Common.Repositories.Contracts
{
    using System.Linq;

    public interface IRepository<TEntity, in TId>
        where TEntity : class
    {
        void Add(TEntity entity);

        IQueryable<TEntity> All();

        TEntity Delete(TEntity entity);

        TEntity Delete(TId id);

        TEntity Find(TId id);

        int SaveChanges();

        void Update(TEntity entity);
    }
}