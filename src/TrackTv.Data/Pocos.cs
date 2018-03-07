namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LinqToDB.Mapping;

    /// <summary>
    /// <para>Database table 'users'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "users")]
    public class UserPoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "is_admin")][NotNull] 
        public bool IsAdmin { get; set; }
        
        [Column(Name = "username")][NotNull] 
        public string Username { get; set; }
        
        [Column(Name = "password")][NotNull] 
        public string Password { get; set; }
        
        [Column(Name = "profile_id")][NotNull] 
        public int ProfileID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'settings'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "settings")]
    public class SettingPoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "setting_value")][NotNull] 
        public string SettingValue { get; set; }
        
        [Column(Name = "setting_name")][NotNull] 
        public string SettingName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'update_queue'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "update_queue")]
    public class UpdateQueuePoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "last_failed_time")][NotNull] 
        public DateTime LastFailedTime { get; set; }
        
        [Column(Name = "thetvdb_last_updated")][NotNull] 
        public DateTime ThetvdbLastUpdated { get; set; }
        
        [Column(Name = "thetvdb_update_id")][NotNull] 
        public int ThetvdbUpdateID { get; set; }
        
        [Column(Name = "fail_count")][NotNull] 
        public int FailCount { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'episodes'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "episodes")]
    public class EpisodePoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "episode_description")][Nullable] 
        public string EpisodeDescription { get; set; }
        
        [Column(Name = "episode_number")][NotNull] 
        public int EpisodeNumber { get; set; }
        
        [Column(Name = "episode_title")][Nullable] 
        public string EpisodeTitle { get; set; }
        
        [Column(Name = "first_aired")][Nullable] 
        public DateTime? FirstAired { get; set; }
        
        [Column(Name = "imdbid")][Nullable] 
        public string Imdbid { get; set; }
        
        [Column(Name = "last_updated")][NotNull] 
        public DateTime LastUpdated { get; set; }
        
        [Column(Name = "season_number")][NotNull] 
        public int SeasonNumber { get; set; }
        
        [Column(Name = "show_id")][NotNull] 
        public int ShowID { get; set; }
        
        [Column(Name = "thetvdbid")][NotNull] 
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'actors'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "actors")]
    public class ActorPoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "actor_image")][Nullable] 
        public string ActorImage { get; set; }
        
        [Column(Name = "actor_name")][NotNull] 
        public string ActorName { get; set; }
        
        [Column(Name = "last_updated")][NotNull] 
        public DateTime LastUpdated { get; set; }
        
        [Column(Name = "thetvdbid")][NotNull] 
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'roles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "roles")]
    public class RolePoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "actor_id")][NotNull] 
        public int ActorID { get; set; }
        
        [Column(Name = "role_name")][Nullable] 
        public string RoleName { get; set; }
        
        [Column(Name = "show_id")][NotNull] 
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'networks'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "networks")]
    public class NetworkPoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "network_name")][NotNull] 
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "genres")]
    public class GenrePoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "genre_name")][NotNull] 
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows_genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows_genres")]
    public class ShowGenrePoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "show_id")][NotNull] 
        public int ShowID { get; set; }
        
        [Column(Name = "genre_id")][NotNull] 
        public int GenreID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'profiles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "profiles")]
    public class ProfilePoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "profile_name")][NotNull] 
        public string ProfileName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows")]
    public class ShowPoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "air_day")][Nullable] 
        public int? AirDay { get; set; }
        
        [Column(Name = "air_time")][Nullable] 
        public DateTime? AirTime { get; set; }
        
        [Column(Name = "first_aired")][Nullable] 
        public DateTime? FirstAired { get; set; }
        
        [Column(Name = "imdbid")][Nullable] 
        public string Imdbid { get; set; }
        
        [Column(Name = "last_updated")][NotNull] 
        public DateTime LastUpdated { get; set; }
        
        [Column(Name = "network_id")][NotNull] 
        public int NetworkID { get; set; }
        
        [Column(Name = "show_banner")][Nullable] 
        public string ShowBanner { get; set; }
        
        [Column(Name = "show_description")][Nullable] 
        public string ShowDescription { get; set; }
        
        [Column(Name = "show_name")][NotNull] 
        public string ShowName { get; set; }
        
        [Column(Name = "show_status")][NotNull] 
        public int ShowStatus { get; set; }
        
        [Column(Name = "thetvdbid")][NotNull] 
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'subscriptions'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Schema="public", Name = "subscriptions")]
    public class SubscriptionPoco : IPoco
    {
        [PrimaryKey, Identity] 
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
        
        [Column(Name = "profile_id")][NotNull] 
        public int ProfileID { get; set; }
        
        [Column(Name = "show_id")][NotNull] 
        public int ShowID { get; set; }
        
    }
    
    public partial class DbService
    {
		private readonly IReadOnlyDictionary<Type, string> primaryKeyMap = new Dictionary<Type, string>
		{
			{typeof(UserPoco), "user_id"},
			{typeof(SettingPoco), "setting_id"},
			{typeof(UpdateQueuePoco), "update_queue_id"},
			{typeof(EpisodePoco), "episode_id"},
			{typeof(ActorPoco), "actor_id"},
			{typeof(RolePoco), "role_id"},
			{typeof(NetworkPoco), "network_id"},
			{typeof(GenrePoco), "genre_id"},
			{typeof(ShowGenrePoco), "shows_genres_id"},
			{typeof(ProfilePoco), "profile_id"},
			{typeof(ShowPoco), "show_id"},
			{typeof(SubscriptionPoco), "subscription_id"},
		};

		private readonly IReadOnlyDictionary<Type, string> tableNameMap = new Dictionary<Type, string>
		{
			{typeof(UserPoco), "users"},
			{typeof(SettingPoco), "settings"},
			{typeof(UpdateQueuePoco), "update_queue"},
			{typeof(EpisodePoco), "episodes"},
			{typeof(ActorPoco), "actors"},
			{typeof(RolePoco), "roles"},
			{typeof(NetworkPoco), "networks"},
			{typeof(GenrePoco), "genres"},
			{typeof(ShowGenrePoco), "shows_genres"},
			{typeof(ProfilePoco), "profiles"},
			{typeof(ShowPoco), "shows"},
			{typeof(SubscriptionPoco), "subscriptions"},
		};

		/// <summary>
		/// <para>Database table 'users'.</para>		
		/// </summary>
        public IQueryable<UserPoco> Users => this.DataConnection.GetTable<UserPoco>();
		
		/// <summary>
		/// <para>Database table 'settings'.</para>		
		/// </summary>
        public IQueryable<SettingPoco> Settings => this.DataConnection.GetTable<SettingPoco>();
		
		/// <summary>
		/// <para>Database table 'update_queue'.</para>		
		/// </summary>
        public IQueryable<UpdateQueuePoco> UpdateQueue => this.DataConnection.GetTable<UpdateQueuePoco>();
		
		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        public IQueryable<EpisodePoco> Episodes => this.DataConnection.GetTable<EpisodePoco>();
		
		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        public IQueryable<ActorPoco> Actors => this.DataConnection.GetTable<ActorPoco>();
		
		/// <summary>
		/// <para>Database table 'roles'.</para>		
		/// </summary>
        public IQueryable<RolePoco> Roles => this.DataConnection.GetTable<RolePoco>();
		
		/// <summary>
		/// <para>Database table 'networks'.</para>		
		/// </summary>
        public IQueryable<NetworkPoco> Networks => this.DataConnection.GetTable<NetworkPoco>();
		
		/// <summary>
		/// <para>Database table 'genres'.</para>		
		/// </summary>
        public IQueryable<GenrePoco> Genres => this.DataConnection.GetTable<GenrePoco>();
		
		/// <summary>
		/// <para>Database table 'shows_genres'.</para>		
		/// </summary>
        public IQueryable<ShowGenrePoco> ShowsGenres => this.DataConnection.GetTable<ShowGenrePoco>();
		
		/// <summary>
		/// <para>Database table 'profiles'.</para>		
		/// </summary>
        public IQueryable<ProfilePoco> Profiles => this.DataConnection.GetTable<ProfilePoco>();
		
		/// <summary>
		/// <para>Database table 'shows'.</para>		
		/// </summary>
        public IQueryable<ShowPoco> Shows => this.DataConnection.GetTable<ShowPoco>();
		
		/// <summary>
		/// <para>Database table 'subscriptions'.</para>		
		/// </summary>
        public IQueryable<SubscriptionPoco> Subscriptions => this.DataConnection.GetTable<SubscriptionPoco>();
		
    }

	public partial interface IDbService
    {
		/// <summary>
		/// <para>Database table 'users'.</para>		
		/// </summary>
        IQueryable<UserPoco> Users { get; }

		/// <summary>
		/// <para>Database table 'settings'.</para>		
		/// </summary>
        IQueryable<SettingPoco> Settings { get; }

		/// <summary>
		/// <para>Database table 'update_queue'.</para>		
		/// </summary>
        IQueryable<UpdateQueuePoco> UpdateQueue { get; }

		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        IQueryable<EpisodePoco> Episodes { get; }

		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        IQueryable<ActorPoco> Actors { get; }

		/// <summary>
		/// <para>Database table 'roles'.</para>		
		/// </summary>
        IQueryable<RolePoco> Roles { get; }

		/// <summary>
		/// <para>Database table 'networks'.</para>		
		/// </summary>
        IQueryable<NetworkPoco> Networks { get; }

		/// <summary>
		/// <para>Database table 'genres'.</para>		
		/// </summary>
        IQueryable<GenrePoco> Genres { get; }

		/// <summary>
		/// <para>Database table 'shows_genres'.</para>		
		/// </summary>
        IQueryable<ShowGenrePoco> ShowsGenres { get; }

		/// <summary>
		/// <para>Database table 'profiles'.</para>		
		/// </summary>
        IQueryable<ProfilePoco> Profiles { get; }

		/// <summary>
		/// <para>Database table 'shows'.</para>		
		/// </summary>
        IQueryable<ShowPoco> Shows { get; }

		/// <summary>
		/// <para>Database table 'subscriptions'.</para>		
		/// </summary>
        IQueryable<SubscriptionPoco> Subscriptions { get; }

    }
}
