namespace TrackTV.Web.Config
{
    using System.Data.Entity;
    using System.Reflection;

    using NetInfrastructure.AutoMapper.Ninject;
    using NetInfrastructure.Configuration;
    using NetInfrastructure.Core.DI;
    using NetInfrastructure.Data.Repositories;

    using Ninject;
    using Ninject.Web.Common;

    using TrackTV.Data;
    using TrackTV.Logic;
    using TrackTV.Logic.Fetchers;

    public class NinjectBinder
    {
        public NinjectBinder(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        private IKernel Kernel { get; }

        public void Load()
        {
            this.Kernel.Bind<ITypeProvider>().To<NinjectTypeProvider>();

            this.RegisterDatabaseBindings<ApplicationDbContext>();
            this.Kernel.AddAutoMapperBindings();

            this.RegisterUserBindings();

            this.ConfigureAutoMapper();

            // Mapper.Initialize(config => config.(type => this.Kernel.Get<ITypeProvider>().Get(type)));
        }

        private void ConfigureAutoMapper()
        {
            var configuration = this.Kernel.Get<AutoMapperConfiguration>();
            configuration.Load(Assembly.GetExecutingAssembly());
            configuration.Load("TrackTV.Services");
        }

        private void RegisterDatabaseBindings<TContext>() where TContext : DbContext
        {
            this.Kernel.Bind<DbContext>().To<TContext>().InRequestScope();

            this.Kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            this.Kernel.Bind(typeof(IRepository<,>)).To(typeof(Repository<,>));
        }

        private void RegisterUserBindings()
        {
            this.Kernel.Bind<IFetcher>().To<Fetcher>();

            this.Kernel.Bind<IConfigurationDocument>().To<ConfigurationManagerDocument>();
            this.Kernel.Bind<IAppSettings>().To<AppSettings>();
        }
    }
}