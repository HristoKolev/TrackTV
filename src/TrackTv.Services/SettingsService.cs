﻿namespace TrackTv.Services
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
            var poco = await this.DbService.Poco.Settings.FirstOrDefaultAsync(p => p.SettingName == setting.ToString());

            return poco.SettingValue;
        }

        public async Task SetSettingAsync(Setting setting, string value)
        {
            var poco = await this.DbService.Poco.Settings.FirstOrDefaultAsync(p => p.SettingName == setting.ToString())
                       ?? new SettingPoco();

            poco.SettingName = setting.ToString();
            poco.SettingValue = value;

            await this.DbService.Save(poco);
        }
    }

    public enum Setting
    {
        LastDatabaseUpdate = 1,

        DisableDatabaseUpdate = 2,

        TheTvDbApiKey = 3,

        UpdateEpisodeChunkSize = 4,

        UpdateChangeChunkSize = 5,
    }
}