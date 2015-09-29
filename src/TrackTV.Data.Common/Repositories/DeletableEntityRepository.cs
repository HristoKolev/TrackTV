namespace TrackTV.Data.Common.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using TrackTV.Data.Common.Models.Contracts;
    using TrackTV.Data.Common.Repositories.Contracts;

    public class DeletableEntityRepository<TEntity, TId> : DefaultRepository<TEntity, TId>, IDeletableEntityRepository<TEntity, TId>
        where TEntity : class, IAuditInfo, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public void ActualDelete(TEntity entity)
        {
            base.Delete(entity);
        }

        public override IQueryable<TEntity> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<TEntity> AllWithDeleted()
        {
            return base.All();
        }

        public override TEntity Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;

            return entity;
        }
    }
}