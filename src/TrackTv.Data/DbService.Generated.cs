namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
	using LinqToDB;
    using LinqToDB.Mapping;

	using NpgsqlTypes;

    /// <summary>
    /// <para>Table name: 'actors'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "actors")]
    public class ActorPoco : IPoco<ActorPoco>
    {
        /// <summary>
		/// <para>Column name: 'actor_id'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>Primary key of table: 'actors'.</para>
		/// <para>Primary key constraint name: 'actors_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "actor_id", DataType = DataType.Int32)]
        public int ActorID { get; set; }

        /// <summary>
		/// <para>Column name: 'actor_image'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
		[Nullable]
		[Column(Name = "actor_name", DataType = DataType.NVarChar)]
        public string ActorName { get; set; }

        /// <summary>
		/// <para>Column name: 'last_updated'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>
		[Nullable]
		[Column(Name = "last_updated", DataType = DataType.DateTime2)]
        public DateTime? LastUpdated { get; set; }

        /// <summary>
		/// <para>Column name: 'thetvdbid'.</para>
		/// <para>Table name: 'actors'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }

		TableMetadataModel<ActorPoco> IPoco<ActorPoco>.Metadata => DbService.ActorPocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'api_change_types'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_change_types")]
    public class ApiChangeTypePoco : IPoco<ApiChangeTypePoco>
    {
        /// <summary>
		/// <para>Column name: 'api_change_type_name'.</para>
		/// <para>Table name: 'api_change_types'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>Primary key constraint name: 'api_change_types_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "api_change_type_id", DataType = DataType.Int32)]
        public int ApiChangeTypeID { get; set; }

		TableMetadataModel<ApiChangeTypePoco> IPoco<ApiChangeTypePoco>.Metadata => DbService.ApiChangeTypePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_changes")]
    public class ApiChangePoco : IPoco<ApiChangePoco>
    {
        /// <summary>
		/// <para>Column name: 'api_change_thetvdbid'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>Primary key constraint name: 'api_changes_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "api_change_id", DataType = DataType.Int32)]
        public int ApiChangeID { get; set; }

        /// <summary>
		/// <para>Column name: 'api_change_last_failed_time'.</para>
		/// <para>Table name: 'api_changes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>Foreign key constraint name: 'fk_api_changes_api_change_type'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "api_change_type", DataType = DataType.Int32)]
        public int ApiChangeType { get; set; }

		TableMetadataModel<ApiChangePoco> IPoco<ApiChangePoco>.Metadata => DbService.ApiChangePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'api_responses'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "api_responses")]
    public class ApiResponsePoco : IPoco<ApiResponsePoco>
    {
        /// <summary>
		/// <para>Column name: 'api_response_episode_thetvdbid'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>Foreign key column [public.api_responses.api_response_episode_thetvdbid -> public.episodes.thetvdbid].</para>
		/// <para>Foreign key constraint name: 'fk_api_responses_episodes_thetvdbid'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>Foreign key constraint name: 'fk_api_responses_shows_thetvdbid'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Jsonb'.</para>
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
		/// <para>Primary key constraint name: 'api_responses_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "api_response_id", DataType = DataType.Int32)]
        public int ApiResponseID { get; set; }

        /// <summary>
		/// <para>Column name: 'api_response_last_updated'.</para>
		/// <para>Table name: 'api_responses'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "api_response_last_updated", DataType = DataType.DateTime2)]
        public DateTime ApiResponseLastUpdated { get; set; }

		TableMetadataModel<ApiResponsePoco> IPoco<ApiResponsePoco>.Metadata => DbService.ApiResponsePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'episodes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "episodes")]
    public class EpisodePoco : IPoco<EpisodePoco>
    {
        /// <summary>
		/// <para>Column name: 'episode_id'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>Primary key of table: 'episodes'.</para>
		/// <para>Primary key constraint name: 'episodes_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "episode_id", DataType = DataType.Int32)]
        public int EpisodeID { get; set; }

        /// <summary>
		/// <para>Column name: 'episode_description'.</para>
		/// <para>Table name: 'episodes'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>Foreign key constraint name: 'fk_episodes_show_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }

		TableMetadataModel<EpisodePoco> IPoco<EpisodePoco>.Metadata => DbService.EpisodePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "genres")]
    public class GenrePoco : IPoco<GenrePoco>
    {
        /// <summary>
		/// <para>Column name: 'genre_id'.</para>
		/// <para>Table name: 'genres'.</para>
		/// <para>Primary key of table: 'genres'.</para>
		/// <para>Primary key constraint name: 'genres_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "genre_id", DataType = DataType.Int32)]
        public int GenreID { get; set; }

        /// <summary>
		/// <para>Column name: 'genre_name'.</para>
		/// <para>Table name: 'genres'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "genre_name", DataType = DataType.NVarChar)]
        public string GenreName { get; set; }

		TableMetadataModel<GenrePoco> IPoco<GenrePoco>.Metadata => DbService.GenrePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'networks'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "networks")]
    public class NetworkPoco : IPoco<NetworkPoco>
    {
        /// <summary>
		/// <para>Column name: 'network_id'.</para>
		/// <para>Table name: 'networks'.</para>
		/// <para>Primary key of table: 'networks'.</para>
		/// <para>Primary key constraint name: 'networks_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "network_id", DataType = DataType.Int32)]
        public int NetworkID { get; set; }

        /// <summary>
		/// <para>Column name: 'network_name'.</para>
		/// <para>Table name: 'networks'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "network_name", DataType = DataType.NVarChar)]
        public string NetworkName { get; set; }

		TableMetadataModel<NetworkPoco> IPoco<NetworkPoco>.Metadata => DbService.NetworkPocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'profiles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "profiles")]
    public class ProfilePoco : IPoco<ProfilePoco>
    {
        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>Table name: 'profiles'.</para>
		/// <para>Primary key of table: 'profiles'.</para>
		/// <para>Primary key constraint name: 'profiles_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }

        /// <summary>
		/// <para>Column name: 'profile_name'.</para>
		/// <para>Table name: 'profiles'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "profile_name", DataType = DataType.NVarChar)]
        public string ProfileName { get; set; }

		TableMetadataModel<ProfilePoco> IPoco<ProfilePoco>.Metadata => DbService.ProfilePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'roles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "roles")]
    public class RolePoco : IPoco<RolePoco>
    {
        /// <summary>
		/// <para>Column name: 'role_id'.</para>
		/// <para>Table name: 'roles'.</para>
		/// <para>Primary key of table: 'roles'.</para>
		/// <para>Primary key constraint name: 'roles_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "role_id", DataType = DataType.Int32)]
        public int RoleID { get; set; }

        /// <summary>
		/// <para>Column name: 'actor_id'.</para>
		/// <para>Table name: 'roles'.</para>
		/// <para>Foreign key column [public.roles.actor_id -> public.actors.actor_id].</para>
		/// <para>Foreign key constraint name: 'fk_roles_actor_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>Foreign key constraint name: 'fk_roles_show_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }

		TableMetadataModel<RolePoco> IPoco<RolePoco>.Metadata => DbService.RolePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'settings'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "settings")]
    public class SettingPoco : IPoco<SettingPoco>
    {
        /// <summary>
		/// <para>Column name: 'setting_id'.</para>
		/// <para>Table name: 'settings'.</para>
		/// <para>Primary key of table: 'settings'.</para>
		/// <para>Primary key constraint name: 'settings_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "setting_id", DataType = DataType.Int32)]
        public int SettingID { get; set; }

        /// <summary>
		/// <para>Column name: 'setting_value'.</para>
		/// <para>Table name: 'settings'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "setting_name", DataType = DataType.NVarChar)]
        public string SettingName { get; set; }

		TableMetadataModel<SettingPoco> IPoco<SettingPoco>.Metadata => DbService.SettingPocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'shows'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows")]
    public class ShowPoco : IPoco<ShowPoco>
    {
        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>Primary key of table: 'shows'.</para>
		/// <para>Primary key constraint name: 'shows_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }

        /// <summary>
		/// <para>Column name: 'air_day'.</para>
		/// <para>Table name: 'shows'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
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
		/// <para>Foreign key constraint name: 'fk_shows_network_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "thetvdbid", DataType = DataType.Int32)]
        public int Thetvdbid { get; set; }

		TableMetadataModel<ShowPoco> IPoco<ShowPoco>.Metadata => DbService.ShowPocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'shows_genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "shows_genres")]
    public class ShowGenrePoco : IPoco<ShowGenrePoco>
    {
        /// <summary>
		/// <para>Column name: 'shows_genres_id'.</para>
		/// <para>Table name: 'shows_genres'.</para>
		/// <para>Primary key of table: 'shows_genres'.</para>
		/// <para>Primary key constraint name: 'shows_genres_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "shows_genres_id", DataType = DataType.Int32)]
        public int ShowsGenresID { get; set; }

        /// <summary>
		/// <para>Column name: 'show_id'.</para>
		/// <para>Table name: 'shows_genres'.</para>
		/// <para>Foreign key column [public.shows_genres.show_id -> public.shows.show_id].</para>
		/// <para>Foreign key constraint name: 'fk_shows_genres_show_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>Foreign key constraint name: 'fk_shows_genres_genre_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "genre_id", DataType = DataType.Int32)]
        public int GenreID { get; set; }

		TableMetadataModel<ShowGenrePoco> IPoco<ShowGenrePoco>.Metadata => DbService.ShowGenrePocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "subscriptions")]
    public class SubscriptionPoco : IPoco<SubscriptionPoco>
    {
        /// <summary>
		/// <para>Column name: 'subscription_id'.</para>
		/// <para>Table name: 'subscriptions'.</para>
		/// <para>Primary key of table: 'subscriptions'.</para>
		/// <para>Primary key constraint name: 'subscriptions_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "subscription_id", DataType = DataType.Int32)]
        public int SubscriptionID { get; set; }

        /// <summary>
		/// <para>Column name: 'profile_id'.</para>
		/// <para>Table name: 'subscriptions'.</para>
		/// <para>Foreign key column [public.subscriptions.profile_id -> public.profiles.profile_id].</para>
		/// <para>Foreign key constraint name: 'fk_subscriptions_profile_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
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
		/// <para>Foreign key constraint name: 'fk_subscriptions_show_id'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "show_id", DataType = DataType.Int32)]
        public int ShowID { get; set; }

		TableMetadataModel<SubscriptionPoco> IPoco<SubscriptionPoco>.Metadata => DbService.SubscriptionPocoMetadata;
    }
    
    /// <summary>
    /// <para>Table name: 'users'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    [Table(Schema="public", Name = "users")]
    public class UserPoco : IPoco<UserPoco>
    {
        /// <summary>
		/// <para>Column name: 'user_id'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>Primary key of table: 'users'.</para>
		/// <para>Primary key constraint name: 'users_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "user_id", DataType = DataType.Int32)]
        public int UserID { get; set; }

        /// <summary>
		/// <para>Column name: 'is_admin'.</para>
		/// <para>Table name: 'users'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
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
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
        /// </summary>
		[NotNull]
		[Column(Name = "profile_id", DataType = DataType.Int32)]
        public int ProfileID { get; set; }

		TableMetadataModel<UserPoco> IPoco<UserPoco>.Metadata => DbService.UserPocoMetadata;
    }
    
    public partial class DbService
    {
        internal static readonly TableMetadataModel<ActorPoco> ActorPocoMetadata = new TableMetadataModel<ActorPoco>
		{
			ClassName = "Actor",
			PluralClassName = "Actors",
			PrimaryKeyColumnName = "actor_id",
			PrimaryKeyPropertyName = "ActorID",
			TableName = "actors",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ActorID,
			SetPrimaryKey = (instance, val) => instance.ActorID = val,
			IsNew = (instance) => instance.ActorID == default,
			Clone = (instance) => new ActorPoco
			{
				ActorID = instance.ActorID,
				ActorImage = instance.ActorImage,
				ActorName = instance.ActorName,
				LastUpdated = instance.LastUpdated,
				Thetvdbid = instance.Thetvdbid,
			},
			Setters = new Dictionary<string, Action<ActorPoco, object>>
			{
				{"actor_id", (instance, val) => instance.ActorID = (int)val },
				{"actor_image", (instance, val) => instance.ActorImage = (string)val },
				{"actor_name", (instance, val) => instance.ActorName = (string)val },
				{"last_updated", (instance, val) => instance.LastUpdated = (DateTime?)val },
				{"thetvdbid", (instance, val) => instance.Thetvdbid = (int)val },
			},
			Getters = new Dictionary<string, Func<ActorPoco, object>>
			{
				{"actor_id", (instance) => instance.ActorID },
				{"actor_image", (instance) => instance.ActorImage },
				{"actor_name", (instance) => instance.ActorName },
				{"last_updated", (instance) => instance.LastUpdated },
				{"thetvdbid", (instance) => instance.Thetvdbid },
			},
			Columns = new List<ColumnMetadataModel<ActorPoco>>
			{
				new ColumnMetadataModel<ActorPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "actor_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "actors_pkey" == string.Empty ? null : "actors_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ActorID",
					TableName = "actors",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ActorID = (int)val,
					GetValue = (instance) => instance.ActorID,
				},
				new ColumnMetadataModel<ActorPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "actor_image",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ActorImage",
					TableName = "actors",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ActorImage = (string)val,
					GetValue = (instance) => instance.ActorImage,
				},
				new ColumnMetadataModel<ActorPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "actor_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ActorName",
					TableName = "actors",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ActorName = (string)val,
					GetValue = (instance) => instance.ActorName,
				},
				new ColumnMetadataModel<ActorPoco>
				{						
					ClrTypeName = "DateTime?",
					ClrType = typeof(DateTime?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "last_updated",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "LastUpdated",
					TableName = "actors",
					TableSchema = "public",
					SetValue = (instance, val) => instance.LastUpdated = (DateTime?)val,
					GetValue = (instance) => instance.LastUpdated,
				},
				new ColumnMetadataModel<ActorPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "thetvdbid",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "Thetvdbid",
					TableName = "actors",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Thetvdbid = (int)val,
					GetValue = (instance) => instance.Thetvdbid,
				},
			}
		};
		
        internal static readonly TableMetadataModel<ApiChangeTypePoco> ApiChangeTypePocoMetadata = new TableMetadataModel<ApiChangeTypePoco>
		{
			ClassName = "ApiChangeType",
			PluralClassName = "ApiChangeTypes",
			PrimaryKeyColumnName = "api_change_type_id",
			PrimaryKeyPropertyName = "ApiChangeTypeID",
			TableName = "api_change_types",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ApiChangeTypeID,
			SetPrimaryKey = (instance, val) => instance.ApiChangeTypeID = val,
			IsNew = (instance) => instance.ApiChangeTypeID == default,
			Clone = (instance) => new ApiChangeTypePoco
			{
				ApiChangeTypeName = instance.ApiChangeTypeName,
				ApiChangeTypeID = instance.ApiChangeTypeID,
			},
			Setters = new Dictionary<string, Action<ApiChangeTypePoco, object>>
			{
				{"api_change_type_name", (instance, val) => instance.ApiChangeTypeName = (string)val },
				{"api_change_type_id", (instance, val) => instance.ApiChangeTypeID = (int)val },
			},
			Getters = new Dictionary<string, Func<ApiChangeTypePoco, object>>
			{
				{"api_change_type_name", (instance) => instance.ApiChangeTypeName },
				{"api_change_type_id", (instance) => instance.ApiChangeTypeID },
			},
			Columns = new List<ColumnMetadataModel<ApiChangeTypePoco>>
			{
				new ColumnMetadataModel<ApiChangeTypePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_type_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ApiChangeTypeName",
					TableName = "api_change_types",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeTypeName = (string)val,
					GetValue = (instance) => instance.ApiChangeTypeName,
				},
				new ColumnMetadataModel<ApiChangeTypePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_type_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "api_change_types_pkey" == string.Empty ? null : "api_change_types_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeTypeID",
					TableName = "api_change_types",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeTypeID = (int)val,
					GetValue = (instance) => instance.ApiChangeTypeID,
				},
			}
		};
		
        internal static readonly TableMetadataModel<ApiChangePoco> ApiChangePocoMetadata = new TableMetadataModel<ApiChangePoco>
		{
			ClassName = "ApiChange",
			PluralClassName = "ApiChanges",
			PrimaryKeyColumnName = "api_change_id",
			PrimaryKeyPropertyName = "ApiChangeID",
			TableName = "api_changes",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ApiChangeID,
			SetPrimaryKey = (instance, val) => instance.ApiChangeID = val,
			IsNew = (instance) => instance.ApiChangeID == default,
			Clone = (instance) => new ApiChangePoco
			{
				ApiChangeThetvdbid = instance.ApiChangeThetvdbid,
				ApiChangeFailCount = instance.ApiChangeFailCount,
				ApiChangeCreatedDate = instance.ApiChangeCreatedDate,
				ApiChangeID = instance.ApiChangeID,
				ApiChangeLastFailedTime = instance.ApiChangeLastFailedTime,
				ApiChangeThetvdbLastUpdated = instance.ApiChangeThetvdbLastUpdated,
				ApiChangeAttachedSeriesID = instance.ApiChangeAttachedSeriesID,
				ApiChangeType = instance.ApiChangeType,
			},
			Setters = new Dictionary<string, Action<ApiChangePoco, object>>
			{
				{"api_change_thetvdbid", (instance, val) => instance.ApiChangeThetvdbid = (int)val },
				{"api_change_fail_count", (instance, val) => instance.ApiChangeFailCount = (int)val },
				{"api_change_created_date", (instance, val) => instance.ApiChangeCreatedDate = (DateTime)val },
				{"api_change_id", (instance, val) => instance.ApiChangeID = (int)val },
				{"api_change_last_failed_time", (instance, val) => instance.ApiChangeLastFailedTime = (DateTime?)val },
				{"api_change_thetvdb_last_updated", (instance, val) => instance.ApiChangeThetvdbLastUpdated = (DateTime)val },
				{"api_change_attached_series_id", (instance, val) => instance.ApiChangeAttachedSeriesID = (int?)val },
				{"api_change_type", (instance, val) => instance.ApiChangeType = (int)val },
			},
			Getters = new Dictionary<string, Func<ApiChangePoco, object>>
			{
				{"api_change_thetvdbid", (instance) => instance.ApiChangeThetvdbid },
				{"api_change_fail_count", (instance) => instance.ApiChangeFailCount },
				{"api_change_created_date", (instance) => instance.ApiChangeCreatedDate },
				{"api_change_id", (instance) => instance.ApiChangeID },
				{"api_change_last_failed_time", (instance) => instance.ApiChangeLastFailedTime },
				{"api_change_thetvdb_last_updated", (instance) => instance.ApiChangeThetvdbLastUpdated },
				{"api_change_attached_series_id", (instance) => instance.ApiChangeAttachedSeriesID },
				{"api_change_type", (instance) => instance.ApiChangeType },
			},
			Columns = new List<ColumnMetadataModel<ApiChangePoco>>
			{
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_thetvdbid",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeThetvdbid",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeThetvdbid = (int)val,
					GetValue = (instance) => instance.ApiChangeThetvdbid,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_fail_count",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeFailCount",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeFailCount = (int)val,
					GetValue = (instance) => instance.ApiChangeFailCount,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "DateTime",
					ClrType = typeof(DateTime),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_created_date",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiChangeCreatedDate",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeCreatedDate = (DateTime)val,
					GetValue = (instance) => instance.ApiChangeCreatedDate,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "api_changes_pkey" == string.Empty ? null : "api_changes_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeID",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeID = (int)val,
					GetValue = (instance) => instance.ApiChangeID,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "DateTime?",
					ClrType = typeof(DateTime?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_last_failed_time",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiChangeLastFailedTime",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeLastFailedTime = (DateTime?)val,
					GetValue = (instance) => instance.ApiChangeLastFailedTime,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "DateTime",
					ClrType = typeof(DateTime),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_thetvdb_last_updated",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiChangeThetvdbLastUpdated",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeThetvdbLastUpdated = (DateTime)val,
					GetValue = (instance) => instance.ApiChangeThetvdbLastUpdated,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "int?",
					ClrType = typeof(int?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_attached_series_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeAttachedSeriesID",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeAttachedSeriesID = (int?)val,
					GetValue = (instance) => instance.ApiChangeAttachedSeriesID,
				},
				new ColumnMetadataModel<ApiChangePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_change_type",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_api_changes_api_change_type" == string.Empty ? null : "fk_api_changes_api_change_type",						
					ForeignKeyReferenceColumnName = "api_change_type_id" == string.Empty ? null : "api_change_type_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "api_change_types" == string.Empty ? null : "api_change_types",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeType",
					TableName = "api_changes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiChangeType = (int)val,
					GetValue = (instance) => instance.ApiChangeType,
				},
			}
		};
		
        internal static readonly TableMetadataModel<ApiResponsePoco> ApiResponsePocoMetadata = new TableMetadataModel<ApiResponsePoco>
		{
			ClassName = "ApiResponse",
			PluralClassName = "ApiResponses",
			PrimaryKeyColumnName = "api_response_id",
			PrimaryKeyPropertyName = "ApiResponseID",
			TableName = "api_responses",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ApiResponseID,
			SetPrimaryKey = (instance, val) => instance.ApiResponseID = val,
			IsNew = (instance) => instance.ApiResponseID == default,
			Clone = (instance) => new ApiResponsePoco
			{
				ApiResponseEpisodeThetvdbid = instance.ApiResponseEpisodeThetvdbid,
				ApiResponseShowThetvdbid = instance.ApiResponseShowThetvdbid,
				ApiResponseBody = instance.ApiResponseBody,
				ApiResponseID = instance.ApiResponseID,
				ApiResponseLastUpdated = instance.ApiResponseLastUpdated,
			},
			Setters = new Dictionary<string, Action<ApiResponsePoco, object>>
			{
				{"api_response_episode_thetvdbid", (instance, val) => instance.ApiResponseEpisodeThetvdbid = (int?)val },
				{"api_response_show_thetvdbid", (instance, val) => instance.ApiResponseShowThetvdbid = (int?)val },
				{"api_response_body", (instance, val) => instance.ApiResponseBody = (string)val },
				{"api_response_id", (instance, val) => instance.ApiResponseID = (int)val },
				{"api_response_last_updated", (instance, val) => instance.ApiResponseLastUpdated = (DateTime)val },
			},
			Getters = new Dictionary<string, Func<ApiResponsePoco, object>>
			{
				{"api_response_episode_thetvdbid", (instance) => instance.ApiResponseEpisodeThetvdbid },
				{"api_response_show_thetvdbid", (instance) => instance.ApiResponseShowThetvdbid },
				{"api_response_body", (instance) => instance.ApiResponseBody },
				{"api_response_id", (instance) => instance.ApiResponseID },
				{"api_response_last_updated", (instance) => instance.ApiResponseLastUpdated },
			},
			Columns = new List<ColumnMetadataModel<ApiResponsePoco>>
			{
				new ColumnMetadataModel<ApiResponsePoco>
				{						
					ClrTypeName = "int?",
					ClrType = typeof(int?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_response_episode_thetvdbid",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_api_responses_episodes_thetvdbid" == string.Empty ? null : "fk_api_responses_episodes_thetvdbid",						
					ForeignKeyReferenceColumnName = "thetvdbid" == string.Empty ? null : "thetvdbid",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "episodes" == string.Empty ? null : "episodes",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiResponseEpisodeThetvdbid",
					TableName = "api_responses",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiResponseEpisodeThetvdbid = (int?)val,
					GetValue = (instance) => instance.ApiResponseEpisodeThetvdbid,
				},
				new ColumnMetadataModel<ApiResponsePoco>
				{						
					ClrTypeName = "int?",
					ClrType = typeof(int?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_response_show_thetvdbid",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_api_responses_shows_thetvdbid" == string.Empty ? null : "fk_api_responses_shows_thetvdbid",						
					ForeignKeyReferenceColumnName = "thetvdbid" == string.Empty ? null : "thetvdbid",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "shows" == string.Empty ? null : "shows",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiResponseShowThetvdbid",
					TableName = "api_responses",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiResponseShowThetvdbid = (int?)val,
					GetValue = (instance) => instance.ApiResponseShowThetvdbid,
				},
				new ColumnMetadataModel<ApiResponsePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_response_body",
					DbDataType = "jsonb",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.BinaryJson",
					Linq2dbDataType = DataType.BinaryJson,
					NpgsDataTypeName = "NpgsqlDbType.Jsonb",
					NpgsDataType = NpgsqlDbType.Jsonb,
					PropertyName = "ApiResponseBody",
					TableName = "api_responses",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiResponseBody = (string)val,
					GetValue = (instance) => instance.ApiResponseBody,
				},
				new ColumnMetadataModel<ApiResponsePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_response_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "api_responses_pkey" == string.Empty ? null : "api_responses_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiResponseID",
					TableName = "api_responses",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiResponseID = (int)val,
					GetValue = (instance) => instance.ApiResponseID,
				},
				new ColumnMetadataModel<ApiResponsePoco>
				{						
					ClrTypeName = "DateTime",
					ClrType = typeof(DateTime),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "api_response_last_updated",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiResponseLastUpdated",
					TableName = "api_responses",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ApiResponseLastUpdated = (DateTime)val,
					GetValue = (instance) => instance.ApiResponseLastUpdated,
				},
			}
		};
		
        internal static readonly TableMetadataModel<EpisodePoco> EpisodePocoMetadata = new TableMetadataModel<EpisodePoco>
		{
			ClassName = "Episode",
			PluralClassName = "Episodes",
			PrimaryKeyColumnName = "episode_id",
			PrimaryKeyPropertyName = "EpisodeID",
			TableName = "episodes",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.EpisodeID,
			SetPrimaryKey = (instance, val) => instance.EpisodeID = val,
			IsNew = (instance) => instance.EpisodeID == default,
			Clone = (instance) => new EpisodePoco
			{
				EpisodeID = instance.EpisodeID,
				EpisodeDescription = instance.EpisodeDescription,
				EpisodeNumber = instance.EpisodeNumber,
				EpisodeTitle = instance.EpisodeTitle,
				FirstAired = instance.FirstAired,
				Imdbid = instance.Imdbid,
				LastUpdated = instance.LastUpdated,
				SeasonNumber = instance.SeasonNumber,
				ShowID = instance.ShowID,
				Thetvdbid = instance.Thetvdbid,
			},
			Setters = new Dictionary<string, Action<EpisodePoco, object>>
			{
				{"episode_id", (instance, val) => instance.EpisodeID = (int)val },
				{"episode_description", (instance, val) => instance.EpisodeDescription = (string)val },
				{"episode_number", (instance, val) => instance.EpisodeNumber = (int)val },
				{"episode_title", (instance, val) => instance.EpisodeTitle = (string)val },
				{"first_aired", (instance, val) => instance.FirstAired = (DateTime?)val },
				{"imdbid", (instance, val) => instance.Imdbid = (string)val },
				{"last_updated", (instance, val) => instance.LastUpdated = (DateTime)val },
				{"season_number", (instance, val) => instance.SeasonNumber = (int)val },
				{"show_id", (instance, val) => instance.ShowID = (int)val },
				{"thetvdbid", (instance, val) => instance.Thetvdbid = (int)val },
			},
			Getters = new Dictionary<string, Func<EpisodePoco, object>>
			{
				{"episode_id", (instance) => instance.EpisodeID },
				{"episode_description", (instance) => instance.EpisodeDescription },
				{"episode_number", (instance) => instance.EpisodeNumber },
				{"episode_title", (instance) => instance.EpisodeTitle },
				{"first_aired", (instance) => instance.FirstAired },
				{"imdbid", (instance) => instance.Imdbid },
				{"last_updated", (instance) => instance.LastUpdated },
				{"season_number", (instance) => instance.SeasonNumber },
				{"show_id", (instance) => instance.ShowID },
				{"thetvdbid", (instance) => instance.Thetvdbid },
			},
			Columns = new List<ColumnMetadataModel<EpisodePoco>>
			{
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "episode_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "episodes_pkey" == string.Empty ? null : "episodes_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "EpisodeID",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.EpisodeID = (int)val,
					GetValue = (instance) => instance.EpisodeID,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "episode_description",
					DbDataType = "text",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Text",
					Linq2dbDataType = DataType.Text,
					NpgsDataTypeName = "NpgsqlDbType.Text",
					NpgsDataType = NpgsqlDbType.Text,
					PropertyName = "EpisodeDescription",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.EpisodeDescription = (string)val,
					GetValue = (instance) => instance.EpisodeDescription,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "episode_number",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "EpisodeNumber",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.EpisodeNumber = (int)val,
					GetValue = (instance) => instance.EpisodeNumber,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "episode_title",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "EpisodeTitle",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.EpisodeTitle = (string)val,
					GetValue = (instance) => instance.EpisodeTitle,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "DateTime?",
					ClrType = typeof(DateTime?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "first_aired",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "FirstAired",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.FirstAired = (DateTime?)val,
					GetValue = (instance) => instance.FirstAired,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "imdbid",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Imdbid",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Imdbid = (string)val,
					GetValue = (instance) => instance.Imdbid,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "DateTime",
					ClrType = typeof(DateTime),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "last_updated",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "LastUpdated",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.LastUpdated = (DateTime)val,
					GetValue = (instance) => instance.LastUpdated,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "season_number",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "SeasonNumber",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.SeasonNumber = (int)val,
					GetValue = (instance) => instance.SeasonNumber,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_episodes_show_id" == string.Empty ? null : "fk_episodes_show_id",						
					ForeignKeyReferenceColumnName = "show_id" == string.Empty ? null : "show_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "shows" == string.Empty ? null : "shows",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowID = (int)val,
					GetValue = (instance) => instance.ShowID,
				},
				new ColumnMetadataModel<EpisodePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "thetvdbid",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "Thetvdbid",
					TableName = "episodes",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Thetvdbid = (int)val,
					GetValue = (instance) => instance.Thetvdbid,
				},
			}
		};
		
        internal static readonly TableMetadataModel<GenrePoco> GenrePocoMetadata = new TableMetadataModel<GenrePoco>
		{
			ClassName = "Genre",
			PluralClassName = "Genres",
			PrimaryKeyColumnName = "genre_id",
			PrimaryKeyPropertyName = "GenreID",
			TableName = "genres",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.GenreID,
			SetPrimaryKey = (instance, val) => instance.GenreID = val,
			IsNew = (instance) => instance.GenreID == default,
			Clone = (instance) => new GenrePoco
			{
				GenreID = instance.GenreID,
				GenreName = instance.GenreName,
			},
			Setters = new Dictionary<string, Action<GenrePoco, object>>
			{
				{"genre_id", (instance, val) => instance.GenreID = (int)val },
				{"genre_name", (instance, val) => instance.GenreName = (string)val },
			},
			Getters = new Dictionary<string, Func<GenrePoco, object>>
			{
				{"genre_id", (instance) => instance.GenreID },
				{"genre_name", (instance) => instance.GenreName },
			},
			Columns = new List<ColumnMetadataModel<GenrePoco>>
			{
				new ColumnMetadataModel<GenrePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "genre_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "genres_pkey" == string.Empty ? null : "genres_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "GenreID",
					TableName = "genres",
					TableSchema = "public",
					SetValue = (instance, val) => instance.GenreID = (int)val,
					GetValue = (instance) => instance.GenreID,
				},
				new ColumnMetadataModel<GenrePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "genre_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "GenreName",
					TableName = "genres",
					TableSchema = "public",
					SetValue = (instance, val) => instance.GenreName = (string)val,
					GetValue = (instance) => instance.GenreName,
				},
			}
		};
		
        internal static readonly TableMetadataModel<NetworkPoco> NetworkPocoMetadata = new TableMetadataModel<NetworkPoco>
		{
			ClassName = "Network",
			PluralClassName = "Networks",
			PrimaryKeyColumnName = "network_id",
			PrimaryKeyPropertyName = "NetworkID",
			TableName = "networks",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.NetworkID,
			SetPrimaryKey = (instance, val) => instance.NetworkID = val,
			IsNew = (instance) => instance.NetworkID == default,
			Clone = (instance) => new NetworkPoco
			{
				NetworkID = instance.NetworkID,
				NetworkName = instance.NetworkName,
			},
			Setters = new Dictionary<string, Action<NetworkPoco, object>>
			{
				{"network_id", (instance, val) => instance.NetworkID = (int)val },
				{"network_name", (instance, val) => instance.NetworkName = (string)val },
			},
			Getters = new Dictionary<string, Func<NetworkPoco, object>>
			{
				{"network_id", (instance) => instance.NetworkID },
				{"network_name", (instance) => instance.NetworkName },
			},
			Columns = new List<ColumnMetadataModel<NetworkPoco>>
			{
				new ColumnMetadataModel<NetworkPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "network_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "networks_pkey" == string.Empty ? null : "networks_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "NetworkID",
					TableName = "networks",
					TableSchema = "public",
					SetValue = (instance, val) => instance.NetworkID = (int)val,
					GetValue = (instance) => instance.NetworkID,
				},
				new ColumnMetadataModel<NetworkPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "network_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "NetworkName",
					TableName = "networks",
					TableSchema = "public",
					SetValue = (instance, val) => instance.NetworkName = (string)val,
					GetValue = (instance) => instance.NetworkName,
				},
			}
		};
		
        internal static readonly TableMetadataModel<ProfilePoco> ProfilePocoMetadata = new TableMetadataModel<ProfilePoco>
		{
			ClassName = "Profile",
			PluralClassName = "Profiles",
			PrimaryKeyColumnName = "profile_id",
			PrimaryKeyPropertyName = "ProfileID",
			TableName = "profiles",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ProfileID,
			SetPrimaryKey = (instance, val) => instance.ProfileID = val,
			IsNew = (instance) => instance.ProfileID == default,
			Clone = (instance) => new ProfilePoco
			{
				ProfileID = instance.ProfileID,
				ProfileName = instance.ProfileName,
			},
			Setters = new Dictionary<string, Action<ProfilePoco, object>>
			{
				{"profile_id", (instance, val) => instance.ProfileID = (int)val },
				{"profile_name", (instance, val) => instance.ProfileName = (string)val },
			},
			Getters = new Dictionary<string, Func<ProfilePoco, object>>
			{
				{"profile_id", (instance) => instance.ProfileID },
				{"profile_name", (instance) => instance.ProfileName },
			},
			Columns = new List<ColumnMetadataModel<ProfilePoco>>
			{
				new ColumnMetadataModel<ProfilePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "profile_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "profiles_pkey" == string.Empty ? null : "profiles_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ProfileID",
					TableName = "profiles",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ProfileID = (int)val,
					GetValue = (instance) => instance.ProfileID,
				},
				new ColumnMetadataModel<ProfilePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "profile_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ProfileName",
					TableName = "profiles",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ProfileName = (string)val,
					GetValue = (instance) => instance.ProfileName,
				},
			}
		};
		
        internal static readonly TableMetadataModel<RolePoco> RolePocoMetadata = new TableMetadataModel<RolePoco>
		{
			ClassName = "Role",
			PluralClassName = "Roles",
			PrimaryKeyColumnName = "role_id",
			PrimaryKeyPropertyName = "RoleID",
			TableName = "roles",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.RoleID,
			SetPrimaryKey = (instance, val) => instance.RoleID = val,
			IsNew = (instance) => instance.RoleID == default,
			Clone = (instance) => new RolePoco
			{
				RoleID = instance.RoleID,
				ActorID = instance.ActorID,
				RoleName = instance.RoleName,
				ShowID = instance.ShowID,
			},
			Setters = new Dictionary<string, Action<RolePoco, object>>
			{
				{"role_id", (instance, val) => instance.RoleID = (int)val },
				{"actor_id", (instance, val) => instance.ActorID = (int)val },
				{"role_name", (instance, val) => instance.RoleName = (string)val },
				{"show_id", (instance, val) => instance.ShowID = (int)val },
			},
			Getters = new Dictionary<string, Func<RolePoco, object>>
			{
				{"role_id", (instance) => instance.RoleID },
				{"actor_id", (instance) => instance.ActorID },
				{"role_name", (instance) => instance.RoleName },
				{"show_id", (instance) => instance.ShowID },
			},
			Columns = new List<ColumnMetadataModel<RolePoco>>
			{
				new ColumnMetadataModel<RolePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "role_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "roles_pkey" == string.Empty ? null : "roles_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "RoleID",
					TableName = "roles",
					TableSchema = "public",
					SetValue = (instance, val) => instance.RoleID = (int)val,
					GetValue = (instance) => instance.RoleID,
				},
				new ColumnMetadataModel<RolePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "actor_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_roles_actor_id" == string.Empty ? null : "fk_roles_actor_id",						
					ForeignKeyReferenceColumnName = "actor_id" == string.Empty ? null : "actor_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "actors" == string.Empty ? null : "actors",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ActorID",
					TableName = "roles",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ActorID = (int)val,
					GetValue = (instance) => instance.ActorID,
				},
				new ColumnMetadataModel<RolePoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "role_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "RoleName",
					TableName = "roles",
					TableSchema = "public",
					SetValue = (instance, val) => instance.RoleName = (string)val,
					GetValue = (instance) => instance.RoleName,
				},
				new ColumnMetadataModel<RolePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_roles_show_id" == string.Empty ? null : "fk_roles_show_id",						
					ForeignKeyReferenceColumnName = "show_id" == string.Empty ? null : "show_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "shows" == string.Empty ? null : "shows",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "roles",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowID = (int)val,
					GetValue = (instance) => instance.ShowID,
				},
			}
		};
		
        internal static readonly TableMetadataModel<SettingPoco> SettingPocoMetadata = new TableMetadataModel<SettingPoco>
		{
			ClassName = "Setting",
			PluralClassName = "Settings",
			PrimaryKeyColumnName = "setting_id",
			PrimaryKeyPropertyName = "SettingID",
			TableName = "settings",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.SettingID,
			SetPrimaryKey = (instance, val) => instance.SettingID = val,
			IsNew = (instance) => instance.SettingID == default,
			Clone = (instance) => new SettingPoco
			{
				SettingID = instance.SettingID,
				SettingValue = instance.SettingValue,
				SettingName = instance.SettingName,
			},
			Setters = new Dictionary<string, Action<SettingPoco, object>>
			{
				{"setting_id", (instance, val) => instance.SettingID = (int)val },
				{"setting_value", (instance, val) => instance.SettingValue = (string)val },
				{"setting_name", (instance, val) => instance.SettingName = (string)val },
			},
			Getters = new Dictionary<string, Func<SettingPoco, object>>
			{
				{"setting_id", (instance) => instance.SettingID },
				{"setting_value", (instance) => instance.SettingValue },
				{"setting_name", (instance) => instance.SettingName },
			},
			Columns = new List<ColumnMetadataModel<SettingPoco>>
			{
				new ColumnMetadataModel<SettingPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "setting_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "settings_pkey" == string.Empty ? null : "settings_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "SettingID",
					TableName = "settings",
					TableSchema = "public",
					SetValue = (instance, val) => instance.SettingID = (int)val,
					GetValue = (instance) => instance.SettingID,
				},
				new ColumnMetadataModel<SettingPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "setting_value",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "SettingValue",
					TableName = "settings",
					TableSchema = "public",
					SetValue = (instance, val) => instance.SettingValue = (string)val,
					GetValue = (instance) => instance.SettingValue,
				},
				new ColumnMetadataModel<SettingPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "setting_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "SettingName",
					TableName = "settings",
					TableSchema = "public",
					SetValue = (instance, val) => instance.SettingName = (string)val,
					GetValue = (instance) => instance.SettingName,
				},
			}
		};
		
        internal static readonly TableMetadataModel<ShowPoco> ShowPocoMetadata = new TableMetadataModel<ShowPoco>
		{
			ClassName = "Show",
			PluralClassName = "Shows",
			PrimaryKeyColumnName = "show_id",
			PrimaryKeyPropertyName = "ShowID",
			TableName = "shows",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ShowID,
			SetPrimaryKey = (instance, val) => instance.ShowID = val,
			IsNew = (instance) => instance.ShowID == default,
			Clone = (instance) => new ShowPoco
			{
				ShowID = instance.ShowID,
				AirDay = instance.AirDay,
				AirTime = instance.AirTime,
				FirstAired = instance.FirstAired,
				Imdbid = instance.Imdbid,
				LastUpdated = instance.LastUpdated,
				NetworkID = instance.NetworkID,
				ShowBanner = instance.ShowBanner,
				ShowDescription = instance.ShowDescription,
				ShowName = instance.ShowName,
				ShowStatus = instance.ShowStatus,
				Thetvdbid = instance.Thetvdbid,
			},
			Setters = new Dictionary<string, Action<ShowPoco, object>>
			{
				{"show_id", (instance, val) => instance.ShowID = (int)val },
				{"air_day", (instance, val) => instance.AirDay = (int?)val },
				{"air_time", (instance, val) => instance.AirTime = (DateTime?)val },
				{"first_aired", (instance, val) => instance.FirstAired = (DateTime?)val },
				{"imdbid", (instance, val) => instance.Imdbid = (string)val },
				{"last_updated", (instance, val) => instance.LastUpdated = (DateTime)val },
				{"network_id", (instance, val) => instance.NetworkID = (int)val },
				{"show_banner", (instance, val) => instance.ShowBanner = (string)val },
				{"show_description", (instance, val) => instance.ShowDescription = (string)val },
				{"show_name", (instance, val) => instance.ShowName = (string)val },
				{"show_status", (instance, val) => instance.ShowStatus = (int)val },
				{"thetvdbid", (instance, val) => instance.Thetvdbid = (int)val },
			},
			Getters = new Dictionary<string, Func<ShowPoco, object>>
			{
				{"show_id", (instance) => instance.ShowID },
				{"air_day", (instance) => instance.AirDay },
				{"air_time", (instance) => instance.AirTime },
				{"first_aired", (instance) => instance.FirstAired },
				{"imdbid", (instance) => instance.Imdbid },
				{"last_updated", (instance) => instance.LastUpdated },
				{"network_id", (instance) => instance.NetworkID },
				{"show_banner", (instance) => instance.ShowBanner },
				{"show_description", (instance) => instance.ShowDescription },
				{"show_name", (instance) => instance.ShowName },
				{"show_status", (instance) => instance.ShowStatus },
				{"thetvdbid", (instance) => instance.Thetvdbid },
			},
			Columns = new List<ColumnMetadataModel<ShowPoco>>
			{
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "shows_pkey" == string.Empty ? null : "shows_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowID = (int)val,
					GetValue = (instance) => instance.ShowID,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "int?",
					ClrType = typeof(int?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "air_day",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "AirDay",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.AirDay = (int?)val,
					GetValue = (instance) => instance.AirDay,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "DateTime?",
					ClrType = typeof(DateTime?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "air_time",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "AirTime",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.AirTime = (DateTime?)val,
					GetValue = (instance) => instance.AirTime,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "DateTime?",
					ClrType = typeof(DateTime?),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "first_aired",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "FirstAired",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.FirstAired = (DateTime?)val,
					GetValue = (instance) => instance.FirstAired,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "imdbid",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Imdbid",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Imdbid = (string)val,
					GetValue = (instance) => instance.Imdbid,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "DateTime",
					ClrType = typeof(DateTime),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "last_updated",
					DbDataType = "timestamp without time zone",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "LastUpdated",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.LastUpdated = (DateTime)val,
					GetValue = (instance) => instance.LastUpdated,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "network_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_shows_network_id" == string.Empty ? null : "fk_shows_network_id",						
					ForeignKeyReferenceColumnName = "network_id" == string.Empty ? null : "network_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "networks" == string.Empty ? null : "networks",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "NetworkID",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.NetworkID = (int)val,
					GetValue = (instance) => instance.NetworkID,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_banner",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ShowBanner",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowBanner = (string)val,
					GetValue = (instance) => instance.ShowBanner,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_description",
					DbDataType = "text",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Text",
					Linq2dbDataType = DataType.Text,
					NpgsDataTypeName = "NpgsqlDbType.Text",
					NpgsDataType = NpgsqlDbType.Text,
					PropertyName = "ShowDescription",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowDescription = (string)val,
					GetValue = (instance) => instance.ShowDescription,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_name",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ShowName",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowName = (string)val,
					GetValue = (instance) => instance.ShowName,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_status",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowStatus",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowStatus = (int)val,
					GetValue = (instance) => instance.ShowStatus,
				},
				new ColumnMetadataModel<ShowPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "thetvdbid",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "Thetvdbid",
					TableName = "shows",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Thetvdbid = (int)val,
					GetValue = (instance) => instance.Thetvdbid,
				},
			}
		};
		
        internal static readonly TableMetadataModel<ShowGenrePoco> ShowGenrePocoMetadata = new TableMetadataModel<ShowGenrePoco>
		{
			ClassName = "ShowGenre",
			PluralClassName = "ShowsGenres",
			PrimaryKeyColumnName = "shows_genres_id",
			PrimaryKeyPropertyName = "ShowsGenresID",
			TableName = "shows_genres",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.ShowsGenresID,
			SetPrimaryKey = (instance, val) => instance.ShowsGenresID = val,
			IsNew = (instance) => instance.ShowsGenresID == default,
			Clone = (instance) => new ShowGenrePoco
			{
				ShowsGenresID = instance.ShowsGenresID,
				ShowID = instance.ShowID,
				GenreID = instance.GenreID,
			},
			Setters = new Dictionary<string, Action<ShowGenrePoco, object>>
			{
				{"shows_genres_id", (instance, val) => instance.ShowsGenresID = (int)val },
				{"show_id", (instance, val) => instance.ShowID = (int)val },
				{"genre_id", (instance, val) => instance.GenreID = (int)val },
			},
			Getters = new Dictionary<string, Func<ShowGenrePoco, object>>
			{
				{"shows_genres_id", (instance) => instance.ShowsGenresID },
				{"show_id", (instance) => instance.ShowID },
				{"genre_id", (instance) => instance.GenreID },
			},
			Columns = new List<ColumnMetadataModel<ShowGenrePoco>>
			{
				new ColumnMetadataModel<ShowGenrePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "shows_genres_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "shows_genres_pkey" == string.Empty ? null : "shows_genres_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowsGenresID",
					TableName = "shows_genres",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowsGenresID = (int)val,
					GetValue = (instance) => instance.ShowsGenresID,
				},
				new ColumnMetadataModel<ShowGenrePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_shows_genres_show_id" == string.Empty ? null : "fk_shows_genres_show_id",						
					ForeignKeyReferenceColumnName = "show_id" == string.Empty ? null : "show_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "shows" == string.Empty ? null : "shows",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "shows_genres",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowID = (int)val,
					GetValue = (instance) => instance.ShowID,
				},
				new ColumnMetadataModel<ShowGenrePoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "genre_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_shows_genres_genre_id" == string.Empty ? null : "fk_shows_genres_genre_id",						
					ForeignKeyReferenceColumnName = "genre_id" == string.Empty ? null : "genre_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "genres" == string.Empty ? null : "genres",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "GenreID",
					TableName = "shows_genres",
					TableSchema = "public",
					SetValue = (instance, val) => instance.GenreID = (int)val,
					GetValue = (instance) => instance.GenreID,
				},
			}
		};
		
        internal static readonly TableMetadataModel<SubscriptionPoco> SubscriptionPocoMetadata = new TableMetadataModel<SubscriptionPoco>
		{
			ClassName = "Subscription",
			PluralClassName = "Subscriptions",
			PrimaryKeyColumnName = "subscription_id",
			PrimaryKeyPropertyName = "SubscriptionID",
			TableName = "subscriptions",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.SubscriptionID,
			SetPrimaryKey = (instance, val) => instance.SubscriptionID = val,
			IsNew = (instance) => instance.SubscriptionID == default,
			Clone = (instance) => new SubscriptionPoco
			{
				SubscriptionID = instance.SubscriptionID,
				ProfileID = instance.ProfileID,
				ShowID = instance.ShowID,
			},
			Setters = new Dictionary<string, Action<SubscriptionPoco, object>>
			{
				{"subscription_id", (instance, val) => instance.SubscriptionID = (int)val },
				{"profile_id", (instance, val) => instance.ProfileID = (int)val },
				{"show_id", (instance, val) => instance.ShowID = (int)val },
			},
			Getters = new Dictionary<string, Func<SubscriptionPoco, object>>
			{
				{"subscription_id", (instance) => instance.SubscriptionID },
				{"profile_id", (instance) => instance.ProfileID },
				{"show_id", (instance) => instance.ShowID },
			},
			Columns = new List<ColumnMetadataModel<SubscriptionPoco>>
			{
				new ColumnMetadataModel<SubscriptionPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "subscription_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "subscriptions_pkey" == string.Empty ? null : "subscriptions_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "SubscriptionID",
					TableName = "subscriptions",
					TableSchema = "public",
					SetValue = (instance, val) => instance.SubscriptionID = (int)val,
					GetValue = (instance) => instance.SubscriptionID,
				},
				new ColumnMetadataModel<SubscriptionPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "profile_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_subscriptions_profile_id" == string.Empty ? null : "fk_subscriptions_profile_id",						
					ForeignKeyReferenceColumnName = "profile_id" == string.Empty ? null : "profile_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "profiles" == string.Empty ? null : "profiles",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ProfileID",
					TableName = "subscriptions",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ProfileID = (int)val,
					GetValue = (instance) => instance.ProfileID,
				},
				new ColumnMetadataModel<SubscriptionPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "show_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("True"),
					ForeignKeyConstraintName = "fk_subscriptions_show_id" == string.Empty ? null : "fk_subscriptions_show_id",						
					ForeignKeyReferenceColumnName = "show_id" == string.Empty ? null : "show_id",
					ForeignKeyReferenceSchemaName = "public" == string.Empty ? null : "public",
					ForeignKeyReferenceTableName = "shows" == string.Empty ? null : "shows",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "subscriptions",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ShowID = (int)val,
					GetValue = (instance) => instance.ShowID,
				},
			}
		};
		
        internal static readonly TableMetadataModel<UserPoco> UserPocoMetadata = new TableMetadataModel<UserPoco>
		{
			ClassName = "User",
			PluralClassName = "Users",
			PrimaryKeyColumnName = "user_id",
			PrimaryKeyPropertyName = "UserID",
			TableName = "users",
			TableSchema = "public",
			GetPrimaryKey = (instance) => instance.UserID,
			SetPrimaryKey = (instance, val) => instance.UserID = val,
			IsNew = (instance) => instance.UserID == default,
			Clone = (instance) => new UserPoco
			{
				UserID = instance.UserID,
				IsAdmin = instance.IsAdmin,
				Username = instance.Username,
				Password = instance.Password,
				ProfileID = instance.ProfileID,
			},
			Setters = new Dictionary<string, Action<UserPoco, object>>
			{
				{"user_id", (instance, val) => instance.UserID = (int)val },
				{"is_admin", (instance, val) => instance.IsAdmin = (bool)val },
				{"username", (instance, val) => instance.Username = (string)val },
				{"password", (instance, val) => instance.Password = (string)val },
				{"profile_id", (instance, val) => instance.ProfileID = (int)val },
			},
			Getters = new Dictionary<string, Func<UserPoco, object>>
			{
				{"user_id", (instance) => instance.UserID },
				{"is_admin", (instance) => instance.IsAdmin },
				{"username", (instance) => instance.Username },
				{"password", (instance) => instance.Password },
				{"profile_id", (instance) => instance.ProfileID },
			},
			Columns = new List<ColumnMetadataModel<UserPoco>>
			{
				new ColumnMetadataModel<UserPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "user_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("True"),						
					PrimaryKeyConstraintName = "users_pkey" == string.Empty ? null : "users_pkey",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "UserID",
					TableName = "users",
					TableSchema = "public",
					SetValue = (instance, val) => instance.UserID = (int)val,
					GetValue = (instance) => instance.UserID,
				},
				new ColumnMetadataModel<UserPoco>
				{						
					ClrTypeName = "bool",
					ClrType = typeof(bool),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "is_admin",
					DbDataType = "boolean",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Boolean",
					Linq2dbDataType = DataType.Boolean,
					NpgsDataTypeName = "NpgsqlDbType.Boolean",
					NpgsDataType = NpgsqlDbType.Boolean,
					PropertyName = "IsAdmin",
					TableName = "users",
					TableSchema = "public",
					SetValue = (instance, val) => instance.IsAdmin = (bool)val,
					GetValue = (instance) => instance.IsAdmin,
				},
				new ColumnMetadataModel<UserPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "username",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Username",
					TableName = "users",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Username = (string)val,
					GetValue = (instance) => instance.Username,
				},
				new ColumnMetadataModel<UserPoco>
				{						
					ClrTypeName = "string",
					ClrType = typeof(string),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "password",
					DbDataType = "character varying",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Password",
					TableName = "users",
					TableSchema = "public",
					SetValue = (instance, val) => instance.Password = (string)val,
					GetValue = (instance) => instance.Password,
				},
				new ColumnMetadataModel<UserPoco>
				{						
					ClrTypeName = "int",
					ClrType = typeof(int),
					ColumnComment = "" == string.Empty ? null : "",
					Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
					ColumnName = "profile_id",
					DbDataType = "integer",
					IsPrimaryKey = bool.Parse("False"),						
					PrimaryKeyConstraintName = "" == string.Empty ? null : "",
					IsForeignKey = bool.Parse("False"),
					ForeignKeyConstraintName = "" == string.Empty ? null : "",						
					ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
					ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
					ForeignKeyReferenceTableName = "" == string.Empty ? null : "",												
					IsNullable = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ProfileID",
					TableName = "users",
					TableSchema = "public",
					SetValue = (instance, val) => instance.ProfileID = (int)val,
					GetValue = (instance) => instance.ProfileID,
				},
			}
		};
		
		static DbService()
		{
			ActorPocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ActorPoco>>();

            foreach (var column in ActorPocoMetadata.Columns)
            {
                ActorPocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			ApiChangeTypePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ApiChangeTypePoco>>();

            foreach (var column in ApiChangeTypePocoMetadata.Columns)
            {
                ApiChangeTypePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			ApiChangePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ApiChangePoco>>();

            foreach (var column in ApiChangePocoMetadata.Columns)
            {
                ApiChangePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			ApiResponsePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ApiResponsePoco>>();

            foreach (var column in ApiResponsePocoMetadata.Columns)
            {
                ApiResponsePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			EpisodePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<EpisodePoco>>();

            foreach (var column in EpisodePocoMetadata.Columns)
            {
                EpisodePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			GenrePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<GenrePoco>>();

            foreach (var column in GenrePocoMetadata.Columns)
            {
                GenrePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			NetworkPocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<NetworkPoco>>();

            foreach (var column in NetworkPocoMetadata.Columns)
            {
                NetworkPocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			ProfilePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ProfilePoco>>();

            foreach (var column in ProfilePocoMetadata.Columns)
            {
                ProfilePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			RolePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<RolePoco>>();

            foreach (var column in RolePocoMetadata.Columns)
            {
                RolePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			SettingPocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<SettingPoco>>();

            foreach (var column in SettingPocoMetadata.Columns)
            {
                SettingPocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			ShowPocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ShowPoco>>();

            foreach (var column in ShowPocoMetadata.Columns)
            {
                ShowPocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			ShowGenrePocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<ShowGenrePoco>>();

            foreach (var column in ShowGenrePocoMetadata.Columns)
            {
                ShowGenrePocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			SubscriptionPocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<SubscriptionPoco>>();

            foreach (var column in SubscriptionPocoMetadata.Columns)
            {
                SubscriptionPocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

			UserPocoMetadata.ColumnsByName = new Dictionary<string, ColumnMetadataModel<UserPoco>>();

            foreach (var column in UserPocoMetadata.Columns)
            {
                UserPocoMetadata.ColumnsByName[column.ColumnName] = column;
            }

		}

		private static readonly IReadOnlyDictionary<Type, object> MetadataByPocoType = new Dictionary<Type, object>
        {
            {typeof(ActorPoco), ActorPocoMetadata},
            {typeof(ApiChangeTypePoco), ApiChangeTypePocoMetadata},
            {typeof(ApiChangePoco), ApiChangePocoMetadata},
            {typeof(ApiResponsePoco), ApiResponsePocoMetadata},
            {typeof(EpisodePoco), EpisodePocoMetadata},
            {typeof(GenrePoco), GenrePocoMetadata},
            {typeof(NetworkPoco), NetworkPocoMetadata},
            {typeof(ProfilePoco), ProfilePocoMetadata},
            {typeof(RolePoco), RolePocoMetadata},
            {typeof(SettingPoco), SettingPocoMetadata},
            {typeof(ShowPoco), ShowPocoMetadata},
            {typeof(ShowGenrePoco), ShowGenrePocoMetadata},
            {typeof(SubscriptionPoco), SubscriptionPocoMetadata},
            {typeof(UserPoco), UserPocoMetadata},
        };		

		public static TableMetadataModel<T> GetMetadata<T>()
            where T : IPoco<T>
        {
			return (TableMetadataModel<T>)MetadataByPocoType[typeof(T)];
        }

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
