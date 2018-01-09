namespace TrackTv.Data
{
    using System;
    using System.Linq;
    using LinqToDB.Mapping;

    /// <summary>
    /// <para>Database table 'Actors'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Actors")]
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
    /// <para>Database table 'Episodes'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Episodes")]
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
    /// <para>Database table 'Genres'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Genres")]
    public class GenrePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int GenreId { get; set; }
        
        [Column(Name = "GenreName")][NotNull] 
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'Networks'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Networks")]
    public class NetworkPoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int NetworkId { get; set; }
        
        [Column(Name = "NetworkName")][NotNull] 
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'Profiles'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Profiles")]
    public class ProfilePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ProfileId { get; set; }
        
        [Column(Name = "Username")][NotNull] 
        public string Username { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'Roles'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Roles")]
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
    /// <para>Database table 'Shows'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Shows")]
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
    /// <para>Database table 'ShowsGenres'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "ShowsGenres")]
    public class ShowsgenrePoco : IPoco
    {
        [PrimaryKey, Identity] 
        public int ShowId { get; set; }
        
        [PrimaryKey, Identity] 
        public int GenreId { get; set; }
        
    }
    
    /// <summary>
    /// <para>Database table 'Subscriptions'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Subscriptions")]
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
    /// <para>Database table 'Users'</para>
    /// <para>This class is automatically generated.</para>
    /// <para>2018-01-08T20:50:59.5285429+02:00</para>
    /// </summary>
    [Table(Name = "Users")]
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

        public IQueryable<ShowPoco> Shows => this.DataConnection.GetTable<ShowPoco>();

        public IQueryable<ShowsgenrePoco> Showsgenres => this.DataConnection.GetTable<ShowsgenrePoco>();

        public IQueryable<SubscriptionPoco> Subscriptions => this.DataConnection.GetTable<SubscriptionPoco>();

        public IQueryable<UserPoco> Users => this.DataConnection.GetTable<UserPoco>();

    }
}
