namespace TrackTV.Web
{
    using System;

    using NetInfrastructure.Core.DI;

    using Ninject;

    public class NinjectTypeProvider : ITypeProvider
    {
        public NinjectTypeProvider(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        private IKernel Kernel { get; }

        public T Get<T>() where T : class
        {
            return this.Kernel.Get<T>();
        }

        public object Get(Type type)
        {
            return this.Kernel.Get(type);
        }
    }
}