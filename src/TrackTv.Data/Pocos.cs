namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
	using LinqToDB;
    using LinqToDB.Mapping;

    /// <summary>
    /// <para>Table name: 'actors'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "actors")]
    public class ActorPoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'actor_id'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>Primary key of table: 'actors'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "actor_id", DataType = DataType.Int32)]
        public int ActorID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ActorID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ActorID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ActorID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ActorID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ActorID == default;
        
        /// <summary>
		/// <para>Column name: 'actor_image'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "actor_image", DataType = DataType.NVarChar)]
        public string ActorImage { get; set; }
        
        /// <summary>
		/// <para>Column name: 'actor_name'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "actor_name", DataType = DataType.NVarChar)]
        public string ActorName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'api_change_types'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_change_types")]
    public class ApiChangeTypePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'api_change_type_name'.</para>
		/// <para>Table name: 'api_change_types'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_change_type_name", DataType = DataType.NVarChar)]
        public string ApiChangeTypeName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_type_id'.</para>
		/// <para>Table name: 'api_change_types'.</para>
		/// <para>Primary key of table: 'api_change_types'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "api_change_type_id", DataType = DataType.Int32)]
        public int ApiChangeTypeID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ApiChangeTypeID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ApiChangeTypeID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ApiChangeTypeID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ApiChangeTypeID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ApiChangeTypeID == default;
        
    }
    
    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_changes")]
    public class ApiChangePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'api_change_thetvdbid'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_change_thetvdbid", DataType = DataType.Int32)]
        public int ApiChangeThetvdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_fail_count'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_change_fail_count", DataType = DataType.Int32)]
        public int ApiChangeFailCount { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_created_date'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_change_created_date", DataType = DataType.DateTime2)]
        public DateTime ApiChangeCreatedDate { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_id'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>Primary key of table: 'api_changes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "api_change_id", DataType = DataType.Int32)]
        public int ApiChangeID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ApiChangeID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ApiChangeID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ApiChangeID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ApiChangeID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ApiChangeID == default;
        
        /// <summary>
		/// <para>Column name: 'api_change_last_failed_time'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "api_change_last_failed_time", DataType = DataType.DateTime2)]
        public DateTime? ApiChangeLastFailedTime { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_thetvdb_last_updated'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_change_thetvdb_last_updated", DataType = DataType.DateTime2)]
        public DateTime ApiChangeThetvdbLastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_attached_series_id'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "api_change_attached_series_id", DataType = DataType.Int32)]
        public int? ApiChangeAttachedSeriesID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_change_type'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>Foreign key column [public.api_changes.api_change_type -> public.api_change_types.api_change_type_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_change_type", DataType = DataType.Int32)]
        public int ApiChangeType { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'api_responses'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_responses")]
    public class ApiResponsePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'api_response_episode_thetvdbid'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>Foreign key column [public.api_responses.api_response_episode_thetvdbid -> public.episodes.thetvdbid].</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "api_response_episode_thetvdbid", DataType = DataType.Int32)]
        public int? ApiResponseEpisodeThetvdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_response_show_thetvdbid'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>Foreign key column [public.api_responses.api_response_show_thetvdbid -> public.shows.thetvdbid].</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "api_response_show_thetvdbid", DataType = DataType.Int32)]
        public int? ApiResponseShowThetvdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_response_body'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'jsonb'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.BinaryJson'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_response_body", DataType = DataType.BinaryJson)]
        public string ApiResponseBody { get; set; }
        
        /// <summary>
		/// <para>Column name: 'api_response_id'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>Primary key of table: 'api_responses'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "api_response_id", DataType = DataType.Int32)]
        public int ApiResponseID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ApiResponseID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ApiResponseID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ApiResponseID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ApiResponseID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ApiResponseID == default;
        
        /// <summary>
		/// <para>Column name: 'api_response_last_updated'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "api_response_last_updated", DataType = DataType.DateTime2)]
        public DateTime ApiResponseLastUpdated { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'episodes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "episodes")]
    public class EpisodePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'episode_id'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>Primary key of table: 'episodes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "episode_id", DataType = DataType.Int32)]
        public int EpisodeID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (EpisodeID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.EpisodeID;

		/// <summary>		
		/// <para>Sets the primary key for the table (EpisodeID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.EpisodeID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.EpisodeID == default;
        
        /// <summary>
		/// <para>Column name: 'episode_description'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "episode_description", DataType = DataType.Text)]
        public string EpisodeDescription { get; set; }
        
        /// <summary>
		/// <para>Column name: 'episode_number'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "episode_number", DataType = DataType.Int32)]
        public int EpisodeNumber { get; set; }
        
        /// <summary>
		/// <para>Column name: 'episode_title'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "episode_title", DataType = DataType.NVarChar)]
        public string EpisodeTitle { get; set; }
        
        /// <summary>
		/// <para>Column name: 'first_aired'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "first_aired", DataType = DataType.DateTime2)]
        public DateTime? FirstAired { get; set; }
        
        /// <summary>
		/// <para>Column name: 'imdbid'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "imdbid", DataType = DataType.NVarChar)]
        public string Imdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'season_number'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "season_number", DataType = DataType.Int32)]
        public int SeasonNumber { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>Foreign key column [public.episodes.show_id -> public.shows.show_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "genres")]
    public class GenrePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'genre_id'.</para>
		/// <para>Table name: 'genres'.</para>
		/// <para>Primary key of table: 'genres'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "genre_id", DataType = DataType.Int32)]
        public int GenreID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (GenreID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.GenreID;

		/// <summary>		
		/// <para>Sets the primary key for the table (GenreID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.GenreID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.GenreID == default;
        
        /// <summary>
		/// <para>Column name: 'genre_name'.</para>
		/// <para>Table name: 'genres'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "genre_name", DataType = DataType.NVarChar)]
        public string GenreName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'networks'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "networks")]
    public class NetworkPoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'network_id'.</para>
		/// <para>Table name: 'networks'.</para>
		/// <para>Primary key of table: 'networks'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "network_id", DataType = DataType.Int32)]
        public int NetworkID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (NetworkID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.NetworkID;

		/// <summary>		
		/// <para>Sets the primary key for the table (NetworkID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.NetworkID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.NetworkID == default;
        
        /// <summary>
		/// <para>Column name: 'network_name'.</para>
		/// <para>Table name: 'networks'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "network_name", DataType = DataType.NVarChar)]
        public string NetworkName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'profiles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "profiles")]
    public class ProfilePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>Table name: 'profiles'.</para>
		/// <para>Primary key of table: 'profiles'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ProfileID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ProfileID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ProfileID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ProfileID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ProfileID == default;
        
        /// <summary>
		/// <para>Column name: 'profile_name'.</para>
		/// <para>Table name: 'profiles'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "profile_name", DataType = DataType.NVarChar)]
        public string ProfileName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'roles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "roles")]
    public class RolePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'role_id'.</para>
		/// <para>Table name: 'roles'.</para>
		/// <para>Primary key of table: 'roles'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "role_id", DataType = DataType.Int32)]
        public int RoleID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (RoleID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.RoleID;

		/// <summary>		
		/// <para>Sets the primary key for the table (RoleID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.RoleID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.RoleID == default;
        
        /// <summary>
		/// <para>Column name: 'actor_id'.</para>
		/// <para>Table name: 'roles'.</para>
		/// <para>Foreign key column [public.roles.actor_id -> public.actors.actor_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "actor_id", DataType = DataType.Int32)]
        public int ActorID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'role_name'.</para>
		/// <para>Table name: 'roles'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "role_name", DataType = DataType.NVarChar)]
        public string RoleName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'roles'.</para>
		/// <para>Foreign key column [public.roles.show_id -> public.shows.show_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'settings'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "settings")]
    public class SettingPoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'setting_id'.</para>
		/// <para>Table name: 'settings'.</para>
		/// <para>Primary key of table: 'settings'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "setting_id", DataType = DataType.Int32)]
        public int SettingID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (SettingID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.SettingID;

		/// <summary>		
		/// <para>Sets the primary key for the table (SettingID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.SettingID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.SettingID == default;
        
        /// <summary>
		/// <para>Column name: 'setting_value'.</para>
		/// <para>Table name: 'settings'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "setting_value", DataType = DataType.NVarChar)]
        public string SettingValue { get; set; }
        
        /// <summary>
		/// <para>Column name: 'setting_name'.</para>
		/// <para>Table name: 'settings'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "setting_name", DataType = DataType.NVarChar)]
        public string SettingName { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'shows'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows")]
    public class ShowPoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>Primary key of table: 'shows'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ShowID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ShowID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ShowID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ShowID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ShowID == default;
        
        /// <summary>
		/// <para>Column name: 'air_day'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "air_day", DataType = DataType.Int32)]
        public int? AirDay { get; set; }
        
        /// <summary>
		/// <para>Column name: 'air_time'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "air_time", DataType = DataType.DateTime2)]
        public DateTime? AirTime { get; set; }
        
        /// <summary>
		/// <para>Column name: 'first_aired'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "first_aired", DataType = DataType.DateTime2)]
        public DateTime? FirstAired { get; set; }
        
        /// <summary>
		/// <para>Column name: 'imdbid'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "imdbid", DataType = DataType.NVarChar)]
        public string Imdbid { get; set; }
        
        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime LastUpdated { get; set; }
        
        /// <summary>
		/// <para>Column name: 'network_id'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>Foreign key column [public.shows.network_id -> public.networks.network_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "network_id", DataType = DataType.Int32)]
        public int NetworkID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_banner'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "show_banner", DataType = DataType.NVarChar)]
        public string ShowBanner { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_description'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
        /// </summary>        
		[Nullable]
		[Column(Name = "show_description", DataType = DataType.Text)]
        public string ShowDescription { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_name'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "show_name", DataType = DataType.NVarChar)]
        public string ShowName { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_status'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "show_status", DataType = DataType.Int32)]
        public int ShowStatus { get; set; }
        
        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'shows_genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows_genres")]
    public class ShowGenrePoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'shows_genres_id'.</para>
		/// <para>Table name: 'shows_genres'.</para>
		/// <para>Primary key of table: 'shows_genres'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "shows_genres_id", DataType = DataType.Int32)]
        public int ShowsGenresID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (ShowsGenresID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.ShowsGenresID;

		/// <summary>		
		/// <para>Sets the primary key for the table (ShowsGenresID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.ShowsGenresID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.ShowsGenresID == default;
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'shows_genres'.</para>
		/// <para>Foreign key column [public.shows_genres.show_id -> public.shows.show_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'genre_id'.</para>
		/// <para>Table name: 'shows_genres'.</para>
		/// <para>Foreign key column [public.shows_genres.genre_id -> public.genres.genre_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "genre_id", DataType = DataType.Int32)]
        public int GenreID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "subscriptions")]
    public class SubscriptionPoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'subscription_id'.</para>
		/// <para>Table name: 'subscriptions'.</para>
		/// <para>Primary key of table: 'subscriptions'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "subscription_id", DataType = DataType.Int32)]
        public int SubscriptionID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (SubscriptionID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.SubscriptionID;

		/// <summary>		
		/// <para>Sets the primary key for the table (SubscriptionID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.SubscriptionID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.SubscriptionID == default;
        
        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>Table name: 'subscriptions'.</para>
		/// <para>Foreign key column [public.subscriptions.profile_id -> public.profiles.profile_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }
        
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'subscriptions'.</para>
		/// <para>Foreign key column [public.subscriptions.show_id -> public.shows.show_id].</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }
        
    }
    
    /// <summary>
    /// <para>Table name: 'users'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "users")]
    public class UserPoco : IPoco
    {
        /// <summary>
		/// <para>Column name: 'user_id'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>Primary key of table: 'users'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[PrimaryKey, Identity]
		[Column(Name = "user_id", DataType = DataType.Int32)]
        public int UserID { get; set; }

		/// <summary>		
		/// <para>Returns the primary key for the table (UserID).</para>
        /// </summary>   
		int IPoco.GetPrimaryKey() => this.UserID;

		/// <summary>		
		/// <para>Sets the primary key for the table (UserID).</para>
        /// </summary> 
		void IPoco.SetPrimaryKey(int value) => this.UserID = value;

		/// <summary>		
		/// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
		bool IPoco.IsNew() => this.UserID == default;
        
        /// <summary>
		/// <para>Column name: 'is_admin'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>CLR type: 'bool'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "is_admin", DataType = DataType.Boolean)]
        public bool IsAdmin { get; set; }
        
        /// <summary>
		/// <para>Column name: 'username'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "username", DataType = DataType.NVarChar)]
        public string Username { get; set; }
        
        /// <summary>
		/// <para>Column name: 'password'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "password", DataType = DataType.NVarChar)]
        public string Password { get; set; }
        
        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>        
		[NotNull]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }
        
    }
    
    public partial class DbService
    {
		private static readonly IReadOnlyDictionary<Type, string> PrimaryKeyMap = new Dictionary<Type, string>
		{
			{typeof(ActorPoco), "actor_id"},
			{typeof(ApiChangeTypePoco), "api_change_type_id"},
			{typeof(ApiChangePoco), "api_change_id"},
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
			{typeof(UserPoco), "user_id"},
		};

		private static readonly IReadOnlyDictionary<Type, string> TableNameMap = new Dictionary<Type, string>
		{
			{typeof(ActorPoco), "actors"},
			{typeof(ApiChangeTypePoco), "api_change_types"},
			{typeof(ApiChangePoco), "api_changes"},
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
			{typeof(UserPoco), "users"},
		};

		private static readonly IReadOnlyDictionary<Type, string> TableSchemaMap = new Dictionary<Type, string>
		{
			{typeof(ActorPoco), "public"},
			{typeof(ApiChangeTypePoco), "public"},
			{typeof(ApiChangePoco), "public"},
			{typeof(ApiResponsePoco), "public"},
			{typeof(EpisodePoco), "public"},
			{typeof(GenrePoco), "public"},
			{typeof(NetworkPoco), "public"},
			{typeof(ProfilePoco), "public"},
			{typeof(RolePoco), "public"},
			{typeof(SettingPoco), "public"},
			{typeof(ShowPoco), "public"},
			{typeof(ShowGenrePoco), "public"},
			{typeof(SubscriptionPoco), "public"},
			{typeof(UserPoco), "public"},
		};

		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        public IQueryable<ActorPoco> Actors => this.DataConnection.GetTable<ActorPoco>();
		
		/// <summary>
		/// <para>Database table 'api_change_types'.</para>		
		/// </summary>
        public IQueryable<ApiChangeTypePoco> ApiChangeTypes => this.DataConnection.GetTable<ApiChangeTypePoco>();
		
		/// <summary>
		/// <para>Database table 'api_changes'.</para>		
		/// </summary>
        public IQueryable<ApiChangePoco> ApiChanges => this.DataConnection.GetTable<ApiChangePoco>();
		
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
		/// <para>Database table 'users'.</para>		
		/// </summary>
        public IQueryable<UserPoco> Users => this.DataConnection.GetTable<UserPoco>();
		
    }

	public partial interface IDbService
    {
		/// <summary>
		/// <para>Database table 'actors'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ActorPoco> Actors { get; }

		/// <summary>
		/// <para>Database table 'api_change_types'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ApiChangeTypePoco> ApiChangeTypes { get; }

		/// <summary>
		/// <para>Database table 'api_changes'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ApiChangePoco> ApiChanges { get; }

		/// <summary>
		/// <para>Database table 'api_responses'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ApiResponsePoco> ApiResponses { get; }

		/// <summary>
		/// <para>Database table 'episodes'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<EpisodePoco> Episodes { get; }

		/// <summary>
		/// <para>Database table 'genres'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<GenrePoco> Genres { get; }

		/// <summary>
		/// <para>Database table 'networks'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<NetworkPoco> Networks { get; }

		/// <summary>
		/// <para>Database table 'profiles'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ProfilePoco> Profiles { get; }

		/// <summary>
		/// <para>Database table 'roles'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<RolePoco> Roles { get; }

		/// <summary>
		/// <para>Database table 'settings'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<SettingPoco> Settings { get; }

		/// <summary>
		/// <para>Database table 'shows'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ShowPoco> Shows { get; }

		/// <summary>
		/// <para>Database table 'shows_genres'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ShowGenrePoco> ShowsGenres { get; }

		/// <summary>
		/// <para>Database table 'subscriptions'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<SubscriptionPoco> Subscriptions { get; }

		/// <summary>
		/// <para>Database table 'users'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<UserPoco> Users { get; }

    }
}
