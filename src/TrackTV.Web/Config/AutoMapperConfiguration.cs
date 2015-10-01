namespace TrackTV.Web.Config
{
    using System.Reflection;

    using NetInfrastructure.AutoMapper;

    public class AutoMapperConfiguration
    {
        public AutoMapperConfiguration(IMapConfigurator configurator)
        {
            this.Configurator = configurator;
        }

        private IMapConfigurator Configurator { get; }

        public void Load(Assembly assembly)
        {
            this.Configurator.ApplyDefaultMappings(assembly);
            this.Configurator.ApplyCustomMappings(assembly);
        }

        public void Load(string assemblyName)
        {
            this.Configurator.ApplyDefaultMappings(assemblyName);
            this.Configurator.ApplyCustomMappings(assemblyName);
        }
    }
}