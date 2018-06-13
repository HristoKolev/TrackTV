namespace TrackTv.Updater
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using log4net;
    using log4net.Config;

    using Newtonsoft.Json;

    using SharpRaven;

    using StructureMap;

    using TrackTv.Updater.Infrastructure;

    public class Program
    {
        private static async Task Main(string[] args)
        {
            Global.CliOptions = CliOptions.Read(args);

            Global.AppConfig = JsonConvert.DeserializeObject<AppConfigModel>(File.ReadAllText(Path.Combine(Global.ConfigDirectory, "appconfig.json")));

            // Log4Net
            var assembly = Assembly.GetEntryAssembly();
            var logRepository = LogManager.GetRepository(assembly);
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo(Path.Combine(Global.ConfigDirectory, "log4net-config.xml")));

            Global.Log = LogManager.GetLogger(assembly, "Global logger");

            Global.Log.Debug("Updater started.");
            Global.Log.Debug("AppConfig:");
            Global.Log.Debug(JsonConvert.SerializeObject(Global.AppConfig, Formatting.Indented));
            Global.Log.Debug("CliOptions:");
            Global.Log.Debug(JsonConvert.SerializeObject(Global.CliOptions, Formatting.Indented));

            Global.ErrorHandler = new ErrorHandler(Global.Log, new RavenClient(Global.AppConfig.SentryUrl));

            using (var mutex = new Mutex(false, "TrackTv.Updater"))
            {
                if (!mutex.WaitOne(0, true))
                {
                    Global.Log.Debug("There is already a running instance of TrackTv.Updater. Exiting...");
                    Environment.Exit(0);
                }

                using (var container = new Container(config => config.AddRegistry<MainRegistry>()))
                {
                    try
                    {
                        var dataSynchronizer = container.GetInstance<DataSynchronizer>();

                        await dataSynchronizer.PerformUpdate(container).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        await Global.ErrorHandler.HandleErrorAsync(e).ConfigureAwait(false);
                        Global.Log.Error("Updater exited with an error.", e);
                    }
                }
            }
        }
    }
}