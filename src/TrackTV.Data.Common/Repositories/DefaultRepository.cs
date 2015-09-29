namespace TrackTV.Data.Common.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using TrackTV.Data.Common.Models.Contracts;
    using TrackTV.Data.Common.Repositories.Contracts;

    public class DefaultRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IAuditInfo
    {
        protected readonly DbContext Context;

        private readonly IDbSet<TEntity> set;

        public DefaultRepository(DbContext context)
        {
            this.Context = context;
            this.set = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            this.ChangeState(entity, EntityState.Added);
        }

        public virtual IQueryable<TEntity> All()
        {
            return this.set;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);

            return entity;
        }

        public TEntity Delete(TId id)
        {
            TEntity entity = this.Find(id);

            this.Delete(entity);

            return entity;
        }

        public TEntity Find(TId id)
        {
            return this.set.Find(id);
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(TEntity entity, EntityState state)
        {
            DbEntityEntry<TEntity> entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}