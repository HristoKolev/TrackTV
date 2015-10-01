namespace TrackTV.Web.Config
{
    using System.Data.Entity;
    using System.Reflection;

    using AutoMapper;

    using NetInfrastructure.AutoMapper;
    using NetInfrastructure.Core.DI;
    using NetInfrastructure.Data.Repositories;

    using Ninject;
    using Ninject.Web.Common;

    using TrackTV.Data;
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

            this.Kernel.Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();

            this.Kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            this.Kernel.Bind(typeof(IRepository<,>)).To(typeof(Repository<,>));

            this.Kernel.Bind<IFetcher>().To<Fetcher>();

            this.RegisterAutoMapperBindings();

            AutoMapperConfiguration configuration = new AutoMapperConfiguration(this.Kernel.Get<IMapConfigurator>());
            configuration.Load(Assembly.GetExecutingAssembly());
            configuration.Load("TrackTV.Services");
        }

        private void RegisterAutoMapperBindings()
        {
            this.Kernel.Bind<IConfiguration>().ToConstant(Mapper.Configuration);
            this.Kernel.Bind(typeof(ICustomMapper<,>)).To(typeof(CustomMapper<,>));

            this.Kernel.Bind<IMappingEngine>().ToConstant(Mapper.Engine);

            this.Kernel.Bind<IMapConfigurator>().To<MapConfigurator>().InSingletonScope();
        }
    }
}