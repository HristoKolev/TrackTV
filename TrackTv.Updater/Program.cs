namespace TrackTv.Updater
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using log4net;
    using log4net.Config;

    using Newtonsoft.Json;

    using StructureMap;

    using TrackTv.Data.Enums;
    using TrackTv.Services;

    public class Program
    {
        private static async Task Main()
        {
            Global.AppConfig =
                JsonConvert.DeserializeObject<AppConfigModel>(File.ReadAllText(Path.Combine(Global.ConfigDirectory, "appconfig.json")));

            // Log4Net
            var assembly = Assembly.GetEntryAssembly();
            var logRepository = LogManager.GetRepository(assembly);
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo(Path.Combine(Global.ConfigDirectory, "log4net-config.xml")));

            Global.Log = LogManager.GetLogger(assembly, "Global logger");

            Global.Log.Debug("Updater started.");

            Global.ErrorHandler = new ErrorHandler(Global.Log, new MishapService(Global.AppConfig.MishapApiKey));

            var container = new Container(config => config.AddRegistry<MainRegistry>());

            try
            {
                var settingsService = container.GetInstance<SettingsService>();

                if (!bool.Parse(await settingsService.GetSettingAsync(Setting.DisableDatabaseUpdate).ConfigureAwait(false)))
                {
                    var lastUpdated = DateTime
                                      .Parse(await settingsService.GetSettingAsync(Setting.LastDatabaseUpdate).ConfigureAwait(false))
                                      .ToUniversalTime();

                    var synchronizer = container.GetInstance<DataSynchronizer>();

                    async Task OnSuccessfulUpdate(DateTime time)
                    {
                        if (time > lastUpdated)
                        {
                            lastUpdated = time;
                        }

                        await settingsService.SetSettingAsync(Setting.LastDatabaseUpdate, lastUpdated.ToString("O")).ConfigureAwait(false);
                    }

                    await synchronizer.UpdateAllAsync(lastUpdated,
                                          async ex => await Global.ErrorHandler.HandleErrorAsync(ex).ConfigureAwait(false),
                                          OnSuccessfulUpdate)
                                      .ConfigureAwait(false);

                    Global.Log.Debug("Updater finished successfully.");
                }
                else
                {
                    Global.Log.Debug("Updates disabled. Exiting...");
                }
            }
            catch (Exception e)
            {
                await Global.ErrorHandler.HandleErrorAsync(e).ConfigureAwait(false);
                Global.Log.Error("Updater exited with an error.", e);
            }
        }
    }
}