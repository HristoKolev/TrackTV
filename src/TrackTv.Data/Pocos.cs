namespace TrackTv.Data
{
    using System;
    using System.Linq;
    using LinqToDB.Mapping;

    /// <summary>
    /// <para>Database table 'actors'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "actors")]
    public class ActorPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ActorId { get; set; }
        
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
    /// <para>Database table 'episodes'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "episodes")]
    public class EpisodePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int EpisodeId { get; set; }
        
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
    /// <para>Database table 'genres'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "genres")]
    public class GenrePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int GenreId { get; set; }
        
        [Column(Name = "GenreName")][NotNull] 
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'networks'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "networks")]
    public class NetworkPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int NetworkId { get; set; }
        
        [Column(Name = "NetworkName")][NotNull] 
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'profiles'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "profiles")]
    public class ProfilePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ProfileId { get; set; }
        
        [Column(Name = "Username")][NotNull] 
        public string Username { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'roles'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "roles")]
    public class RolePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int RoleId { get; set; }
        
        [Column(Name = "ActorId")][NotNull] 
        public int ActorId { get; set; }
        
        [Column(Name = "RoleName")][Nullable] 
        public string RoleName { get; set; }
        
        [Column(Name = "ShowId")][NotNull] 
        public int ShowId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'settings'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
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
        
    }
    
    /// <summary>
    /// <para>Database table 'shows'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "shows")]
    public class ShowPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ShowId { get; set; }
        
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
    /// <para>Database table 'shows_genres'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
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
        
    }
    
    /// <summary>
    /// <para>Database table 'subscriptions'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "subscriptions")]
    public class SubscriptionPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int SubscriptionId { get; set; }
        
        [Column(Name = "ProfileId")][NotNull] 
        public int ProfileId { get; set; }
        
        [Column(Name = "ShowId")][NotNull] 
        public int ShowId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'users'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-10T12:19:35.3811496+02:00</para>
    /// </summary>
    [Table(Name = "users")]
    public class UserPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int UserId { get; set; }
        
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
        public IQueryable<ActorPoco> Actors => this.DataConnection.GetTable<ActorPoco>();

        public IQueryable<EpisodePoco> Episodes => this.DataConnection.GetTable<EpisodePoco>();

        public IQueryable<GenrePoco> Genres => this.DataConnection.GetTable<GenrePoco>();

        public IQueryable<NetworkPoco> Networks => this.DataConnection.GetTable<NetworkPoco>();

        public IQueryable<ProfilePoco> Profiles => this.DataConnection.GetTable<ProfilePoco>();

        public IQueryable<RolePoco> Roles => this.DataConnection.GetTable<RolePoco>();

        public IQueryable<SettingPoco> Settings => this.DataConnection.GetTable<SettingPoco>();

        public IQueryable<ShowPoco> Shows => this.DataConnection.GetTable<ShowPoco>();

        public IQueryable<ShowGenrePoco> ShowsGenres => this.DataConnection.GetTable<ShowGenrePoco>();

        public IQueryable<SubscriptionPoco> Subscriptions => this.DataConnection.GetTable<SubscriptionPoco>();

        public IQueryable<UserPoco> Users => this.DataConnection.GetTable<UserPoco>();

    }
}
