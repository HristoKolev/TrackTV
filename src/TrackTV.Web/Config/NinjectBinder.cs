﻿namespace TrackTV.Web.Config
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Reflection;

    using AutoMapper;

    using NetInfrastructure.AutoMapper;
    using NetInfrastructure.Core.DI;
    using NetInfrastructure.Data.Repositories;

    using Ninject;
    using Ninject.Web.Common;

    using TrackTV.Data;
    using TrackTV.Logic;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Services;

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

            this.Kernel.Bind<ITrackTVData>().To<TrackTVData>();

            this.Kernel.Bind<IFetcher>().To<Fetcher>();

            this.RegisterManagers();

            this.RegisterServices();

            this.RegisterAutoMapperBindings();

            this.RegisterMappedModels(this.Kernel.Get<IMapConfigurator>(), Assembly.GetExecutingAssembly());

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

        private void RegisterManagers()
        {
            this.Kernel.Bind<ShowManager>().ToSelf();
            this.Kernel.Bind<EpisodeManager>().ToSelf();

        }

        private void RegisterMappedModels(IMapConfigurator configurator, Assembly assembly)
        {
            IEnumerable<Type> models = configurator.GetCustomModels(assembly);

            foreach (Type model in models)
            {
                this.Kernel.Bind(model).ToSelf();
            }
        }

        private void RegisterServices()
        {
            this.Kernel.Bind<CalendarService>().ToSelf();
        }
    }
}