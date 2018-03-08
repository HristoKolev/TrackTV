namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
	using LinqToDB;
    using LinqToDB.Mapping;

    /// <summary>
    /// <para>Database table 'actors'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "actors")]
    public class ActorPoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'actor_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "actor_id", DataType = DataType.Int32)]
        public int ActorID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ActorID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ActorID = value;
		}

		bool IPoco.IsNew()
		{
			return this.ActorID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'actor_image'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "actor_image", DataType = DataType.NVarChar)]
        public string ActorImage { get; set; }
        
        /// <summary>
		/// <para>Column name: 'actor_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "actor_name", DataType = DataType.NVarChar)]
        public string ActorName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'api_responses'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_responses")]
    public class ApiResponsePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'api_response_episode_thetvdbid'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "api_response_episode_thetvdbid", DataType = DataType.Int32)]
        public int? ApiResponseEpisodeThetvdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_response_show_thetvdbid'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "api_response_show_thetvdbid", DataType = DataType.Int32)]
        public int? ApiResponseShowThetvdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_response_body'.</para>
		/// <para>PostgreSQL data type: 'jsonb'.</para>
		/// <para>linq2db data type: 'DataType.BinaryJson'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "api_response_body", DataType = DataType.BinaryJson)]
        public string ApiResponseBody { get; set; }
        
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'api_response_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "api_response_id", DataType = DataType.Int32)]
        public int ApiResponseID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ApiResponseID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ApiResponseID = value;
		}

		bool IPoco.IsNew()
		{
			return this.ApiResponseID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'api_response_last_updated'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "api_response_last_updated", DataType = DataType.DateTime2)]
        public DateTime ApiResponseLastUpdated { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'episodes'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "episodes")]
    public class EpisodePoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'episode_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "episode_id", DataType = DataType.Int32)]
        public int EpisodeID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.EpisodeID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.EpisodeID = value;
		}

		bool IPoco.IsNew()
		{
			return this.EpisodeID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'episode_description'.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "episode_description", DataType = DataType.Text)]
        public string EpisodeDescription { get; set; }
        
        /// <summary>
		/// <para>Column name: 'episode_number'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "episode_number", DataType = DataType.Int32)]
        public int EpisodeNumber { get; set; }
        
        /// <summary>
		/// <para>Column name: 'episode_title'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "episode_title", DataType = DataType.NVarChar)]
        public string EpisodeTitle { get; set; }
        
        /// <summary>
		/// <para>Column name: 'first_aired'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "first_aired", DataType = DataType.DateTime2)]
        public DateTime? FirstAired { get; set; }
        
        /// <summary>
		/// <para>Column name: 'imdbid'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "imdbid", DataType = DataType.NVarChar)]
        public string Imdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'season_number'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "season_number", DataType = DataType.Int32)]
        public int SeasonNumber { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "genres")]
    public class GenrePoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'genre_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "genre_id", DataType = DataType.Int32)]
        public int GenreID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.GenreID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.GenreID = value;
		}

		bool IPoco.IsNew()
		{
			return this.GenreID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'genre_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "genre_name", DataType = DataType.NVarChar)]
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'networks'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "networks")]
    public class NetworkPoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'network_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "network_id", DataType = DataType.Int32)]
        public int NetworkID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.NetworkID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.NetworkID = value;
		}

		bool IPoco.IsNew()
		{
			return this.NetworkID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'network_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "network_name", DataType = DataType.NVarChar)]
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'profiles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "profiles")]
    public class ProfilePoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ProfileID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ProfileID = value;
		}

		bool IPoco.IsNew()
		{
			return this.ProfileID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'profile_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "profile_name", DataType = DataType.NVarChar)]
        public string ProfileName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'roles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "roles")]
    public class RolePoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'role_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "role_id", DataType = DataType.Int32)]
        public int RoleID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.RoleID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.RoleID = value;
		}

		bool IPoco.IsNew()
		{
			return this.RoleID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'actor_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "actor_id", DataType = DataType.Int32)]
        public int ActorID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'role_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "role_name", DataType = DataType.NVarChar)]
        public string RoleName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'settings'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "settings")]
    public class SettingPoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'setting_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "setting_id", DataType = DataType.Int32)]
        public int SettingID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.SettingID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.SettingID = value;
		}

		bool IPoco.IsNew()
		{
			return this.SettingID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'setting_value'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "setting_value", DataType = DataType.NVarChar)]
        public string SettingValue { get; set; }
        
        /// <summary>
		/// <para>Column name: 'setting_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "setting_name", DataType = DataType.NVarChar)]
        public string SettingName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows")]
    public class ShowPoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'show_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ShowID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ShowID = value;
		}

		bool IPoco.IsNew()
		{
			return this.ShowID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'air_day'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "air_day", DataType = DataType.Int32)]
        public int? AirDay { get; set; }
        
        /// <summary>
		/// <para>Column name: 'air_time'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "air_time", DataType = DataType.DateTime2)]
        public DateTime? AirTime { get; set; }
        
        /// <summary>
		/// <para>Column name: 'first_aired'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "first_aired", DataType = DataType.DateTime2)]
        public DateTime? FirstAired { get; set; }
        
        /// <summary>
		/// <para>Column name: 'imdbid'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "imdbid", DataType = DataType.NVarChar)]
        public string Imdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'network_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "network_id", DataType = DataType.Int32)]
        public int NetworkID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_banner'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "show_banner", DataType = DataType.NVarChar)]
        public string ShowBanner { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_description'.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
        /// </summary>        
        [Nullable]
		[Column(Name = "show_description", DataType = DataType.Text)]
        public string ShowDescription { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_name'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "show_name", DataType = DataType.NVarChar)]
        public string ShowName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_status'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "show_status", DataType = DataType.Int32)]
        public int ShowStatus { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows_genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows_genres")]
    public class ShowGenrePoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'shows_genres_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "shows_genres_id", DataType = DataType.Int32)]
        public int ShowsGenresID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ShowsGenresID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ShowsGenresID = value;
		}

		bool IPoco.IsNew()
		{
			return this.ShowsGenresID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'genre_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "genre_id", DataType = DataType.Int32)]
        public int GenreID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'subscriptions'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "subscriptions")]
    public class SubscriptionPoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'subscription_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "subscription_id", DataType = DataType.Int32)]
        public int SubscriptionID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.SubscriptionID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.SubscriptionID = value;
		}

		bool IPoco.IsNew()
		{
			return this.SubscriptionID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'update_queue'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "update_queue")]
    public class UpdateQueuePoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'update_queue_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "update_queue_id", DataType = DataType.Int32)]
        public int UpdateQueueID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.UpdateQueueID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.UpdateQueueID = value;
		}

		bool IPoco.IsNew()
		{
			return this.UpdateQueueID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'last_failed_time'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "last_failed_time", DataType = DataType.DateTime2)]
        public DateTime LastFailedTime { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdb_last_updated'.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "thetvdb_last_updated", DataType = DataType.DateTime2)]
        public DateTime ThetvdbLastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdb_update_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "thetvdb_update_id", DataType = DataType.Int32)]
        public int ThetvdbUpdateID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'fail_count'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "fail_count", DataType = DataType.Int32)]
        public int FailCount { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'users'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "users")]
    public class UserPoco : IPoco
    {
        /// <summary>
		/// <para>Primary key column.</para>
		/// <para>Column name: 'user_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [PrimaryKey, Identity]
		[Column(Name = "user_id", DataType = DataType.Int32)]
        public int UserID { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.UserID;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.UserID = value;
		}

		bool IPoco.IsNew()
		{
			return this.UserID == default;
		}
        
        /// <summary>
		/// <para>Column name: 'is_admin'.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "is_admin", DataType = DataType.Boolean)]
        public bool IsAdmin { get; set; }
        
        /// <summary>
		/// <para>Column name: 'username'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "username", DataType = DataType.NVarChar)]
        public string Username { get; set; }
        
        /// <summary>
		/// <para>Column name: 'password'.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "password", DataType = DataType.NVarChar)]
        public string Password { get; set; }
        
        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
        [NotNull]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }
        
    }
    
    public partial class DbService
    {
		private readonly IReadOnlyDictionary<Type, string> primaryKeyMap = new Dictionary<Type, string>
		{
			{typeof(ActorPoco), "actor_id"},
			{typeof(ApiResponsePoco), "api_response_id"},
			{typeof(EpisodePoco), "episode_id"},
			{typeof(GenrePoco), "genre_id"},
			{typeof(NetworkPoco), "network_id"},
			{typeof(ProfilePoco), "profile_id"},
			{typeof(RolePoco), "role_id"},
			{typeof(SettingPoco), "setting_id"},
			{typeof(ShowPoco), "show_id"},
			{typeof(ShowGenrePoco), "shows_genres_id"},
			{typeof(SubscriptionPoco), "subscription_id"},
			{typeof(UpdateQueuePoco), "update_queue_id"},
			{typeof(UserPoco), "user_id"},
		};

		private readonly IReadOnlyDictionary<Type, string> tableNameMap = new Dictionary<Type, string>
		{
			{typeof(ActorPoco), "actors"},
			{typeof(ApiResponsePoco), "api_responses"},
			{typeof(EpisodePoco), "episodes"},
			{typeof(GenrePoco), "genres"},
			{typeof(NetworkPoco), "networks"},
			{typeof(ProfilePoco), "profiles"},
			{typeof(RolePoco), "roles"},
			{typeof(SettingPoco), "settings"},
			{typeof(ShowPoco), "shows"},
			{typeof(ShowGenrePoco), "shows_genres"},
			{typeof(SubscriptionPoco), "subscriptions"},
			{typeof(UpdateQueuePoco), "update_queue"},
			{typeof(UserPoco), "users"},
		};

		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        public IQueryable<ActorPoco> Actors => this.DataConnection.GetTable<ActorPoco>();
		
		/// <summary>
		/// <para>Database table 'api_responses'.</para>		
		/// </summary>
        public IQueryable<ApiResponsePoco> ApiResponses => this.DataConnection.GetTable<ApiResponsePoco>();
		
		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        public IQueryable<EpisodePoco> Episodes => this.DataConnection.GetTable<EpisodePoco>();
		
		/// <summary>
		/// <para>Database table 'genres'.</para>		
		/// </summary>
        public IQueryable<GenrePoco> Genres => this.DataConnection.GetTable<GenrePoco>();
		
		/// <summary>
		/// <para>Database table 'networks'.</para>		
		/// </summary>
        public IQueryable<NetworkPoco> Networks => this.DataConnection.GetTable<NetworkPoco>();
		
		/// <summary>
		/// <para>Database table 'profiles'.</para>		
		/// </summary>
        public IQueryable<ProfilePoco> Profiles => this.DataConnection.GetTable<ProfilePoco>();
		
		/// <summary>
		/// <para>Database table 'roles'.</para>		
		/// </summary>
        public IQueryable<RolePoco> Roles => this.DataConnection.GetTable<RolePoco>();
		
		/// <summary>
		/// <para>Database table 'settings'.</para>		
		/// </summary>
        public IQueryable<SettingPoco> Settings => this.DataConnection.GetTable<SettingPoco>();
		
		/// <summary>
		/// <para>Database table 'shows'.</para>		
		/// </summary>
        public IQueryable<ShowPoco> Shows => this.DataConnection.GetTable<ShowPoco>();
		
		/// <summary>
		/// <para>Database table 'shows_genres'.</para>		
		/// </summary>
        public IQueryable<ShowGenrePoco> ShowsGenres => this.DataConnection.GetTable<ShowGenrePoco>();
		
		/// <summary>
		/// <para>Database table 'subscriptions'.</para>		
		/// </summary>
        public IQueryable<SubscriptionPoco> Subscriptions => this.DataConnection.GetTable<SubscriptionPoco>();
		
		/// <summary>
		/// <para>Database table 'update_queue'.</para>		
		/// </summary>
        public IQueryable<UpdateQueuePoco> UpdateQueue => this.DataConnection.GetTable<UpdateQueuePoco>();
		
		/// <summary>
		/// <para>Database table 'users'.</para>		
		/// </summary>
        public IQueryable<UserPoco> Users => this.DataConnection.GetTable<UserPoco>();
		
    }

	public partial interface IDbService
    {
		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        IQueryable<ActorPoco> Actors { get; }

		/// <summary>
		/// <para>Database table 'api_responses'.</para>		
		/// </summary>
        IQueryable<ApiResponsePoco> ApiResponses { get; }

		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        IQueryable<EpisodePoco> Episodes { get; }

		/// <summary>
		/// <para>Database table 'genres'.</para>		
		/// </summary>
        IQueryable<GenrePoco> Genres { get; }

		/// <summary>
		/// <para>Database table 'networks'.</para>		
		/// </summary>
        IQueryable<NetworkPoco> Networks { get; }

		/// <summary>
		/// <para>Database table 'profiles'.</para>		
		/// </summary>
        IQueryable<ProfilePoco> Profiles { get; }

		/// <summary>
		/// <para>Database table 'roles'.</para>		
		/// </summary>
        IQueryable<RolePoco> Roles { get; }

		/// <summary>
		/// <para>Database table 'settings'.</para>		
		/// </summary>
        IQueryable<SettingPoco> Settings { get; }

		/// <summary>
		/// <para>Database table 'shows'.</para>		
		/// </summary>
        IQueryable<ShowPoco> Shows { get; }

		/// <summary>
		/// <para>Database table 'shows_genres'.</para>		
		/// </summary>
        IQueryable<ShowGenrePoco> ShowsGenres { get; }

		/// <summary>
		/// <para>Database table 'subscriptions'.</para>		
		/// </summary>
        IQueryable<SubscriptionPoco> Subscriptions { get; }

		/// <summary>
		/// <para>Database table 'update_queue'.</para>		
		/// </summary>
        IQueryable<UpdateQueuePoco> UpdateQueue { get; }

		/// <summary>
		/// <para>Database table 'users'.</para>		
		/// </summary>
        IQueryable<UserPoco> Users { get; }

    }
}
