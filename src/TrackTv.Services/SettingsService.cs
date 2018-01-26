namespace TrackTv.Services
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class SettingsService
    {
        public SettingsService(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task<string> GetSettingAsync(Setting setting)
        {
            var poco = await this.DbService.Settings.FirstOrDefaultAsync(p => p.SettingName == setting.ToString()).ConfigureAwait(false);

            return poco.SettingValue;
        }

        public async Task SetSettingAsync(Setting setting, string value)
        {
            var poco = await this.DbService.Settings.FirstOrDefaultAsync(p => p.SettingName == setting.ToString()).ConfigureAwait(false)
                       ?? new SettingPoco();

            poco.SettingName = setting.ToString();
            poco.SettingValue = value;

            await this.DbService.Save(poco).ConfigureAwait(false);
        }
    }

    public enum Setting
    {
        LastDatabaseUpdate = 1,

        DisableDatabaseUpdate = 3
    }
}