namespace TrackTV.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using TrackTV.Data.Common.Models.Contracts;
    using TrackTV.Data.Common.Repositories;
    using TrackTV.Data.Common.Repositories.Contracts;

    public abstract class DataObject
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        protected DataObject(DbContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        protected IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class, IDeletableEntity, IAuditInfo
        {
            Type type = typeof(TEntity);

            if (!this.repositories.ContainsKey(type))
            {
                object newRepository = new DeletableEntityRepository<TEntity, TId>(this.context);

                this.repositories.Add(type, newRepository);
            }

            return (IRepository<TEntity, TId>)this.repositories[type];
        }
    }
}