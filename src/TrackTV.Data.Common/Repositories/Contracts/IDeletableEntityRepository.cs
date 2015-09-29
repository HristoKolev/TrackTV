namespace TrackTV.Data.Common.Repositories.Contracts
{
    using System.Linq;

    public interface IDeletableEntityRepository<TEntity, in TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        void ActualDelete(TEntity entity);

        IQueryable<TEntity> AllWithDeleted();
    }
}