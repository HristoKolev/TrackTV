namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LinqToDB.Mapping;

    /// <summary>
    /// <para>Database table 'actors'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "actors")]
    public class ActorPoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "actor_id")]
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
        
        [Nullable][Column(Name = "actor_image")]
        public string ActorImage { get; set; }
        
        [NotNull][Column(Name = "actor_name")]
        public string ActorName { get; set; }
        
        [NotNull][Column(Name = "last_updated")]
        public DateTime LastUpdated { get; set; }
        
        [NotNull][Column(Name = "thetvdbid")]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'api_responses'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_responses")]
    public class ApiResponsePoco : IPoco
    {
        [Nullable][Column(Name = "api_response_episode_thetvdbid")]
        public int? ApiResponseEpisodeThetvdbid { get; set; }
        
        [Nullable][Column(Name = "api_response_show_thetvdbid")]
        public int? ApiResponseShowThetvdbid { get; set; }
        
        [NotNull][Column(Name = "api_response_body")]
        public string ApiResponseBody { get; set; }
        
        [PrimaryKey, Identity][Column(Name = "api_response_id")]
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
        
    }
    
    /// <summary>
    /// <para>Database table 'episodes'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "episodes")]
    public class EpisodePoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "episode_id")]
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
        
        [Nullable][Column(Name = "episode_description")]
        public string EpisodeDescription { get; set; }
        
        [NotNull][Column(Name = "episode_number")]
        public int EpisodeNumber { get; set; }
        
        [Nullable][Column(Name = "episode_title")]
        public string EpisodeTitle { get; set; }
        
        [Nullable][Column(Name = "first_aired")]
        public DateTime? FirstAired { get; set; }
        
        [Nullable][Column(Name = "imdbid")]
        public string Imdbid { get; set; }
        
        [NotNull][Column(Name = "last_updated")]
        public DateTime LastUpdated { get; set; }
        
        [NotNull][Column(Name = "season_number")]
        public int SeasonNumber { get; set; }
        
        [NotNull][Column(Name = "show_id")]
        public int ShowID { get; set; }
        
        [NotNull][Column(Name = "thetvdbid")]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "genres")]
    public class GenrePoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "genre_id")]
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
        
        [NotNull][Column(Name = "genre_name")]
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'networks'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "networks")]
    public class NetworkPoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "network_id")]
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
        
        [NotNull][Column(Name = "network_name")]
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'profiles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "profiles")]
    public class ProfilePoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "profile_id")]
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
        
        [NotNull][Column(Name = "profile_name")]
        public string ProfileName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'roles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "roles")]
    public class RolePoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "role_id")]
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
        
        [NotNull][Column(Name = "actor_id")]
        public int ActorID { get; set; }
        
        [Nullable][Column(Name = "role_name")]
        public string RoleName { get; set; }
        
        [NotNull][Column(Name = "show_id")]
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'settings'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "settings")]
    public class SettingPoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "setting_id")]
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
        
        [NotNull][Column(Name = "setting_value")]
        public string SettingValue { get; set; }
        
        [NotNull][Column(Name = "setting_name")]
        public string SettingName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows")]
    public class ShowPoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "show_id")]
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
        
        [Nullable][Column(Name = "air_day")]
        public int? AirDay { get; set; }
        
        [Nullable][Column(Name = "air_time")]
        public DateTime? AirTime { get; set; }
        
        [Nullable][Column(Name = "first_aired")]
        public DateTime? FirstAired { get; set; }
        
        [Nullable][Column(Name = "imdbid")]
        public string Imdbid { get; set; }
        
        [NotNull][Column(Name = "last_updated")]
        public DateTime LastUpdated { get; set; }
        
        [NotNull][Column(Name = "network_id")]
        public int NetworkID { get; set; }
        
        [Nullable][Column(Name = "show_banner")]
        public string ShowBanner { get; set; }
        
        [Nullable][Column(Name = "show_description")]
        public string ShowDescription { get; set; }
        
        [NotNull][Column(Name = "show_name")]
        public string ShowName { get; set; }
        
        [NotNull][Column(Name = "show_status")]
        public int ShowStatus { get; set; }
        
        [NotNull][Column(Name = "thetvdbid")]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows_genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows_genres")]
    public class ShowGenrePoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "shows_genres_id")]
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
        
        [NotNull][Column(Name = "show_id")]
        public int ShowID { get; set; }
        
        [NotNull][Column(Name = "genre_id")]
        public int GenreID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'subscriptions'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "subscriptions")]
    public class SubscriptionPoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "subscription_id")]
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
        
        [NotNull][Column(Name = "profile_id")]
        public int ProfileID { get; set; }
        
        [NotNull][Column(Name = "show_id")]
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'update_queue'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "update_queue")]
    public class UpdateQueuePoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "update_queue_id")]
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
        
        [NotNull][Column(Name = "last_failed_time")]
        public DateTime LastFailedTime { get; set; }
        
        [NotNull][Column(Name = "thetvdb_last_updated")]
        public DateTime ThetvdbLastUpdated { get; set; }
        
        [NotNull][Column(Name = "thetvdb_update_id")]
        public int ThetvdbUpdateID { get; set; }
        
        [NotNull][Column(Name = "fail_count")]
        public int FailCount { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'users'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "users")]
    public class UserPoco : IPoco
    {
        [PrimaryKey, Identity][Column(Name = "user_id")]
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
        
        [NotNull][Column(Name = "is_admin")]
        public bool IsAdmin { get; set; }
        
        [NotNull][Column(Name = "username")]
        public string Username { get; set; }
        
        [NotNull][Column(Name = "password")]
        public string Password { get; set; }
        
        [NotNull][Column(Name = "profile_id")]
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
