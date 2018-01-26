namespace TrackTv.Data
{
    using System;
    using System.Linq;
    using LinqToDB.Mapping;
	using LinqToDB.Data;
	using System.Threading.Tasks;
	using System.Runtime.CompilerServices;

    /// <summary>
    /// <para>Database table 'actors'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "actors")]
    public class ActorPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ActorId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ActorId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ActorId = value;
		}

		bool IPoco.IsNew()
		{
			return this.ActorId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "ActorId";
		}

        
        [Column(Name = "ActorImage")][Nullable] 
        public string ActorImage { get; set; }
        
        [Column(Name = "ActorName")][NotNull] 
        public string ActorName { get; set; }
        
        [Column(Name = "LastUpdated")][NotNull] 
        public DateTime LastUpdated { get; set; }
        
        [Column(Name = "TheTvDbId")][NotNull] 
        public int TheTvDbId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'episodes'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "episodes")]
    public class EpisodePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int EpisodeId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.EpisodeId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.EpisodeId = value;
		}

		bool IPoco.IsNew()
		{
			return this.EpisodeId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "EpisodeId";
		}

        
        [Column(Name = "EpisodeDescription")][Nullable] 
        public string EpisodeDescription { get; set; }
        
        [Column(Name = "EpisodeNumber")][NotNull] 
        public int EpisodeNumber { get; set; }
        
        [Column(Name = "EpisodeTitle")][Nullable] 
        public string EpisodeTitle { get; set; }
        
        [Column(Name = "FirstAired")][Nullable] 
        public DateTime? FirstAired { get; set; }
        
        [Column(Name = "ImdbId")][Nullable] 
        public string ImdbId { get; set; }
        
        [Column(Name = "LastUpdated")][NotNull] 
        public DateTime LastUpdated { get; set; }
        
        [Column(Name = "SeasonNumber")][NotNull] 
        public int SeasonNumber { get; set; }
        
        [Column(Name = "ShowId")][NotNull] 
        public int ShowId { get; set; }
        
        [Column(Name = "TheTvDbId")][NotNull] 
        public int TheTvDbId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'failed_updates'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "failed_updates")]
    public class FailedUpdatePoco : IPoco
    {
        [Column(Name = "FailedTime")][NotNull] 
        public DateTime FailedTime { get; set; }
        
        [Column(Name = "TheTvDbLastUpdated")][NotNull] 
        public DateTime TheTvDbLastUpdated { get; set; }
        
        [Column(Name = "TheTvDbUpdateId")][NotNull] 
        public int TheTvDbUpdateId { get; set; }
        
        [PrimaryKey, Identity] 
        public int FailedUpdateId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.FailedUpdateId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.FailedUpdateId = value;
		}

		bool IPoco.IsNew()
		{
			return this.FailedUpdateId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "FailedUpdateId";
		}

        
        [Column(Name = "NumberOfFails")][NotNull] 
        public int NumberOfFails { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "genres")]
    public class GenrePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int GenreId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.GenreId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.GenreId = value;
		}

		bool IPoco.IsNew()
		{
			return this.GenreId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "GenreId";
		}

        
        [Column(Name = "GenreName")][NotNull] 
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'networks'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "networks")]
    public class NetworkPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int NetworkId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.NetworkId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.NetworkId = value;
		}

		bool IPoco.IsNew()
		{
			return this.NetworkId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "NetworkId";
		}

        
        [Column(Name = "NetworkName")][NotNull] 
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'profiles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "profiles")]
    public class ProfilePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ProfileId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ProfileId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ProfileId = value;
		}

		bool IPoco.IsNew()
		{
			return this.ProfileId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "ProfileId";
		}

        
        [Column(Name = "Username")][NotNull] 
        public string Username { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'roles'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "roles")]
    public class RolePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int RoleId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.RoleId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.RoleId = value;
		}

		bool IPoco.IsNew()
		{
			return this.RoleId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "RoleId";
		}

        
        [Column(Name = "ActorId")][NotNull] 
        public int ActorId { get; set; }
        
        [Column(Name = "RoleName")][Nullable] 
        public string RoleName { get; set; }
        
        [Column(Name = "ShowId")][NotNull] 
        public int ShowId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'settings'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "settings")]
    public class SettingPoco : IPoco
    {
        [Column(Name = "SettingValue")][NotNull] 
        public string SettingValue { get; set; }
        
        [Column(Name = "SettingName")][NotNull] 
        public string SettingName { get; set; }
        
        [PrimaryKey, Identity] 
        public int SettingId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.SettingId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.SettingId = value;
		}

		bool IPoco.IsNew()
		{
			return this.SettingId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "SettingId";
		}

        
    }
    
    /// <summary>
    /// <para>Database table 'shows'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "shows")]
    public class ShowPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ShowId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ShowId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ShowId = value;
		}

		bool IPoco.IsNew()
		{
			return this.ShowId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "ShowId";
		}

        
        [Column(Name = "AirDay")][Nullable] 
        public int? AirDay { get; set; }
        
        [Column(Name = "AirTime")][Nullable] 
        public DateTime? AirTime { get; set; }
        
        [Column(Name = "FirstAired")][Nullable] 
        public DateTime? FirstAired { get; set; }
        
        [Column(Name = "ImdbId")][Nullable] 
        public string ImdbId { get; set; }
        
        [Column(Name = "LastUpdated")][NotNull] 
        public DateTime LastUpdated { get; set; }
        
        [Column(Name = "NetworkId")][NotNull] 
        public int NetworkId { get; set; }
        
        [Column(Name = "ShowBanner")][Nullable] 
        public string ShowBanner { get; set; }
        
        [Column(Name = "ShowDescription")][Nullable] 
        public string ShowDescription { get; set; }
        
        [Column(Name = "ShowName")][NotNull] 
        public string ShowName { get; set; }
        
        [Column(Name = "ShowStatus")][NotNull] 
        public int ShowStatus { get; set; }
        
        [Column(Name = "TheTvDbId")][NotNull] 
        public int TheTvDbId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'shows_genres'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "shows_genres")]
    public class ShowGenrePoco : IPoco
    {
        [Column(Name = "ShowId")][NotNull] 
        public int ShowId { get; set; }
        
        [Column(Name = "GenreId")][NotNull] 
        public int GenreId { get; set; }
        
        [PrimaryKey, Identity] 
        public int ShowsGenresId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.ShowsGenresId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.ShowsGenresId = value;
		}

		bool IPoco.IsNew()
		{
			return this.ShowsGenresId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "ShowsGenresId";
		}

        
    }
    
    /// <summary>
    /// <para>Database table 'subscriptions'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "subscriptions")]
    public class SubscriptionPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int SubscriptionId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.SubscriptionId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.SubscriptionId = value;
		}

		bool IPoco.IsNew()
		{
			return this.SubscriptionId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "SubscriptionId";
		}

        
        [Column(Name = "ProfileId")][NotNull] 
        public int ProfileId { get; set; }
        
        [Column(Name = "ShowId")][NotNull] 
        public int ShowId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'users'.</para>
    /// <para>This class is automatically generated.</para>
    /// </summary>
    [Table(Name = "users")]
    public class UserPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int UserId { get; set; }

		int IPoco.GetPrimaryKey()
		{
			return this.UserId;
		}

		void IPoco.SetPrimaryKey(int value)
		{
			this.UserId = value;
		}

		bool IPoco.IsNew()
		{
			return this.UserId == default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string GetPrimaryKeyColumnName()
		{
			return "UserId";
		}

        
        [Column(Name = "IsAdmin")][NotNull] 
        public bool IsAdmin { get; set; }
        
        [Column(Name = "Username")][NotNull] 
        public string Username { get; set; }
        
        [Column(Name = "Password")][NotNull] 
        public string Password { get; set; }
        
        [Column(Name = "ProfileId")][NotNull] 
        public int ProfileId { get; set; }
        
    }
    
    public partial class DbService
    {
		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        public IQueryable<ActorPoco> Actors => this.DataConnection.GetTable<ActorPoco>();
		
		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        public IQueryable<EpisodePoco> Episodes => this.DataConnection.GetTable<EpisodePoco>();
		
		/// <summary>
		/// <para>Database table 'failed_updates'.</para>		
		/// </summary>
        public IQueryable<FailedUpdatePoco> FailedUpdates => this.DataConnection.GetTable<FailedUpdatePoco>();
		
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
		/// <para>Database table 'users'.</para>		
		/// </summary>
        public IQueryable<UserPoco> Users => this.DataConnection.GetTable<UserPoco>();
		

		/// <summary>
		/// <para>Deletes a record from a table mapped to <see cref="TPoco"/> by ID.</para>
		/// </summary>
		public Task Delete<TPoco>(int id) where TPoco : IPoco
		{
			if(typeof(TPoco) == typeof(ActorPoco)) return this.DataConnection.ExecuteAsync("DELETE FROM actors WHERE " + ActorPoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(EpisodePoco)) return this.DataConnection.ExecuteAsync("DELETE FROM episodes WHERE " + EpisodePoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(FailedUpdatePoco)) return this.DataConnection.ExecuteAsync("DELETE FROM failed_updates WHERE " + FailedUpdatePoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(GenrePoco)) return this.DataConnection.ExecuteAsync("DELETE FROM genres WHERE " + GenrePoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(NetworkPoco)) return this.DataConnection.ExecuteAsync("DELETE FROM networks WHERE " + NetworkPoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(ProfilePoco)) return this.DataConnection.ExecuteAsync("DELETE FROM profiles WHERE " + ProfilePoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(RolePoco)) return this.DataConnection.ExecuteAsync("DELETE FROM roles WHERE " + RolePoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(SettingPoco)) return this.DataConnection.ExecuteAsync("DELETE FROM settings WHERE " + SettingPoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(ShowPoco)) return this.DataConnection.ExecuteAsync("DELETE FROM shows WHERE " + ShowPoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(ShowGenrePoco)) return this.DataConnection.ExecuteAsync("DELETE FROM shows_genres WHERE " + ShowGenrePoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(SubscriptionPoco)) return this.DataConnection.ExecuteAsync("DELETE FROM subscriptions WHERE " + SubscriptionPoco.GetPrimaryKeyColumnName() + " = " + id + ";");
			if(typeof(TPoco) == typeof(UserPoco)) return this.DataConnection.ExecuteAsync("DELETE FROM users WHERE " + UserPoco.GetPrimaryKeyColumnName() + " = " + id + ";");

			throw new ArgumentOutOfRangeException("TPoco");
		}
    }

	public partial interface IDbService
    {
		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        IQueryable<ActorPoco> Actors { get; }

		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        IQueryable<EpisodePoco> Episodes { get; }

		/// <summary>
		/// <para>Database table 'failed_updates'.</para>		
		/// </summary>
        IQueryable<FailedUpdatePoco> FailedUpdates { get; }

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
		/// <para>Database table 'users'.</para>		
		/// </summary>
        IQueryable<UserPoco> Users { get; }

    }
}
