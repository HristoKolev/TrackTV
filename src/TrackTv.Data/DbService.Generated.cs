namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
	using LinqToDB;
    using LinqToDB.Mapping;

	using NpgsqlTypes;
	using Npgsql;

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

		public ActorBM ToBm()
		{
			return new ActorBM
			{
				ActorID = this.ActorID,
				ActorImage = this.ActorImage,
				ActorName = this.ActorName,
				LastUpdated = this.LastUpdated,
				Thetvdbid = this.Thetvdbid,
			};
		}
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

		public ApiChangeTypeBM ToBm()
		{
			return new ApiChangeTypeBM
			{
				ApiChangeTypeName = this.ApiChangeTypeName,
				ApiChangeTypeID = this.ApiChangeTypeID,
			};
		}
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

		public ApiChangeBM ToBm()
		{
			return new ApiChangeBM
			{
				ApiChangeThetvdbid = this.ApiChangeThetvdbid,
				ApiChangeFailCount = this.ApiChangeFailCount,
				ApiChangeCreatedDate = this.ApiChangeCreatedDate,
				ApiChangeID = this.ApiChangeID,
				ApiChangeLastFailedTime = this.ApiChangeLastFailedTime,
				ApiChangeThetvdbLastUpdated = this.ApiChangeThetvdbLastUpdated,
				ApiChangeAttachedSeriesID = this.ApiChangeAttachedSeriesID,
				ApiChangeType = this.ApiChangeType,
			};
		}
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

		public ApiResponseBM ToBm()
		{
			return new ApiResponseBM
			{
				ApiResponseEpisodeThetvdbid = this.ApiResponseEpisodeThetvdbid,
				ApiResponseShowThetvdbid = this.ApiResponseShowThetvdbid,
				ApiResponseBody = this.ApiResponseBody,
				ApiResponseID = this.ApiResponseID,
				ApiResponseLastUpdated = this.ApiResponseLastUpdated,
			};
		}
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

		public EpisodeBM ToBm()
		{
			return new EpisodeBM
			{
				EpisodeID = this.EpisodeID,
				EpisodeDescription = this.EpisodeDescription,
				EpisodeNumber = this.EpisodeNumber,
				EpisodeTitle = this.EpisodeTitle,
				FirstAired = this.FirstAired,
				Imdbid = this.Imdbid,
				LastUpdated = this.LastUpdated,
				SeasonNumber = this.SeasonNumber,
				ShowID = this.ShowID,
				Thetvdbid = this.Thetvdbid,
			};
		}
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

		public GenreBM ToBm()
		{
			return new GenreBM
			{
				GenreID = this.GenreID,
				GenreName = this.GenreName,
			};
		}
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

		public NetworkBM ToBm()
		{
			return new NetworkBM
			{
				NetworkID = this.NetworkID,
				NetworkName = this.NetworkName,
			};
		}
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

		public ProfileBM ToBm()
		{
			return new ProfileBM
			{
				ProfileID = this.ProfileID,
				ProfileName = this.ProfileName,
			};
		}
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

		public RoleBM ToBm()
		{
			return new RoleBM
			{
				RoleID = this.RoleID,
				ActorID = this.ActorID,
				RoleName = this.RoleName,
				ShowID = this.ShowID,
			};
		}
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

		public SettingBM ToBm()
		{
			return new SettingBM
			{
				SettingID = this.SettingID,
				SettingValue = this.SettingValue,
				SettingName = this.SettingName,
			};
		}
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

		public ShowBM ToBm()
		{
			return new ShowBM
			{
				ShowID = this.ShowID,
				AirDay = this.AirDay,
				AirTime = this.AirTime,
				FirstAired = this.FirstAired,
				Imdbid = this.Imdbid,
				LastUpdated = this.LastUpdated,
				NetworkID = this.NetworkID,
				ShowBanner = this.ShowBanner,
				ShowDescription = this.ShowDescription,
				ShowName = this.ShowName,
				ShowStatus = this.ShowStatus,
				Thetvdbid = this.Thetvdbid,
			};
		}
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

		public ShowGenreBM ToBm()
		{
			return new ShowGenreBM
			{
				ShowsGenresID = this.ShowsGenresID,
				ShowID = this.ShowID,
				GenreID = this.GenreID,
			};
		}
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

		public SubscriptionBM ToBm()
		{
			return new SubscriptionBM
			{
				SubscriptionID = this.SubscriptionID,
				ProfileID = this.ProfileID,
				ShowID = this.ShowID,
			};
		}
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

		public UserBM ToBm()
		{
			return new UserBM
			{
				UserID = this.UserID,
				IsAdmin = this.IsAdmin,
				Username = this.Username,
				Password = this.Password,
				ProfileID = this.ProfileID,
			};
		}
    }


    /// <summary>
    /// <para>Table name: 'actors'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ActorCM : ICatalogModel<ActorPoco>
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
        public int Thetvdbid { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'api_change_types'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeTypeCM : ICatalogModel<ApiChangeTypePoco>
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
        public int ApiChangeTypeID { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeCM : ICatalogModel<ApiChangePoco>
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
        public int ApiChangeType { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'api_responses'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiResponseCM : ICatalogModel<ApiResponsePoco>
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
        public DateTime ApiResponseLastUpdated { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'episodes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class EpisodeCM : ICatalogModel<EpisodePoco>
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
        public int Thetvdbid { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class GenreCM : ICatalogModel<GenrePoco>
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
        public string GenreName { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'networks'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class NetworkCM : ICatalogModel<NetworkPoco>
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
        public string NetworkName { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'profiles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ProfileCM : ICatalogModel<ProfilePoco>
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
        public string ProfileName { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'roles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class RoleCM : ICatalogModel<RolePoco>
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
        public int ShowID { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'settings'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SettingCM : ICatalogModel<SettingPoco>
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
        public string SettingName { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'shows'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowCM : ICatalogModel<ShowPoco>
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
        public int Thetvdbid { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'shows_genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowGenreCM : ICatalogModel<ShowGenrePoco>
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
        public int GenreID { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SubscriptionCM : ICatalogModel<SubscriptionPoco>
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
        public int ShowID { get; set; }

    }
    
    /// <summary>
    /// <para>Table name: 'users'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class UserCM : ICatalogModel<UserPoco>
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
        public int ProfileID { get; set; }

    }
    

    /// <summary>
    /// <para>Table name: 'actors'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ActorFM : IFilterModel<ActorPoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? ActorID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ActorID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ActorID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ActorID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ActorID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ActorID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ActorID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ActorID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ActorImage { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ActorImage_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ActorImage_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ActorImage_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ActorImage_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ActorImage_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ActorImage_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ActorImage_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ActorImage_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ActorImage_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ActorImage_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ActorImage_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ActorName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ActorName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ActorName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ActorName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ActorName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ActorName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ActorName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ActorName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ActorName_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ActorName_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ActorName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ActorName_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? LastUpdated { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? LastUpdated_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? LastUpdated_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? LastUpdated_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] LastUpdated_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] LastUpdated_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? Thetvdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? Thetvdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? Thetvdbid_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? Thetvdbid_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? Thetvdbid_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? Thetvdbid_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] Thetvdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] Thetvdbid_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'api_change_types'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeTypeFM : IFilterModel<ApiChangeTypePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public string ApiChangeTypeName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ApiChangeTypeName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ApiChangeTypeName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ApiChangeTypeName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ApiChangeTypeName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ApiChangeTypeName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ApiChangeTypeName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ApiChangeTypeName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ApiChangeTypeName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ApiChangeTypeName_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiChangeTypeID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiChangeTypeID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ApiChangeTypeID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ApiChangeTypeID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ApiChangeTypeID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ApiChangeTypeID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiChangeTypeID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiChangeTypeID_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeFM : IFilterModel<ApiChangePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiChangeThetvdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiChangeThetvdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ApiChangeThetvdbid_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ApiChangeThetvdbid_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ApiChangeThetvdbid_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ApiChangeThetvdbid_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiChangeThetvdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiChangeThetvdbid_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiChangeFailCount { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiChangeFailCount_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ApiChangeFailCount_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ApiChangeFailCount_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ApiChangeFailCount_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ApiChangeFailCount_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiChangeFailCount_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiChangeFailCount_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? ApiChangeCreatedDate { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? ApiChangeCreatedDate_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public DateTime? ApiChangeCreatedDate_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public DateTime? ApiChangeCreatedDate_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public DateTime? ApiChangeCreatedDate_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public DateTime? ApiChangeCreatedDate_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] ApiChangeCreatedDate_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] ApiChangeCreatedDate_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiChangeID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiChangeID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ApiChangeID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ApiChangeID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ApiChangeID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ApiChangeID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiChangeID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiChangeID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? ApiChangeLastFailedTime { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? ApiChangeLastFailedTime_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ApiChangeLastFailedTime_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ApiChangeLastFailedTime_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] ApiChangeLastFailedTime_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] ApiChangeLastFailedTime_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? ApiChangeThetvdbLastUpdated { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? ApiChangeThetvdbLastUpdated_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public DateTime? ApiChangeThetvdbLastUpdated_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public DateTime? ApiChangeThetvdbLastUpdated_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public DateTime? ApiChangeThetvdbLastUpdated_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public DateTime? ApiChangeThetvdbLastUpdated_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] ApiChangeThetvdbLastUpdated_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] ApiChangeThetvdbLastUpdated_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiChangeAttachedSeriesID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiChangeAttachedSeriesID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ApiChangeAttachedSeriesID_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ApiChangeAttachedSeriesID_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiChangeAttachedSeriesID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiChangeAttachedSeriesID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiChangeType { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiChangeType_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ApiChangeType_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ApiChangeType_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ApiChangeType_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ApiChangeType_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiChangeType_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiChangeType_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'api_responses'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiResponseFM : IFilterModel<ApiResponsePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiResponseEpisodeThetvdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiResponseEpisodeThetvdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ApiResponseEpisodeThetvdbid_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ApiResponseEpisodeThetvdbid_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiResponseEpisodeThetvdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiResponseEpisodeThetvdbid_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiResponseShowThetvdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiResponseShowThetvdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ApiResponseShowThetvdbid_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ApiResponseShowThetvdbid_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiResponseShowThetvdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiResponseShowThetvdbid_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ApiResponseBody { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ApiResponseBody_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ApiResponseBody_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ApiResponseBody_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ApiResponseBody_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ApiResponseBody_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ApiResponseBody_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ApiResponseBody_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ApiResponseBody_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ApiResponseBody_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ApiResponseID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ApiResponseID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ApiResponseID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ApiResponseID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ApiResponseID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ApiResponseID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ApiResponseID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ApiResponseID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? ApiResponseLastUpdated { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? ApiResponseLastUpdated_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public DateTime? ApiResponseLastUpdated_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public DateTime? ApiResponseLastUpdated_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public DateTime? ApiResponseLastUpdated_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public DateTime? ApiResponseLastUpdated_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] ApiResponseLastUpdated_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] ApiResponseLastUpdated_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'episodes'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class EpisodeFM : IFilterModel<EpisodePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? EpisodeID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? EpisodeID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? EpisodeID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? EpisodeID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? EpisodeID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? EpisodeID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] EpisodeID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] EpisodeID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string EpisodeDescription { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string EpisodeDescription_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string EpisodeDescription_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string EpisodeDescription_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string EpisodeDescription_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string EpisodeDescription_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string EpisodeDescription_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string EpisodeDescription_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? EpisodeDescription_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? EpisodeDescription_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] EpisodeDescription_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] EpisodeDescription_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? EpisodeNumber { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? EpisodeNumber_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? EpisodeNumber_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? EpisodeNumber_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? EpisodeNumber_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? EpisodeNumber_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] EpisodeNumber_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] EpisodeNumber_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string EpisodeTitle { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string EpisodeTitle_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string EpisodeTitle_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string EpisodeTitle_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string EpisodeTitle_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string EpisodeTitle_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string EpisodeTitle_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string EpisodeTitle_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? EpisodeTitle_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? EpisodeTitle_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] EpisodeTitle_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] EpisodeTitle_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? FirstAired { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? FirstAired_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? FirstAired_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? FirstAired_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] FirstAired_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] FirstAired_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string Imdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string Imdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string Imdbid_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string Imdbid_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string Imdbid_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string Imdbid_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string Imdbid_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string Imdbid_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? Imdbid_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? Imdbid_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] Imdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] Imdbid_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? LastUpdated { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? LastUpdated_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public DateTime? LastUpdated_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public DateTime? LastUpdated_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public DateTime? LastUpdated_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public DateTime? LastUpdated_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] LastUpdated_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] LastUpdated_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? SeasonNumber { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? SeasonNumber_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? SeasonNumber_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? SeasonNumber_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? SeasonNumber_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? SeasonNumber_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] SeasonNumber_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] SeasonNumber_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? Thetvdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? Thetvdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? Thetvdbid_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? Thetvdbid_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? Thetvdbid_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? Thetvdbid_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] Thetvdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] Thetvdbid_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class GenreFM : IFilterModel<GenrePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? GenreID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? GenreID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? GenreID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? GenreID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? GenreID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? GenreID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] GenreID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] GenreID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string GenreName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string GenreName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string GenreName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string GenreName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string GenreName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string GenreName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string GenreName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string GenreName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] GenreName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] GenreName_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'networks'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class NetworkFM : IFilterModel<NetworkPoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? NetworkID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? NetworkID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? NetworkID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? NetworkID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? NetworkID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? NetworkID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] NetworkID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] NetworkID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string NetworkName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string NetworkName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string NetworkName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string NetworkName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string NetworkName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string NetworkName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string NetworkName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string NetworkName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] NetworkName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] NetworkName_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'profiles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ProfileFM : IFilterModel<ProfilePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? ProfileID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ProfileID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ProfileID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ProfileID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ProfileID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ProfileID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ProfileID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ProfileID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ProfileName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ProfileName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ProfileName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ProfileName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ProfileName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ProfileName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ProfileName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ProfileName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ProfileName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ProfileName_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'roles'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class RoleFM : IFilterModel<RolePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? RoleID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? RoleID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? RoleID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? RoleID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? RoleID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? RoleID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] RoleID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] RoleID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ActorID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ActorID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ActorID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ActorID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ActorID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ActorID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ActorID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ActorID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string RoleName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string RoleName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string RoleName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string RoleName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string RoleName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string RoleName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string RoleName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string RoleName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? RoleName_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? RoleName_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] RoleName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] RoleName_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowID_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'settings'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SettingFM : IFilterModel<SettingPoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? SettingID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? SettingID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? SettingID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? SettingID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? SettingID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? SettingID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] SettingID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] SettingID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string SettingValue { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string SettingValue_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string SettingValue_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string SettingValue_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string SettingValue_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string SettingValue_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string SettingValue_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string SettingValue_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] SettingValue_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] SettingValue_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string SettingName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string SettingName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string SettingName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string SettingName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string SettingName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string SettingName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string SettingName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string SettingName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] SettingName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] SettingName_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'shows'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowFM : IFilterModel<ShowPoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? AirDay { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? AirDay_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? AirDay_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? AirDay_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] AirDay_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] AirDay_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? AirTime { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? AirTime_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? AirTime_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? AirTime_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] AirTime_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] AirTime_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? FirstAired { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? FirstAired_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? FirstAired_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? FirstAired_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] FirstAired_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] FirstAired_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string Imdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string Imdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string Imdbid_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string Imdbid_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string Imdbid_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string Imdbid_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string Imdbid_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string Imdbid_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? Imdbid_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? Imdbid_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] Imdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] Imdbid_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public DateTime? LastUpdated { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public DateTime? LastUpdated_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public DateTime? LastUpdated_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public DateTime? LastUpdated_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public DateTime? LastUpdated_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public DateTime? LastUpdated_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public DateTime[] LastUpdated_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public DateTime[] LastUpdated_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? NetworkID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? NetworkID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? NetworkID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? NetworkID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? NetworkID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? NetworkID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] NetworkID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] NetworkID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ShowBanner { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ShowBanner_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ShowBanner_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ShowBanner_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ShowBanner_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ShowBanner_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ShowBanner_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ShowBanner_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ShowBanner_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ShowBanner_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ShowBanner_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ShowBanner_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ShowDescription { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ShowDescription_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ShowDescription_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ShowDescription_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ShowDescription_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ShowDescription_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ShowDescription_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ShowDescription_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull)]
		public bool? ShowDescription_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull)]
		public bool? ShowDescription_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ShowDescription_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ShowDescription_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string ShowName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string ShowName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string ShowName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string ShowName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string ShowName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string ShowName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string ShowName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string ShowName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] ShowName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] ShowName_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowStatus { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowStatus_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowStatus_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowStatus_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowStatus_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowStatus_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowStatus_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowStatus_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? Thetvdbid { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? Thetvdbid_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? Thetvdbid_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? Thetvdbid_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? Thetvdbid_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? Thetvdbid_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] Thetvdbid_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] Thetvdbid_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'shows_genres'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowGenreFM : IFilterModel<ShowGenrePoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowsGenresID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowsGenresID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowsGenresID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowsGenresID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowsGenresID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowsGenresID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowsGenresID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowsGenresID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? GenreID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? GenreID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? GenreID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? GenreID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? GenreID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? GenreID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] GenreID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] GenreID_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SubscriptionFM : IFilterModel<SubscriptionPoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? SubscriptionID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? SubscriptionID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? SubscriptionID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? SubscriptionID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? SubscriptionID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? SubscriptionID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] SubscriptionID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] SubscriptionID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ProfileID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ProfileID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ProfileID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ProfileID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ProfileID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ProfileID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ProfileID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ProfileID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ShowID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ShowID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ShowID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ShowID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ShowID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ShowID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ShowID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ShowID_IsNotIn { get; set; }


    }
    
    /// <summary>
    /// <para>Table name: 'users'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class UserFM : IFilterModel<UserPoco>
    {
		[FilterOperator(QueryOperatorType.Equal)]
        public int? UserID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? UserID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? UserID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? UserID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? UserID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? UserID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] UserID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] UserID_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public bool? IsAdmin { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public bool? IsAdmin_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public bool[] IsAdmin_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public bool[] IsAdmin_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string Username { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string Username_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string Username_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string Username_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string Username_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string Username_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string Username_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string Username_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] Username_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] Username_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public string Password { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public string Password_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith)]
        public string Password_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith)]
        public string Password_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith)]
        public string Password_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith)]
        public string Password_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains)]
        public string Password_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain)]
        public string Password_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public string[] Password_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public string[] Password_IsNotIn { get; set; }


		[FilterOperator(QueryOperatorType.Equal)]
        public int? ProfileID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual)]
        public int? ProfileID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan)]
        public int? ProfileID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual)]
        public int? ProfileID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan)]
        public int? ProfileID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual)]
        public int? ProfileID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn)]
		public int[] ProfileID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn)]
		public int[] ProfileID_IsNotIn { get; set; }


    }
    

    /// <summary>
    /// <para>Table name: 'actors'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ActorBM : IBusinessModel<ActorPoco>
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
        public int Thetvdbid { get; set; }

		public ActorPoco ToPoco()
		{
			return new ActorPoco
			{
				ActorID = this.ActorID,
				ActorImage = this.ActorImage,
				ActorName = this.ActorName,
				LastUpdated = this.LastUpdated,
				Thetvdbid = this.Thetvdbid,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'api_change_types'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ApiChangeTypeBM : IBusinessModel<ApiChangeTypePoco>
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
        public int ApiChangeTypeID { get; set; }

		public ApiChangeTypePoco ToPoco()
		{
			return new ApiChangeTypePoco
			{
				ApiChangeTypeName = this.ApiChangeTypeName,
				ApiChangeTypeID = this.ApiChangeTypeID,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ApiChangeBM : IBusinessModel<ApiChangePoco>
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
        public int ApiChangeType { get; set; }

		public ApiChangePoco ToPoco()
		{
			return new ApiChangePoco
			{
				ApiChangeThetvdbid = this.ApiChangeThetvdbid,
				ApiChangeFailCount = this.ApiChangeFailCount,
				ApiChangeCreatedDate = this.ApiChangeCreatedDate,
				ApiChangeID = this.ApiChangeID,
				ApiChangeLastFailedTime = this.ApiChangeLastFailedTime,
				ApiChangeThetvdbLastUpdated = this.ApiChangeThetvdbLastUpdated,
				ApiChangeAttachedSeriesID = this.ApiChangeAttachedSeriesID,
				ApiChangeType = this.ApiChangeType,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'api_responses'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ApiResponseBM : IBusinessModel<ApiResponsePoco>
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
        public DateTime ApiResponseLastUpdated { get; set; }

		public ApiResponsePoco ToPoco()
		{
			return new ApiResponsePoco
			{
				ApiResponseEpisodeThetvdbid = this.ApiResponseEpisodeThetvdbid,
				ApiResponseShowThetvdbid = this.ApiResponseShowThetvdbid,
				ApiResponseBody = this.ApiResponseBody,
				ApiResponseID = this.ApiResponseID,
				ApiResponseLastUpdated = this.ApiResponseLastUpdated,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'episodes'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class EpisodeBM : IBusinessModel<EpisodePoco>
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
        public int Thetvdbid { get; set; }

		public EpisodePoco ToPoco()
		{
			return new EpisodePoco
			{
				EpisodeID = this.EpisodeID,
				EpisodeDescription = this.EpisodeDescription,
				EpisodeNumber = this.EpisodeNumber,
				EpisodeTitle = this.EpisodeTitle,
				FirstAired = this.FirstAired,
				Imdbid = this.Imdbid,
				LastUpdated = this.LastUpdated,
				SeasonNumber = this.SeasonNumber,
				ShowID = this.ShowID,
				Thetvdbid = this.Thetvdbid,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'genres'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class GenreBM : IBusinessModel<GenrePoco>
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
        public string GenreName { get; set; }

		public GenrePoco ToPoco()
		{
			return new GenrePoco
			{
				GenreID = this.GenreID,
				GenreName = this.GenreName,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'networks'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class NetworkBM : IBusinessModel<NetworkPoco>
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
        public string NetworkName { get; set; }

		public NetworkPoco ToPoco()
		{
			return new NetworkPoco
			{
				NetworkID = this.NetworkID,
				NetworkName = this.NetworkName,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'profiles'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ProfileBM : IBusinessModel<ProfilePoco>
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
        public string ProfileName { get; set; }

		public ProfilePoco ToPoco()
		{
			return new ProfilePoco
			{
				ProfileID = this.ProfileID,
				ProfileName = this.ProfileName,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'roles'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class RoleBM : IBusinessModel<RolePoco>
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
        public int ShowID { get; set; }

		public RolePoco ToPoco()
		{
			return new RolePoco
			{
				RoleID = this.RoleID,
				ActorID = this.ActorID,
				RoleName = this.RoleName,
				ShowID = this.ShowID,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'settings'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class SettingBM : IBusinessModel<SettingPoco>
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
        public string SettingName { get; set; }

		public SettingPoco ToPoco()
		{
			return new SettingPoco
			{
				SettingID = this.SettingID,
				SettingValue = this.SettingValue,
				SettingName = this.SettingName,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'shows'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ShowBM : IBusinessModel<ShowPoco>
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
        public int Thetvdbid { get; set; }

		public ShowPoco ToPoco()
		{
			return new ShowPoco
			{
				ShowID = this.ShowID,
				AirDay = this.AirDay,
				AirTime = this.AirTime,
				FirstAired = this.FirstAired,
				Imdbid = this.Imdbid,
				LastUpdated = this.LastUpdated,
				NetworkID = this.NetworkID,
				ShowBanner = this.ShowBanner,
				ShowDescription = this.ShowDescription,
				ShowName = this.ShowName,
				ShowStatus = this.ShowStatus,
				Thetvdbid = this.Thetvdbid,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'shows_genres'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class ShowGenreBM : IBusinessModel<ShowGenrePoco>
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
        public int GenreID { get; set; }

		public ShowGenrePoco ToPoco()
		{
			return new ShowGenrePoco
			{
				ShowsGenresID = this.ShowsGenresID,
				ShowID = this.ShowID,
				GenreID = this.GenreID,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class SubscriptionBM : IBusinessModel<SubscriptionPoco>
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
        public int ShowID { get; set; }

		public SubscriptionPoco ToPoco()
		{
			return new SubscriptionPoco
			{
				SubscriptionID = this.SubscriptionID,
				ProfileID = this.ProfileID,
				ShowID = this.ShowID,
			};
		}
	}
    
    /// <summary>
    /// <para>Table name: 'users'.</para>
	/// <para>Table schema: 'public'.</para>  
    /// </summary>
    public partial class UserBM : IBusinessModel<UserPoco>
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
        public int ProfileID { get; set; }

		public UserPoco ToPoco()
		{
			return new UserPoco
			{
				UserID = this.UserID,
				IsAdmin = this.IsAdmin,
				Username = this.Username,
				Password = this.Password,
				ProfileID = this.ProfileID,
			};
		}
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
			MapToCM = (instance) => new ActorCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ActorImage ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ActorName ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.LastUpdated ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.Thetvdbid },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ActorImage != myInstance.ActorImage)
				{
					changedColumnNames.Add("actor_image");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.ActorImage ?? (object)DBNull.Value });			
				}

				if(dbInstance.ActorName != myInstance.ActorName)
				{
					changedColumnNames.Add("actor_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.ActorName ?? (object)DBNull.Value });			
				}

				if(dbInstance.LastUpdated != myInstance.LastUpdated)
				{
					changedColumnNames.Add("last_updated");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.LastUpdated ?? (object)DBNull.Value });			
				}

				if(dbInstance.Thetvdbid != myInstance.Thetvdbid)
				{
					changedColumnNames.Add("thetvdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.Thetvdbid });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("actor_image");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ActorImage ?? (object)DBNull.Value });			
				columnNames.Add("actor_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ActorName ?? (object)DBNull.Value });			
				columnNames.Add("last_updated");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.LastUpdated ?? (object)DBNull.Value });			
				columnNames.Add("thetvdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.Thetvdbid });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ActorFM;

				if(fm.ActorID != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ActorID_NotEqual != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ActorID_LessThan != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ActorID_LessThanOrEqual != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ActorID_GreaterThan != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ActorID_GreaterThanOrEqual != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ActorID_IsIn != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ActorID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ActorID_IsNotIn != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ActorID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ActorImage != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ActorImage_NotEqual != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ActorImage_StartsWith != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ActorImage_DoesNotStartWith != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ActorImage_EndsWith != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ActorImage_DoesNotEndWith != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ActorImage_Contains != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ActorImage_DoesNotContain != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorImage_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ActorImage_IsNull != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ActorImage_IsNotNull != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ActorImage_IsIn != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ActorImage_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ActorImage_IsNotIn != null)
				{
					columnNames.Add("actor_image");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ActorImage_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ActorName != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ActorName_NotEqual != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ActorName_StartsWith != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ActorName_DoesNotStartWith != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ActorName_EndsWith != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ActorName_DoesNotEndWith != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ActorName_Contains != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ActorName_DoesNotContain != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ActorName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ActorName_IsNull != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ActorName_IsNotNull != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ActorName_IsIn != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ActorName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ActorName_IsNotIn != null)
				{
					columnNames.Add("actor_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ActorName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.LastUpdated != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.LastUpdated_NotEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.LastUpdated_IsNull != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.LastUpdated_IsNotNull != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.LastUpdated_IsIn != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.LastUpdated_IsNotIn != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Thetvdbid != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Thetvdbid_NotEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Thetvdbid_LessThan != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.Thetvdbid_LessThanOrEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.Thetvdbid_GreaterThan != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.Thetvdbid_GreaterThanOrEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.Thetvdbid_IsIn != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.Thetvdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Thetvdbid_IsNotIn != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.Thetvdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ActorID",
					TableName = "actors",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ActorImage",
					TableName = "actors",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ActorName",
					TableName = "actors",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime?",					
					ClrType = typeof(DateTime?),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "LastUpdated",
					TableName = "actors",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "Thetvdbid",
					TableName = "actors",
					TableSchema = "public",
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
			MapToCM = (instance) => new ApiChangeTypeCM
			{
				ApiChangeTypeName = instance.ApiChangeTypeName,
				ApiChangeTypeID = instance.ApiChangeTypeID,
			},
			Setters = new Dictionary<string, Action<ApiChangeTypePoco, object>>
			{
				{"api_change_type_name", (instance, val) => instance.ApiChangeTypeName = (string)val },
				{"api_change_type_id", (instance, val) => instance.ApiChangeTypeID = (int)val },
			},
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ApiChangeTypeName ?? (object)DBNull.Value },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ApiChangeTypeName != myInstance.ApiChangeTypeName)
				{
					changedColumnNames.Add("api_change_type_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.ApiChangeTypeName ?? (object)DBNull.Value });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("api_change_type_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ApiChangeTypeName ?? (object)DBNull.Value });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ApiChangeTypeFM;

				if(fm.ApiChangeTypeName != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeTypeName_NotEqual != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeTypeName_StartsWith != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ApiChangeTypeName_DoesNotStartWith != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ApiChangeTypeName_EndsWith != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ApiChangeTypeName_DoesNotEndWith != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ApiChangeTypeName_Contains != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ApiChangeTypeName_DoesNotContain != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ApiChangeTypeName_IsIn != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeTypeName_IsNotIn != null)
				{
					columnNames.Add("api_change_type_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ApiChangeTypeName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeTypeID != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeTypeID_NotEqual != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeTypeID_LessThan != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeTypeID_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeTypeID_GreaterThan != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeTypeID_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeTypeID_IsIn != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeTypeID_IsNotIn != null)
				{
					columnNames.Add("api_change_type_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeTypeID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ApiChangeTypeName",
					TableName = "api_change_types",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeTypeID",
					TableName = "api_change_types",
					TableSchema = "public",
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
			MapToCM = (instance) => new ApiChangeCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeThetvdbid },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeFailCount },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiChangeCreatedDate },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiChangeLastFailedTime ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiChangeThetvdbLastUpdated },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeAttachedSeriesID ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeType },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ApiChangeThetvdbid != myInstance.ApiChangeThetvdbid)
				{
					changedColumnNames.Add("api_change_thetvdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ApiChangeThetvdbid });			
				}

				if(dbInstance.ApiChangeFailCount != myInstance.ApiChangeFailCount)
				{
					changedColumnNames.Add("api_change_fail_count");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ApiChangeFailCount });			
				}

				if(dbInstance.ApiChangeCreatedDate != myInstance.ApiChangeCreatedDate)
				{
					changedColumnNames.Add("api_change_created_date");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.ApiChangeCreatedDate });			
				}

				if(dbInstance.ApiChangeLastFailedTime != myInstance.ApiChangeLastFailedTime)
				{
					changedColumnNames.Add("api_change_last_failed_time");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.ApiChangeLastFailedTime ?? (object)DBNull.Value });			
				}

				if(dbInstance.ApiChangeThetvdbLastUpdated != myInstance.ApiChangeThetvdbLastUpdated)
				{
					changedColumnNames.Add("api_change_thetvdb_last_updated");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.ApiChangeThetvdbLastUpdated });			
				}

				if(dbInstance.ApiChangeAttachedSeriesID != myInstance.ApiChangeAttachedSeriesID)
				{
					changedColumnNames.Add("api_change_attached_series_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ApiChangeAttachedSeriesID ?? (object)DBNull.Value });			
				}

				if(dbInstance.ApiChangeType != myInstance.ApiChangeType)
				{
					changedColumnNames.Add("api_change_type");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ApiChangeType });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("api_change_thetvdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeThetvdbid });			
				columnNames.Add("api_change_fail_count");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeFailCount });			
				columnNames.Add("api_change_created_date");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiChangeCreatedDate });			
				columnNames.Add("api_change_last_failed_time");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiChangeLastFailedTime ?? (object)DBNull.Value });			
				columnNames.Add("api_change_thetvdb_last_updated");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiChangeThetvdbLastUpdated });			
				columnNames.Add("api_change_attached_series_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeAttachedSeriesID ?? (object)DBNull.Value });			
				columnNames.Add("api_change_type");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiChangeType });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ApiChangeFM;

				if(fm.ApiChangeThetvdbid != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeThetvdbid_NotEqual != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeThetvdbid_LessThan != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeThetvdbid_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeThetvdbid_GreaterThan != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeThetvdbid_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeThetvdbid_IsIn != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeThetvdbid_IsNotIn != null)
				{
					columnNames.Add("api_change_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeThetvdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeFailCount != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeFailCount_NotEqual != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeFailCount_LessThan != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeFailCount_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeFailCount_GreaterThan != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeFailCount_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeFailCount_IsIn != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeFailCount_IsNotIn != null)
				{
					columnNames.Add("api_change_fail_count");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeFailCount_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeCreatedDate != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeCreatedDate_NotEqual != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeCreatedDate_LessThan != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeCreatedDate_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeCreatedDate_GreaterThan != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeCreatedDate_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeCreatedDate_IsIn != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeCreatedDate_IsNotIn != null)
				{
					columnNames.Add("api_change_created_date");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiChangeCreatedDate_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeID != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeID_NotEqual != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeID_LessThan != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeID_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeID_GreaterThan != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeID_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeID_IsIn != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeID_IsNotIn != null)
				{
					columnNames.Add("api_change_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeLastFailedTime != null)
				{
					columnNames.Add("api_change_last_failed_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeLastFailedTime });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeLastFailedTime_NotEqual != null)
				{
					columnNames.Add("api_change_last_failed_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeLastFailedTime_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeLastFailedTime_IsNull != null)
				{
					columnNames.Add("api_change_last_failed_time");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ApiChangeLastFailedTime_IsNotNull != null)
				{
					columnNames.Add("api_change_last_failed_time");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ApiChangeLastFailedTime_IsIn != null)
				{
					columnNames.Add("api_change_last_failed_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiChangeLastFailedTime_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeLastFailedTime_IsNotIn != null)
				{
					columnNames.Add("api_change_last_failed_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiChangeLastFailedTime_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeThetvdbLastUpdated != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeThetvdbLastUpdated_NotEqual != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeThetvdbLastUpdated_LessThan != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeThetvdbLastUpdated_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeThetvdbLastUpdated_GreaterThan != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeThetvdbLastUpdated_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeThetvdbLastUpdated_IsIn != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeThetvdbLastUpdated_IsNotIn != null)
				{
					columnNames.Add("api_change_thetvdb_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiChangeThetvdbLastUpdated_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeAttachedSeriesID != null)
				{
					columnNames.Add("api_change_attached_series_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeAttachedSeriesID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeAttachedSeriesID_NotEqual != null)
				{
					columnNames.Add("api_change_attached_series_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeAttachedSeriesID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeAttachedSeriesID_IsNull != null)
				{
					columnNames.Add("api_change_attached_series_id");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ApiChangeAttachedSeriesID_IsNotNull != null)
				{
					columnNames.Add("api_change_attached_series_id");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ApiChangeAttachedSeriesID_IsIn != null)
				{
					columnNames.Add("api_change_attached_series_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeAttachedSeriesID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeAttachedSeriesID_IsNotIn != null)
				{
					columnNames.Add("api_change_attached_series_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeAttachedSeriesID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiChangeType != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeType });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiChangeType_NotEqual != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeType_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiChangeType_LessThan != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeType_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiChangeType_LessThanOrEqual != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeType_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiChangeType_GreaterThan != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeType_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiChangeType_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiChangeType_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiChangeType_IsIn != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeType_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiChangeType_IsNotIn != null)
				{
					columnNames.Add("api_change_type");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiChangeType_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeThetvdbid",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeFailCount",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime",					
					ClrType = typeof(DateTime),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiChangeCreatedDate",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeID",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime?",					
					ClrType = typeof(DateTime?),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiChangeLastFailedTime",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime",					
					ClrType = typeof(DateTime),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiChangeThetvdbLastUpdated",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int?",					
					ClrType = typeof(int?),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeAttachedSeriesID",
					TableName = "api_changes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiChangeType",
					TableName = "api_changes",
					TableSchema = "public",
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
			MapToCM = (instance) => new ApiResponseCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiResponseEpisodeThetvdbid ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiResponseShowThetvdbid ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = instance.ApiResponseBody ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiResponseLastUpdated },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ApiResponseEpisodeThetvdbid != myInstance.ApiResponseEpisodeThetvdbid)
				{
					changedColumnNames.Add("api_response_episode_thetvdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ApiResponseEpisodeThetvdbid ?? (object)DBNull.Value });			
				}

				if(dbInstance.ApiResponseShowThetvdbid != myInstance.ApiResponseShowThetvdbid)
				{
					changedColumnNames.Add("api_response_show_thetvdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ApiResponseShowThetvdbid ?? (object)DBNull.Value });			
				}

				if(dbInstance.ApiResponseBody != myInstance.ApiResponseBody)
				{
					changedColumnNames.Add("api_response_body");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = myInstance.ApiResponseBody ?? (object)DBNull.Value });			
				}

				if(dbInstance.ApiResponseLastUpdated != myInstance.ApiResponseLastUpdated)
				{
					changedColumnNames.Add("api_response_last_updated");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.ApiResponseLastUpdated });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("api_response_episode_thetvdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiResponseEpisodeThetvdbid ?? (object)DBNull.Value });			
				columnNames.Add("api_response_show_thetvdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ApiResponseShowThetvdbid ?? (object)DBNull.Value });			
				columnNames.Add("api_response_body");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = instance.ApiResponseBody ?? (object)DBNull.Value });			
				columnNames.Add("api_response_last_updated");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.ApiResponseLastUpdated });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ApiResponseFM;

				if(fm.ApiResponseEpisodeThetvdbid != null)
				{
					columnNames.Add("api_response_episode_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseEpisodeThetvdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiResponseEpisodeThetvdbid_NotEqual != null)
				{
					columnNames.Add("api_response_episode_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseEpisodeThetvdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiResponseEpisodeThetvdbid_IsNull != null)
				{
					columnNames.Add("api_response_episode_thetvdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ApiResponseEpisodeThetvdbid_IsNotNull != null)
				{
					columnNames.Add("api_response_episode_thetvdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ApiResponseEpisodeThetvdbid_IsIn != null)
				{
					columnNames.Add("api_response_episode_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiResponseEpisodeThetvdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiResponseEpisodeThetvdbid_IsNotIn != null)
				{
					columnNames.Add("api_response_episode_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiResponseEpisodeThetvdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiResponseShowThetvdbid != null)
				{
					columnNames.Add("api_response_show_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseShowThetvdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiResponseShowThetvdbid_NotEqual != null)
				{
					columnNames.Add("api_response_show_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseShowThetvdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiResponseShowThetvdbid_IsNull != null)
				{
					columnNames.Add("api_response_show_thetvdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ApiResponseShowThetvdbid_IsNotNull != null)
				{
					columnNames.Add("api_response_show_thetvdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ApiResponseShowThetvdbid_IsIn != null)
				{
					columnNames.Add("api_response_show_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiResponseShowThetvdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiResponseShowThetvdbid_IsNotIn != null)
				{
					columnNames.Add("api_response_show_thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiResponseShowThetvdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiResponseBody != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiResponseBody_NotEqual != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiResponseBody_StartsWith != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ApiResponseBody_DoesNotStartWith != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ApiResponseBody_EndsWith != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ApiResponseBody_DoesNotEndWith != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ApiResponseBody_Contains != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ApiResponseBody_DoesNotContain != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ApiResponseBody_IsIn != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiResponseBody_IsNotIn != null)
				{
					columnNames.Add("api_response_body");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Jsonb) { Value = fm.ApiResponseBody_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiResponseID != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiResponseID_NotEqual != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiResponseID_LessThan != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiResponseID_LessThanOrEqual != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiResponseID_GreaterThan != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiResponseID_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ApiResponseID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiResponseID_IsIn != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiResponseID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiResponseID_IsNotIn != null)
				{
					columnNames.Add("api_response_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ApiResponseID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ApiResponseLastUpdated != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ApiResponseLastUpdated_NotEqual != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ApiResponseLastUpdated_LessThan != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ApiResponseLastUpdated_LessThanOrEqual != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ApiResponseLastUpdated_GreaterThan != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ApiResponseLastUpdated_GreaterThanOrEqual != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ApiResponseLastUpdated_IsIn != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ApiResponseLastUpdated_IsNotIn != null)
				{
					columnNames.Add("api_response_last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.ApiResponseLastUpdated_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int?",					
					ClrType = typeof(int?),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiResponseEpisodeThetvdbid",
					TableName = "api_responses",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int?",					
					ClrType = typeof(int?),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiResponseShowThetvdbid",
					TableName = "api_responses",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.BinaryJson",
					Linq2dbDataType = DataType.BinaryJson,
					NpgsDataTypeName = "NpgsqlDbType.Jsonb",
					NpgsDataType = NpgsqlDbType.Jsonb,
					PropertyName = "ApiResponseBody",
					TableName = "api_responses",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ApiResponseID",
					TableName = "api_responses",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime",					
					ClrType = typeof(DateTime),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "ApiResponseLastUpdated",
					TableName = "api_responses",
					TableSchema = "public",
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
			MapToCM = (instance) => new EpisodeCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = instance.EpisodeDescription ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.EpisodeNumber },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.EpisodeTitle ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.FirstAired ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Imdbid ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.LastUpdated },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.SeasonNumber },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.Thetvdbid },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.EpisodeDescription != myInstance.EpisodeDescription)
				{
					changedColumnNames.Add("episode_description");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = myInstance.EpisodeDescription ?? (object)DBNull.Value });			
				}

				if(dbInstance.EpisodeNumber != myInstance.EpisodeNumber)
				{
					changedColumnNames.Add("episode_number");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.EpisodeNumber });			
				}

				if(dbInstance.EpisodeTitle != myInstance.EpisodeTitle)
				{
					changedColumnNames.Add("episode_title");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.EpisodeTitle ?? (object)DBNull.Value });			
				}

				if(dbInstance.FirstAired != myInstance.FirstAired)
				{
					changedColumnNames.Add("first_aired");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.FirstAired ?? (object)DBNull.Value });			
				}

				if(dbInstance.Imdbid != myInstance.Imdbid)
				{
					changedColumnNames.Add("imdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.Imdbid ?? (object)DBNull.Value });			
				}

				if(dbInstance.LastUpdated != myInstance.LastUpdated)
				{
					changedColumnNames.Add("last_updated");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.LastUpdated });			
				}

				if(dbInstance.SeasonNumber != myInstance.SeasonNumber)
				{
					changedColumnNames.Add("season_number");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.SeasonNumber });			
				}

				if(dbInstance.ShowID != myInstance.ShowID)
				{
					changedColumnNames.Add("show_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ShowID });			
				}

				if(dbInstance.Thetvdbid != myInstance.Thetvdbid)
				{
					changedColumnNames.Add("thetvdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.Thetvdbid });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("episode_description");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = instance.EpisodeDescription ?? (object)DBNull.Value });			
				columnNames.Add("episode_number");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.EpisodeNumber });			
				columnNames.Add("episode_title");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.EpisodeTitle ?? (object)DBNull.Value });			
				columnNames.Add("first_aired");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.FirstAired ?? (object)DBNull.Value });			
				columnNames.Add("imdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Imdbid ?? (object)DBNull.Value });			
				columnNames.Add("last_updated");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.LastUpdated });			
				columnNames.Add("season_number");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.SeasonNumber });			
				columnNames.Add("show_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID });			
				columnNames.Add("thetvdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.Thetvdbid });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as EpisodeFM;

				if(fm.EpisodeID != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.EpisodeID_NotEqual != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.EpisodeID_LessThan != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.EpisodeID_LessThanOrEqual != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.EpisodeID_GreaterThan != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.EpisodeID_GreaterThanOrEqual != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.EpisodeID_IsIn != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.EpisodeID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.EpisodeID_IsNotIn != null)
				{
					columnNames.Add("episode_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.EpisodeID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.EpisodeDescription != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.EpisodeDescription_NotEqual != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.EpisodeDescription_StartsWith != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.EpisodeDescription_DoesNotStartWith != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.EpisodeDescription_EndsWith != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.EpisodeDescription_DoesNotEndWith != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.EpisodeDescription_Contains != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.EpisodeDescription_DoesNotContain != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.EpisodeDescription_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.EpisodeDescription_IsNull != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.EpisodeDescription_IsNotNull != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.EpisodeDescription_IsIn != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.EpisodeDescription_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.EpisodeDescription_IsNotIn != null)
				{
					columnNames.Add("episode_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.EpisodeDescription_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.EpisodeNumber != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeNumber });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.EpisodeNumber_NotEqual != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.EpisodeNumber_LessThan != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.EpisodeNumber_LessThanOrEqual != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.EpisodeNumber_GreaterThan != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.EpisodeNumber_GreaterThanOrEqual != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.EpisodeNumber_IsIn != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.EpisodeNumber_IsNotIn != null)
				{
					columnNames.Add("episode_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.EpisodeNumber_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.EpisodeTitle != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.EpisodeTitle_NotEqual != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.EpisodeTitle_StartsWith != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.EpisodeTitle_DoesNotStartWith != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.EpisodeTitle_EndsWith != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.EpisodeTitle_DoesNotEndWith != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.EpisodeTitle_Contains != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.EpisodeTitle_DoesNotContain != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.EpisodeTitle_IsNull != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.EpisodeTitle_IsNotNull != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.EpisodeTitle_IsIn != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.EpisodeTitle_IsNotIn != null)
				{
					columnNames.Add("episode_title");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.EpisodeTitle_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.FirstAired != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.FirstAired });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.FirstAired_NotEqual != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.FirstAired_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.FirstAired_IsNull != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.FirstAired_IsNotNull != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.FirstAired_IsIn != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.FirstAired_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.FirstAired_IsNotIn != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.FirstAired_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Imdbid != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Imdbid_NotEqual != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Imdbid_StartsWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.Imdbid_DoesNotStartWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.Imdbid_EndsWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.Imdbid_DoesNotEndWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.Imdbid_Contains != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.Imdbid_DoesNotContain != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.Imdbid_IsNull != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.Imdbid_IsNotNull != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.Imdbid_IsIn != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Imdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Imdbid_IsNotIn != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Imdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.LastUpdated != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.LastUpdated_NotEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.LastUpdated_LessThan != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.LastUpdated_LessThanOrEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.LastUpdated_GreaterThan != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.LastUpdated_GreaterThanOrEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.LastUpdated_IsIn != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.LastUpdated_IsNotIn != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.SeasonNumber != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SeasonNumber });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.SeasonNumber_NotEqual != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SeasonNumber_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.SeasonNumber_LessThan != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SeasonNumber_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.SeasonNumber_LessThanOrEqual != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SeasonNumber_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.SeasonNumber_GreaterThan != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SeasonNumber_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.SeasonNumber_GreaterThanOrEqual != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SeasonNumber_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.SeasonNumber_IsIn != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.SeasonNumber_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.SeasonNumber_IsNotIn != null)
				{
					columnNames.Add("season_number");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.SeasonNumber_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowID != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowID_NotEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowID_LessThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowID_LessThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowID_GreaterThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowID_GreaterThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowID_IsIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowID_IsNotIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Thetvdbid != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Thetvdbid_NotEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Thetvdbid_LessThan != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.Thetvdbid_LessThanOrEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.Thetvdbid_GreaterThan != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.Thetvdbid_GreaterThanOrEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.Thetvdbid_IsIn != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.Thetvdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Thetvdbid_IsNotIn != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.Thetvdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "EpisodeID",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Text",
					Linq2dbDataType = DataType.Text,
					NpgsDataTypeName = "NpgsqlDbType.Text",
					NpgsDataType = NpgsqlDbType.Text,
					PropertyName = "EpisodeDescription",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "EpisodeNumber",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "EpisodeTitle",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime?",					
					ClrType = typeof(DateTime?),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "FirstAired",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Imdbid",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime",					
					ClrType = typeof(DateTime),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "LastUpdated",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "SeasonNumber",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "episodes",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "Thetvdbid",
					TableName = "episodes",
					TableSchema = "public",
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
			MapToCM = (instance) => new GenreCM
			{
				GenreID = instance.GenreID,
				GenreName = instance.GenreName,
			},
			Setters = new Dictionary<string, Action<GenrePoco, object>>
			{
				{"genre_id", (instance, val) => instance.GenreID = (int)val },
				{"genre_name", (instance, val) => instance.GenreName = (string)val },
			},
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.GenreName ?? (object)DBNull.Value },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.GenreName != myInstance.GenreName)
				{
					changedColumnNames.Add("genre_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.GenreName ?? (object)DBNull.Value });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("genre_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.GenreName ?? (object)DBNull.Value });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as GenreFM;

				if(fm.GenreID != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.GenreID_NotEqual != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.GenreID_LessThan != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.GenreID_LessThanOrEqual != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.GenreID_GreaterThan != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.GenreID_GreaterThanOrEqual != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.GenreID_IsIn != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.GenreID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.GenreID_IsNotIn != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.GenreID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.GenreName != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.GenreName_NotEqual != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.GenreName_StartsWith != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.GenreName_DoesNotStartWith != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.GenreName_EndsWith != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.GenreName_DoesNotEndWith != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.GenreName_Contains != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.GenreName_DoesNotContain != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.GenreName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.GenreName_IsIn != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.GenreName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.GenreName_IsNotIn != null)
				{
					columnNames.Add("genre_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.GenreName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "GenreID",
					TableName = "genres",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "GenreName",
					TableName = "genres",
					TableSchema = "public",
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
			MapToCM = (instance) => new NetworkCM
			{
				NetworkID = instance.NetworkID,
				NetworkName = instance.NetworkName,
			},
			Setters = new Dictionary<string, Action<NetworkPoco, object>>
			{
				{"network_id", (instance, val) => instance.NetworkID = (int)val },
				{"network_name", (instance, val) => instance.NetworkName = (string)val },
			},
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.NetworkName ?? (object)DBNull.Value },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.NetworkName != myInstance.NetworkName)
				{
					changedColumnNames.Add("network_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.NetworkName ?? (object)DBNull.Value });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("network_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.NetworkName ?? (object)DBNull.Value });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as NetworkFM;

				if(fm.NetworkID != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.NetworkID_NotEqual != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.NetworkID_LessThan != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.NetworkID_LessThanOrEqual != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.NetworkID_GreaterThan != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.NetworkID_GreaterThanOrEqual != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.NetworkID_IsIn != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.NetworkID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.NetworkID_IsNotIn != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.NetworkID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.NetworkName != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.NetworkName_NotEqual != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.NetworkName_StartsWith != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.NetworkName_DoesNotStartWith != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.NetworkName_EndsWith != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.NetworkName_DoesNotEndWith != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.NetworkName_Contains != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.NetworkName_DoesNotContain != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.NetworkName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.NetworkName_IsIn != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.NetworkName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.NetworkName_IsNotIn != null)
				{
					columnNames.Add("network_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.NetworkName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "NetworkID",
					TableName = "networks",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "NetworkName",
					TableName = "networks",
					TableSchema = "public",
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
			MapToCM = (instance) => new ProfileCM
			{
				ProfileID = instance.ProfileID,
				ProfileName = instance.ProfileName,
			},
			Setters = new Dictionary<string, Action<ProfilePoco, object>>
			{
				{"profile_id", (instance, val) => instance.ProfileID = (int)val },
				{"profile_name", (instance, val) => instance.ProfileName = (string)val },
			},
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ProfileName ?? (object)DBNull.Value },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ProfileName != myInstance.ProfileName)
				{
					changedColumnNames.Add("profile_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.ProfileName ?? (object)DBNull.Value });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("profile_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ProfileName ?? (object)DBNull.Value });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ProfileFM;

				if(fm.ProfileID != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ProfileID_NotEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ProfileID_LessThan != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ProfileID_LessThanOrEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ProfileID_GreaterThan != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ProfileID_GreaterThanOrEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ProfileID_IsIn != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ProfileID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ProfileID_IsNotIn != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ProfileID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ProfileName != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ProfileName_NotEqual != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ProfileName_StartsWith != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ProfileName_DoesNotStartWith != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ProfileName_EndsWith != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ProfileName_DoesNotEndWith != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ProfileName_Contains != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ProfileName_DoesNotContain != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ProfileName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ProfileName_IsIn != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ProfileName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ProfileName_IsNotIn != null)
				{
					columnNames.Add("profile_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ProfileName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ProfileID",
					TableName = "profiles",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ProfileName",
					TableName = "profiles",
					TableSchema = "public",
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
			MapToCM = (instance) => new RoleCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ActorID },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.RoleName ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ActorID != myInstance.ActorID)
				{
					changedColumnNames.Add("actor_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ActorID });			
				}

				if(dbInstance.RoleName != myInstance.RoleName)
				{
					changedColumnNames.Add("role_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.RoleName ?? (object)DBNull.Value });			
				}

				if(dbInstance.ShowID != myInstance.ShowID)
				{
					changedColumnNames.Add("show_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ShowID });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("actor_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ActorID });			
				columnNames.Add("role_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.RoleName ?? (object)DBNull.Value });			
				columnNames.Add("show_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as RoleFM;

				if(fm.RoleID != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.RoleID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.RoleID_NotEqual != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.RoleID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.RoleID_LessThan != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.RoleID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.RoleID_LessThanOrEqual != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.RoleID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.RoleID_GreaterThan != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.RoleID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.RoleID_GreaterThanOrEqual != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.RoleID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.RoleID_IsIn != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.RoleID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.RoleID_IsNotIn != null)
				{
					columnNames.Add("role_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.RoleID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ActorID != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ActorID_NotEqual != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ActorID_LessThan != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ActorID_LessThanOrEqual != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ActorID_GreaterThan != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ActorID_GreaterThanOrEqual != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ActorID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ActorID_IsIn != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ActorID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ActorID_IsNotIn != null)
				{
					columnNames.Add("actor_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ActorID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.RoleName != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.RoleName_NotEqual != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.RoleName_StartsWith != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.RoleName_DoesNotStartWith != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.RoleName_EndsWith != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.RoleName_DoesNotEndWith != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.RoleName_Contains != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.RoleName_DoesNotContain != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.RoleName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.RoleName_IsNull != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.RoleName_IsNotNull != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.RoleName_IsIn != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.RoleName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.RoleName_IsNotIn != null)
				{
					columnNames.Add("role_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.RoleName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowID != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowID_NotEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowID_LessThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowID_LessThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowID_GreaterThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowID_GreaterThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowID_IsIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowID_IsNotIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "RoleID",
					TableName = "roles",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ActorID",
					TableName = "roles",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "RoleName",
					TableName = "roles",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "roles",
					TableSchema = "public",
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
			MapToCM = (instance) => new SettingCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.SettingValue ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.SettingName ?? (object)DBNull.Value },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.SettingValue != myInstance.SettingValue)
				{
					changedColumnNames.Add("setting_value");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.SettingValue ?? (object)DBNull.Value });			
				}

				if(dbInstance.SettingName != myInstance.SettingName)
				{
					changedColumnNames.Add("setting_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.SettingName ?? (object)DBNull.Value });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("setting_value");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.SettingValue ?? (object)DBNull.Value });			
				columnNames.Add("setting_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.SettingName ?? (object)DBNull.Value });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as SettingFM;

				if(fm.SettingID != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SettingID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.SettingID_NotEqual != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SettingID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.SettingID_LessThan != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SettingID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.SettingID_LessThanOrEqual != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SettingID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.SettingID_GreaterThan != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SettingID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.SettingID_GreaterThanOrEqual != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SettingID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.SettingID_IsIn != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.SettingID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.SettingID_IsNotIn != null)
				{
					columnNames.Add("setting_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.SettingID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.SettingValue != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.SettingValue_NotEqual != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.SettingValue_StartsWith != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.SettingValue_DoesNotStartWith != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.SettingValue_EndsWith != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.SettingValue_DoesNotEndWith != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.SettingValue_Contains != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.SettingValue_DoesNotContain != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingValue_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.SettingValue_IsIn != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.SettingValue_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.SettingValue_IsNotIn != null)
				{
					columnNames.Add("setting_value");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.SettingValue_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.SettingName != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.SettingName_NotEqual != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.SettingName_StartsWith != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.SettingName_DoesNotStartWith != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.SettingName_EndsWith != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.SettingName_DoesNotEndWith != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.SettingName_Contains != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.SettingName_DoesNotContain != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.SettingName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.SettingName_IsIn != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.SettingName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.SettingName_IsNotIn != null)
				{
					columnNames.Add("setting_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.SettingName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "SettingID",
					TableName = "settings",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "SettingValue",
					TableName = "settings",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "SettingName",
					TableName = "settings",
					TableSchema = "public",
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
			MapToCM = (instance) => new ShowCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.AirDay ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.AirTime ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.FirstAired ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Imdbid ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.LastUpdated },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.NetworkID },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ShowBanner ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = instance.ShowDescription ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ShowName ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowStatus },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.Thetvdbid },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.AirDay != myInstance.AirDay)
				{
					changedColumnNames.Add("air_day");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.AirDay ?? (object)DBNull.Value });			
				}

				if(dbInstance.AirTime != myInstance.AirTime)
				{
					changedColumnNames.Add("air_time");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.AirTime ?? (object)DBNull.Value });			
				}

				if(dbInstance.FirstAired != myInstance.FirstAired)
				{
					changedColumnNames.Add("first_aired");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.FirstAired ?? (object)DBNull.Value });			
				}

				if(dbInstance.Imdbid != myInstance.Imdbid)
				{
					changedColumnNames.Add("imdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.Imdbid ?? (object)DBNull.Value });			
				}

				if(dbInstance.LastUpdated != myInstance.LastUpdated)
				{
					changedColumnNames.Add("last_updated");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.LastUpdated });			
				}

				if(dbInstance.NetworkID != myInstance.NetworkID)
				{
					changedColumnNames.Add("network_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.NetworkID });			
				}

				if(dbInstance.ShowBanner != myInstance.ShowBanner)
				{
					changedColumnNames.Add("show_banner");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.ShowBanner ?? (object)DBNull.Value });			
				}

				if(dbInstance.ShowDescription != myInstance.ShowDescription)
				{
					changedColumnNames.Add("show_description");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = myInstance.ShowDescription ?? (object)DBNull.Value });			
				}

				if(dbInstance.ShowName != myInstance.ShowName)
				{
					changedColumnNames.Add("show_name");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.ShowName ?? (object)DBNull.Value });			
				}

				if(dbInstance.ShowStatus != myInstance.ShowStatus)
				{
					changedColumnNames.Add("show_status");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ShowStatus });			
				}

				if(dbInstance.Thetvdbid != myInstance.Thetvdbid)
				{
					changedColumnNames.Add("thetvdbid");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.Thetvdbid });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("air_day");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.AirDay ?? (object)DBNull.Value });			
				columnNames.Add("air_time");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.AirTime ?? (object)DBNull.Value });			
				columnNames.Add("first_aired");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.FirstAired ?? (object)DBNull.Value });			
				columnNames.Add("imdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Imdbid ?? (object)DBNull.Value });			
				columnNames.Add("last_updated");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.LastUpdated });			
				columnNames.Add("network_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.NetworkID });			
				columnNames.Add("show_banner");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ShowBanner ?? (object)DBNull.Value });			
				columnNames.Add("show_description");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = instance.ShowDescription ?? (object)DBNull.Value });			
				columnNames.Add("show_name");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.ShowName ?? (object)DBNull.Value });			
				columnNames.Add("show_status");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowStatus });			
				columnNames.Add("thetvdbid");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.Thetvdbid });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ShowFM;

				if(fm.ShowID != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowID_NotEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowID_LessThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowID_LessThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowID_GreaterThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowID_GreaterThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowID_IsIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowID_IsNotIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.AirDay != null)
				{
					columnNames.Add("air_day");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.AirDay });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.AirDay_NotEqual != null)
				{
					columnNames.Add("air_day");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.AirDay_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.AirDay_IsNull != null)
				{
					columnNames.Add("air_day");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.AirDay_IsNotNull != null)
				{
					columnNames.Add("air_day");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.AirDay_IsIn != null)
				{
					columnNames.Add("air_day");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.AirDay_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.AirDay_IsNotIn != null)
				{
					columnNames.Add("air_day");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.AirDay_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.AirTime != null)
				{
					columnNames.Add("air_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.AirTime });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.AirTime_NotEqual != null)
				{
					columnNames.Add("air_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.AirTime_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.AirTime_IsNull != null)
				{
					columnNames.Add("air_time");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.AirTime_IsNotNull != null)
				{
					columnNames.Add("air_time");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.AirTime_IsIn != null)
				{
					columnNames.Add("air_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.AirTime_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.AirTime_IsNotIn != null)
				{
					columnNames.Add("air_time");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.AirTime_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.FirstAired != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.FirstAired });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.FirstAired_NotEqual != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.FirstAired_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.FirstAired_IsNull != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.FirstAired_IsNotNull != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.FirstAired_IsIn != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.FirstAired_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.FirstAired_IsNotIn != null)
				{
					columnNames.Add("first_aired");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.FirstAired_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Imdbid != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Imdbid_NotEqual != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Imdbid_StartsWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.Imdbid_DoesNotStartWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.Imdbid_EndsWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.Imdbid_DoesNotEndWith != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.Imdbid_Contains != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.Imdbid_DoesNotContain != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Imdbid_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.Imdbid_IsNull != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.Imdbid_IsNotNull != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.Imdbid_IsIn != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Imdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Imdbid_IsNotIn != null)
				{
					columnNames.Add("imdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Imdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.LastUpdated != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.LastUpdated_NotEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.LastUpdated_LessThan != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.LastUpdated_LessThanOrEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.LastUpdated_GreaterThan != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.LastUpdated_GreaterThanOrEqual != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.LastUpdated_IsIn != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.LastUpdated_IsNotIn != null)
				{
					columnNames.Add("last_updated");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.LastUpdated_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.NetworkID != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.NetworkID_NotEqual != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.NetworkID_LessThan != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.NetworkID_LessThanOrEqual != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.NetworkID_GreaterThan != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.NetworkID_GreaterThanOrEqual != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.NetworkID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.NetworkID_IsIn != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.NetworkID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.NetworkID_IsNotIn != null)
				{
					columnNames.Add("network_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.NetworkID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowBanner != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowBanner_NotEqual != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowBanner_StartsWith != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ShowBanner_DoesNotStartWith != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ShowBanner_EndsWith != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ShowBanner_DoesNotEndWith != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ShowBanner_Contains != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ShowBanner_DoesNotContain != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowBanner_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ShowBanner_IsNull != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ShowBanner_IsNotNull != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ShowBanner_IsIn != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ShowBanner_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowBanner_IsNotIn != null)
				{
					columnNames.Add("show_banner");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ShowBanner_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowDescription != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowDescription_NotEqual != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowDescription_StartsWith != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ShowDescription_DoesNotStartWith != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ShowDescription_EndsWith != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ShowDescription_DoesNotEndWith != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ShowDescription_Contains != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ShowDescription_DoesNotContain != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.ShowDescription_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ShowDescription_IsNull != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.ShowDescription_IsNotNull != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}			

				if(fm.ShowDescription_IsIn != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.ShowDescription_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowDescription_IsNotIn != null)
				{
					columnNames.Add("show_description");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.ShowDescription_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowName != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowName_NotEqual != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowName_StartsWith != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.ShowName_DoesNotStartWith != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.ShowName_EndsWith != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.ShowName_DoesNotEndWith != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.ShowName_Contains != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.ShowName_DoesNotContain != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.ShowName_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.ShowName_IsIn != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ShowName_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowName_IsNotIn != null)
				{
					columnNames.Add("show_name");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.ShowName_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowStatus != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowStatus });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowStatus_NotEqual != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowStatus_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowStatus_LessThan != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowStatus_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowStatus_LessThanOrEqual != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowStatus_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowStatus_GreaterThan != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowStatus_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowStatus_GreaterThanOrEqual != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowStatus_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowStatus_IsIn != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowStatus_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowStatus_IsNotIn != null)
				{
					columnNames.Add("show_status");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowStatus_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Thetvdbid != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Thetvdbid_NotEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Thetvdbid_LessThan != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.Thetvdbid_LessThanOrEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.Thetvdbid_GreaterThan != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.Thetvdbid_GreaterThanOrEqual != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.Thetvdbid_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.Thetvdbid_IsIn != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.Thetvdbid_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Thetvdbid_IsNotIn != null)
				{
					columnNames.Add("thetvdbid");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.Thetvdbid_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int?",					
					ClrType = typeof(int?),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "AirDay",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime?",					
					ClrType = typeof(DateTime?),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "AirTime",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime?",					
					ClrType = typeof(DateTime?),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("True"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "FirstAired",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Imdbid",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "DateTime",					
					ClrType = typeof(DateTime),
					ClrNonNullableTypeName = "DateTime",
					ClrNonNullableType = typeof(DateTime),
					ClrNullableTypeName = "DateTime?",
					ClrNullableType = typeof(DateTime?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.DateTime2",
					Linq2dbDataType = DataType.DateTime2,
					NpgsDataTypeName = "NpgsqlDbType.Timestamp",
					NpgsDataType = NpgsqlDbType.Timestamp,
					PropertyName = "LastUpdated",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "NetworkID",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ShowBanner",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.Text",
					Linq2dbDataType = DataType.Text,
					NpgsDataTypeName = "NpgsqlDbType.Text",
					NpgsDataType = NpgsqlDbType.Text,
					PropertyName = "ShowDescription",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "ShowName",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowStatus",
					TableName = "shows",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "Thetvdbid",
					TableName = "shows",
					TableSchema = "public",
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
			MapToCM = (instance) => new ShowGenreCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.GenreID },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ShowID != myInstance.ShowID)
				{
					changedColumnNames.Add("show_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ShowID });			
				}

				if(dbInstance.GenreID != myInstance.GenreID)
				{
					changedColumnNames.Add("genre_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.GenreID });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("show_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID });			
				columnNames.Add("genre_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.GenreID });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as ShowGenreFM;

				if(fm.ShowsGenresID != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowsGenresID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowsGenresID_NotEqual != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowsGenresID_LessThan != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowsGenresID_LessThanOrEqual != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowsGenresID_GreaterThan != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowsGenresID_GreaterThanOrEqual != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowsGenresID_IsIn != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowsGenresID_IsNotIn != null)
				{
					columnNames.Add("shows_genres_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowsGenresID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowID != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowID_NotEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowID_LessThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowID_LessThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowID_GreaterThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowID_GreaterThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowID_IsIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowID_IsNotIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.GenreID != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.GenreID_NotEqual != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.GenreID_LessThan != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.GenreID_LessThanOrEqual != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.GenreID_GreaterThan != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.GenreID_GreaterThanOrEqual != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.GenreID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.GenreID_IsIn != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.GenreID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.GenreID_IsNotIn != null)
				{
					columnNames.Add("genre_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.GenreID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowsGenresID",
					TableName = "shows_genres",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "shows_genres",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "GenreID",
					TableName = "shows_genres",
					TableSchema = "public",
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
			MapToCM = (instance) => new SubscriptionCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ProfileID },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.ProfileID != myInstance.ProfileID)
				{
					changedColumnNames.Add("profile_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ProfileID });			
				}

				if(dbInstance.ShowID != myInstance.ShowID)
				{
					changedColumnNames.Add("show_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ShowID });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("profile_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ProfileID });			
				columnNames.Add("show_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ShowID });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as SubscriptionFM;

				if(fm.SubscriptionID != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SubscriptionID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.SubscriptionID_NotEqual != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SubscriptionID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.SubscriptionID_LessThan != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SubscriptionID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.SubscriptionID_LessThanOrEqual != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SubscriptionID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.SubscriptionID_GreaterThan != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SubscriptionID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.SubscriptionID_GreaterThanOrEqual != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.SubscriptionID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.SubscriptionID_IsIn != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.SubscriptionID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.SubscriptionID_IsNotIn != null)
				{
					columnNames.Add("subscription_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.SubscriptionID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ProfileID != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ProfileID_NotEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ProfileID_LessThan != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ProfileID_LessThanOrEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ProfileID_GreaterThan != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ProfileID_GreaterThanOrEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ProfileID_IsIn != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ProfileID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ProfileID_IsNotIn != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ProfileID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ShowID != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ShowID_NotEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ShowID_LessThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ShowID_LessThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ShowID_GreaterThan != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ShowID_GreaterThanOrEqual != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ShowID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ShowID_IsIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ShowID_IsNotIn != null)
				{
					columnNames.Add("show_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ShowID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "SubscriptionID",
					TableName = "subscriptions",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ProfileID",
					TableName = "subscriptions",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ShowID",
					TableName = "subscriptions",
					TableSchema = "public",
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
			MapToCM = (instance) => new UserCM
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
			GenerateParameters = (instance) => 
			{
				return new NpgsqlParameter[] 
				{
					new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = instance.IsAdmin },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Username ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Password ?? (object)DBNull.Value },
					new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ProfileID },
				};
			},
			GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.IsAdmin != myInstance.IsAdmin)
				{
					changedColumnNames.Add("is_admin");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = myInstance.IsAdmin });			
				}

				if(dbInstance.Username != myInstance.Username)
				{
					changedColumnNames.Add("username");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.Username ?? (object)DBNull.Value });			
				}

				if(dbInstance.Password != myInstance.Password)
				{
					changedColumnNames.Add("password");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.Password ?? (object)DBNull.Value });			
				}

				if(dbInstance.ProfileID != myInstance.ProfileID)
				{
					changedColumnNames.Add("profile_id");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.ProfileID });			
				}

				return (changedColumnNames, changedColumnParameters);
			},
			GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("is_admin");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = instance.IsAdmin });			
				columnNames.Add("username");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Username ?? (object)DBNull.Value });			
				columnNames.Add("password");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.Password ?? (object)DBNull.Value });			
				columnNames.Add("profile_id");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.ProfileID });			
				return (columnNames, columnParameters);
			},
			ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as UserFM;

				if(fm.UserID != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.UserID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.UserID_NotEqual != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.UserID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.UserID_LessThan != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.UserID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.UserID_LessThanOrEqual != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.UserID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.UserID_GreaterThan != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.UserID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.UserID_GreaterThanOrEqual != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.UserID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.UserID_IsIn != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.UserID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.UserID_IsNotIn != null)
				{
					columnNames.Add("user_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.UserID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.IsAdmin != null)
				{
					columnNames.Add("is_admin");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = fm.IsAdmin });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.IsAdmin_NotEqual != null)
				{
					columnNames.Add("is_admin");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = fm.IsAdmin_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.IsAdmin_IsIn != null)
				{
					columnNames.Add("is_admin");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Boolean) { Value = fm.IsAdmin_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.IsAdmin_IsNotIn != null)
				{
					columnNames.Add("is_admin");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Boolean) { Value = fm.IsAdmin_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Username != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Username_NotEqual != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Username_StartsWith != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.Username_DoesNotStartWith != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.Username_EndsWith != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.Username_DoesNotEndWith != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.Username_Contains != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.Username_DoesNotContain != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Username_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.Username_IsIn != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Username_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Username_IsNotIn != null)
				{
					columnNames.Add("username");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Username_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.Password != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.Password_NotEqual != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.Password_StartsWith != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.Password_DoesNotStartWith != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.Password_EndsWith != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.Password_DoesNotEndWith != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.Password_Contains != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.Password_DoesNotContain != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.Password_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.Password_IsIn != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Password_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.Password_IsNotIn != null)
				{
					columnNames.Add("password");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.Password_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.ProfileID != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.ProfileID_NotEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.ProfileID_LessThan != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.ProfileID_LessThanOrEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.ProfileID_GreaterThan != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.ProfileID_GreaterThanOrEqual != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.ProfileID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.ProfileID_IsIn != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ProfileID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.ProfileID_IsNotIn != null)
				{
					columnNames.Add("profile_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.ProfileID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			},
			Columns = new List<ColumnMetadataModel>
			{
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "UserID",
					TableName = "users",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "bool",					
					ClrType = typeof(bool),
					ClrNonNullableTypeName = "bool",
					ClrNonNullableType = typeof(bool),
					ClrNullableTypeName = "bool?",
					ClrNullableType = typeof(bool?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Boolean",
					Linq2dbDataType = DataType.Boolean,
					NpgsDataTypeName = "NpgsqlDbType.Boolean",
					NpgsDataType = NpgsqlDbType.Boolean,
					PropertyName = "IsAdmin",
					TableName = "users",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Username",
					TableName = "users",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "string",					
					ClrType = typeof(string),
					ClrNonNullableTypeName = "string",
					ClrNonNullableType = typeof(string),
					ClrNullableTypeName = "string",
					ClrNullableType = typeof(string),
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
					IsClrValueType = bool.Parse("False"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("True"),
					Linq2dbDataTypeName = "DataType.NVarChar",
					Linq2dbDataType = DataType.NVarChar,
					NpgsDataTypeName = "NpgsqlDbType.Varchar",
					NpgsDataType = NpgsqlDbType.Varchar,
					PropertyName = "Password",
					TableName = "users",
					TableSchema = "public",
				},
				new ColumnMetadataModel
				{						
					ClrTypeName = "int",					
					ClrType = typeof(int),
					ClrNonNullableTypeName = "int",
					ClrNonNullableType = typeof(int),
					ClrNullableTypeName = "int?",
					ClrNullableType = typeof(int?),
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
					IsClrValueType = bool.Parse("True"),
					IsClrNullableType = bool.Parse("False"),
					IsClrReferenceType = bool.Parse("False"),
					Linq2dbDataTypeName = "DataType.Int32",
					Linq2dbDataType = DataType.Int32,
					NpgsDataTypeName = "NpgsqlDbType.Integer",
					NpgsDataType = NpgsqlDbType.Integer,
					PropertyName = "ProfileID",
					TableName = "users",
					TableSchema = "public",
				},
			}
		};
		
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
		
		private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> TableToPropertyMap = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {"actors", new Dictionary<string, string>
			{
				{"actor_id", "ActorID"},
				{"actor_image", "ActorImage"},
				{"actor_name", "ActorName"},
				{"last_updated", "LastUpdated"},
				{"thetvdbid", "Thetvdbid"},
			}},
            {"api_change_types", new Dictionary<string, string>
			{
				{"api_change_type_name", "ApiChangeTypeName"},
				{"api_change_type_id", "ApiChangeTypeID"},
			}},
            {"api_changes", new Dictionary<string, string>
			{
				{"api_change_thetvdbid", "ApiChangeThetvdbid"},
				{"api_change_fail_count", "ApiChangeFailCount"},
				{"api_change_created_date", "ApiChangeCreatedDate"},
				{"api_change_id", "ApiChangeID"},
				{"api_change_last_failed_time", "ApiChangeLastFailedTime"},
				{"api_change_thetvdb_last_updated", "ApiChangeThetvdbLastUpdated"},
				{"api_change_attached_series_id", "ApiChangeAttachedSeriesID"},
				{"api_change_type", "ApiChangeType"},
			}},
            {"api_responses", new Dictionary<string, string>
			{
				{"api_response_episode_thetvdbid", "ApiResponseEpisodeThetvdbid"},
				{"api_response_show_thetvdbid", "ApiResponseShowThetvdbid"},
				{"api_response_body", "ApiResponseBody"},
				{"api_response_id", "ApiResponseID"},
				{"api_response_last_updated", "ApiResponseLastUpdated"},
			}},
            {"episodes", new Dictionary<string, string>
			{
				{"episode_id", "EpisodeID"},
				{"episode_description", "EpisodeDescription"},
				{"episode_number", "EpisodeNumber"},
				{"episode_title", "EpisodeTitle"},
				{"first_aired", "FirstAired"},
				{"imdbid", "Imdbid"},
				{"last_updated", "LastUpdated"},
				{"season_number", "SeasonNumber"},
				{"show_id", "ShowID"},
				{"thetvdbid", "Thetvdbid"},
			}},
            {"genres", new Dictionary<string, string>
			{
				{"genre_id", "GenreID"},
				{"genre_name", "GenreName"},
			}},
            {"networks", new Dictionary<string, string>
			{
				{"network_id", "NetworkID"},
				{"network_name", "NetworkName"},
			}},
            {"profiles", new Dictionary<string, string>
			{
				{"profile_id", "ProfileID"},
				{"profile_name", "ProfileName"},
			}},
            {"roles", new Dictionary<string, string>
			{
				{"role_id", "RoleID"},
				{"actor_id", "ActorID"},
				{"role_name", "RoleName"},
				{"show_id", "ShowID"},
			}},
            {"settings", new Dictionary<string, string>
			{
				{"setting_id", "SettingID"},
				{"setting_value", "SettingValue"},
				{"setting_name", "SettingName"},
			}},
            {"shows", new Dictionary<string, string>
			{
				{"show_id", "ShowID"},
				{"air_day", "AirDay"},
				{"air_time", "AirTime"},
				{"first_aired", "FirstAired"},
				{"imdbid", "Imdbid"},
				{"last_updated", "LastUpdated"},
				{"network_id", "NetworkID"},
				{"show_banner", "ShowBanner"},
				{"show_description", "ShowDescription"},
				{"show_name", "ShowName"},
				{"show_status", "ShowStatus"},
				{"thetvdbid", "Thetvdbid"},
			}},
            {"shows_genres", new Dictionary<string, string>
			{
				{"shows_genres_id", "ShowsGenresID"},
				{"show_id", "ShowID"},
				{"genre_id", "GenreID"},
			}},
            {"subscriptions", new Dictionary<string, string>
			{
				{"subscription_id", "SubscriptionID"},
				{"profile_id", "ProfileID"},
				{"show_id", "ShowID"},
			}},
            {"users", new Dictionary<string, string>
			{
				{"user_id", "UserID"},
				{"is_admin", "IsAdmin"},
				{"username", "Username"},
				{"password", "Password"},
				{"profile_id", "ProfileID"},
			}},
        };

		/// <summary>
		/// <para>Database table 'actors'.</para>		
		/// </summary>
        public IQueryable<ActorPoco> Actors => this.LinqToDbConnection.GetTable<ActorPoco>();

		/// <summary>
		/// <para>Database table 'actors'.</para>
		/// <para>Filter model 'ActorFM'.</para>
		/// <para>Catalog model 'ActorCM'.</para>
		/// </summary>
		public Task<List<ActorCM>> Filter(ActorFM filter) => this.FilterInternal<ActorPoco, ActorCM>(filter);
		
		/// <summary>
		/// <para>Database table 'api_change_types'.</para>		
		/// </summary>
        public IQueryable<ApiChangeTypePoco> ApiChangeTypes => this.LinqToDbConnection.GetTable<ApiChangeTypePoco>();

		/// <summary>
		/// <para>Database table 'api_change_types'.</para>
		/// <para>Filter model 'ApiChangeTypeFM'.</para>
		/// <para>Catalog model 'ApiChangeTypeCM'.</para>
		/// </summary>
		public Task<List<ApiChangeTypeCM>> Filter(ApiChangeTypeFM filter) => this.FilterInternal<ApiChangeTypePoco, ApiChangeTypeCM>(filter);
		
		/// <summary>
		/// <para>Database table 'api_changes'.</para>		
		/// </summary>
        public IQueryable<ApiChangePoco> ApiChanges => this.LinqToDbConnection.GetTable<ApiChangePoco>();

		/// <summary>
		/// <para>Database table 'api_changes'.</para>
		/// <para>Filter model 'ApiChangeFM'.</para>
		/// <para>Catalog model 'ApiChangeCM'.</para>
		/// </summary>
		public Task<List<ApiChangeCM>> Filter(ApiChangeFM filter) => this.FilterInternal<ApiChangePoco, ApiChangeCM>(filter);
		
		/// <summary>
		/// <para>Database table 'api_responses'.</para>		
		/// </summary>
        public IQueryable<ApiResponsePoco> ApiResponses => this.LinqToDbConnection.GetTable<ApiResponsePoco>();

		/// <summary>
		/// <para>Database table 'api_responses'.</para>
		/// <para>Filter model 'ApiResponseFM'.</para>
		/// <para>Catalog model 'ApiResponseCM'.</para>
		/// </summary>
		public Task<List<ApiResponseCM>> Filter(ApiResponseFM filter) => this.FilterInternal<ApiResponsePoco, ApiResponseCM>(filter);
		
		/// <summary>
		/// <para>Database table 'episodes'.</para>		
		/// </summary>
        public IQueryable<EpisodePoco> Episodes => this.LinqToDbConnection.GetTable<EpisodePoco>();

		/// <summary>
		/// <para>Database table 'episodes'.</para>
		/// <para>Filter model 'EpisodeFM'.</para>
		/// <para>Catalog model 'EpisodeCM'.</para>
		/// </summary>
		public Task<List<EpisodeCM>> Filter(EpisodeFM filter) => this.FilterInternal<EpisodePoco, EpisodeCM>(filter);
		
		/// <summary>
		/// <para>Database table 'genres'.</para>		
		/// </summary>
        public IQueryable<GenrePoco> Genres => this.LinqToDbConnection.GetTable<GenrePoco>();

		/// <summary>
		/// <para>Database table 'genres'.</para>
		/// <para>Filter model 'GenreFM'.</para>
		/// <para>Catalog model 'GenreCM'.</para>
		/// </summary>
		public Task<List<GenreCM>> Filter(GenreFM filter) => this.FilterInternal<GenrePoco, GenreCM>(filter);
		
		/// <summary>
		/// <para>Database table 'networks'.</para>		
		/// </summary>
        public IQueryable<NetworkPoco> Networks => this.LinqToDbConnection.GetTable<NetworkPoco>();

		/// <summary>
		/// <para>Database table 'networks'.</para>
		/// <para>Filter model 'NetworkFM'.</para>
		/// <para>Catalog model 'NetworkCM'.</para>
		/// </summary>
		public Task<List<NetworkCM>> Filter(NetworkFM filter) => this.FilterInternal<NetworkPoco, NetworkCM>(filter);
		
		/// <summary>
		/// <para>Database table 'profiles'.</para>		
		/// </summary>
        public IQueryable<ProfilePoco> Profiles => this.LinqToDbConnection.GetTable<ProfilePoco>();

		/// <summary>
		/// <para>Database table 'profiles'.</para>
		/// <para>Filter model 'ProfileFM'.</para>
		/// <para>Catalog model 'ProfileCM'.</para>
		/// </summary>
		public Task<List<ProfileCM>> Filter(ProfileFM filter) => this.FilterInternal<ProfilePoco, ProfileCM>(filter);
		
		/// <summary>
		/// <para>Database table 'roles'.</para>		
		/// </summary>
        public IQueryable<RolePoco> Roles => this.LinqToDbConnection.GetTable<RolePoco>();

		/// <summary>
		/// <para>Database table 'roles'.</para>
		/// <para>Filter model 'RoleFM'.</para>
		/// <para>Catalog model 'RoleCM'.</para>
		/// </summary>
		public Task<List<RoleCM>> Filter(RoleFM filter) => this.FilterInternal<RolePoco, RoleCM>(filter);
		
		/// <summary>
		/// <para>Database table 'settings'.</para>		
		/// </summary>
        public IQueryable<SettingPoco> Settings => this.LinqToDbConnection.GetTable<SettingPoco>();

		/// <summary>
		/// <para>Database table 'settings'.</para>
		/// <para>Filter model 'SettingFM'.</para>
		/// <para>Catalog model 'SettingCM'.</para>
		/// </summary>
		public Task<List<SettingCM>> Filter(SettingFM filter) => this.FilterInternal<SettingPoco, SettingCM>(filter);
		
		/// <summary>
		/// <para>Database table 'shows'.</para>		
		/// </summary>
        public IQueryable<ShowPoco> Shows => this.LinqToDbConnection.GetTable<ShowPoco>();

		/// <summary>
		/// <para>Database table 'shows'.</para>
		/// <para>Filter model 'ShowFM'.</para>
		/// <para>Catalog model 'ShowCM'.</para>
		/// </summary>
		public Task<List<ShowCM>> Filter(ShowFM filter) => this.FilterInternal<ShowPoco, ShowCM>(filter);
		
		/// <summary>
		/// <para>Database table 'shows_genres'.</para>		
		/// </summary>
        public IQueryable<ShowGenrePoco> ShowsGenres => this.LinqToDbConnection.GetTable<ShowGenrePoco>();

		/// <summary>
		/// <para>Database table 'shows_genres'.</para>
		/// <para>Filter model 'ShowGenreFM'.</para>
		/// <para>Catalog model 'ShowGenreCM'.</para>
		/// </summary>
		public Task<List<ShowGenreCM>> Filter(ShowGenreFM filter) => this.FilterInternal<ShowGenrePoco, ShowGenreCM>(filter);
		
		/// <summary>
		/// <para>Database table 'subscriptions'.</para>		
		/// </summary>
        public IQueryable<SubscriptionPoco> Subscriptions => this.LinqToDbConnection.GetTable<SubscriptionPoco>();

		/// <summary>
		/// <para>Database table 'subscriptions'.</para>
		/// <para>Filter model 'SubscriptionFM'.</para>
		/// <para>Catalog model 'SubscriptionCM'.</para>
		/// </summary>
		public Task<List<SubscriptionCM>> Filter(SubscriptionFM filter) => this.FilterInternal<SubscriptionPoco, SubscriptionCM>(filter);
		
		/// <summary>
		/// <para>Database table 'users'.</para>		
		/// </summary>
        public IQueryable<UserPoco> Users => this.LinqToDbConnection.GetTable<UserPoco>();

		/// <summary>
		/// <para>Database table 'users'.</para>
		/// <para>Filter model 'UserFM'.</para>
		/// <para>Catalog model 'UserCM'.</para>
		/// </summary>
		public Task<List<UserCM>> Filter(UserFM filter) => this.FilterInternal<UserPoco, UserCM>(filter);
		
    }

	public partial interface IDbService
    {
		/// <summary>
		/// <para>Database table 'actors'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ActorPoco> Actors { get; }

		/// <summary>
		/// <para>Database table 'actors'.</para>
		/// <para>Filter model 'ActorFM'.</para>
		/// <para>Catalog model 'ActorCM'.</para>
		/// </summary>
		Task<List<ActorCM>> Filter(ActorFM filter);

		/// <summary>
		/// <para>Database table 'api_change_types'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ApiChangeTypePoco> ApiChangeTypes { get; }

		/// <summary>
		/// <para>Database table 'api_change_types'.</para>
		/// <para>Filter model 'ApiChangeTypeFM'.</para>
		/// <para>Catalog model 'ApiChangeTypeCM'.</para>
		/// </summary>
		Task<List<ApiChangeTypeCM>> Filter(ApiChangeTypeFM filter);

		/// <summary>
		/// <para>Database table 'api_changes'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ApiChangePoco> ApiChanges { get; }

		/// <summary>
		/// <para>Database table 'api_changes'.</para>
		/// <para>Filter model 'ApiChangeFM'.</para>
		/// <para>Catalog model 'ApiChangeCM'.</para>
		/// </summary>
		Task<List<ApiChangeCM>> Filter(ApiChangeFM filter);

		/// <summary>
		/// <para>Database table 'api_responses'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ApiResponsePoco> ApiResponses { get; }

		/// <summary>
		/// <para>Database table 'api_responses'.</para>
		/// <para>Filter model 'ApiResponseFM'.</para>
		/// <para>Catalog model 'ApiResponseCM'.</para>
		/// </summary>
		Task<List<ApiResponseCM>> Filter(ApiResponseFM filter);

		/// <summary>
		/// <para>Database table 'episodes'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<EpisodePoco> Episodes { get; }

		/// <summary>
		/// <para>Database table 'episodes'.</para>
		/// <para>Filter model 'EpisodeFM'.</para>
		/// <para>Catalog model 'EpisodeCM'.</para>
		/// </summary>
		Task<List<EpisodeCM>> Filter(EpisodeFM filter);

		/// <summary>
		/// <para>Database table 'genres'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<GenrePoco> Genres { get; }

		/// <summary>
		/// <para>Database table 'genres'.</para>
		/// <para>Filter model 'GenreFM'.</para>
		/// <para>Catalog model 'GenreCM'.</para>
		/// </summary>
		Task<List<GenreCM>> Filter(GenreFM filter);

		/// <summary>
		/// <para>Database table 'networks'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<NetworkPoco> Networks { get; }

		/// <summary>
		/// <para>Database table 'networks'.</para>
		/// <para>Filter model 'NetworkFM'.</para>
		/// <para>Catalog model 'NetworkCM'.</para>
		/// </summary>
		Task<List<NetworkCM>> Filter(NetworkFM filter);

		/// <summary>
		/// <para>Database table 'profiles'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ProfilePoco> Profiles { get; }

		/// <summary>
		/// <para>Database table 'profiles'.</para>
		/// <para>Filter model 'ProfileFM'.</para>
		/// <para>Catalog model 'ProfileCM'.</para>
		/// </summary>
		Task<List<ProfileCM>> Filter(ProfileFM filter);

		/// <summary>
		/// <para>Database table 'roles'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<RolePoco> Roles { get; }

		/// <summary>
		/// <para>Database table 'roles'.</para>
		/// <para>Filter model 'RoleFM'.</para>
		/// <para>Catalog model 'RoleCM'.</para>
		/// </summary>
		Task<List<RoleCM>> Filter(RoleFM filter);

		/// <summary>
		/// <para>Database table 'settings'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<SettingPoco> Settings { get; }

		/// <summary>
		/// <para>Database table 'settings'.</para>
		/// <para>Filter model 'SettingFM'.</para>
		/// <para>Catalog model 'SettingCM'.</para>
		/// </summary>
		Task<List<SettingCM>> Filter(SettingFM filter);

		/// <summary>
		/// <para>Database table 'shows'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ShowPoco> Shows { get; }

		/// <summary>
		/// <para>Database table 'shows'.</para>
		/// <para>Filter model 'ShowFM'.</para>
		/// <para>Catalog model 'ShowCM'.</para>
		/// </summary>
		Task<List<ShowCM>> Filter(ShowFM filter);

		/// <summary>
		/// <para>Database table 'shows_genres'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<ShowGenrePoco> ShowsGenres { get; }

		/// <summary>
		/// <para>Database table 'shows_genres'.</para>
		/// <para>Filter model 'ShowGenreFM'.</para>
		/// <para>Catalog model 'ShowGenreCM'.</para>
		/// </summary>
		Task<List<ShowGenreCM>> Filter(ShowGenreFM filter);

		/// <summary>
		/// <para>Database table 'subscriptions'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<SubscriptionPoco> Subscriptions { get; }

		/// <summary>
		/// <para>Database table 'subscriptions'.</para>
		/// <para>Filter model 'SubscriptionFM'.</para>
		/// <para>Catalog model 'SubscriptionCM'.</para>
		/// </summary>
		Task<List<SubscriptionCM>> Filter(SubscriptionFM filter);

		/// <summary>
		/// <para>Database table 'users'.</para>
		/// <para>Table schema: 'public'.</para>
		/// </summary>
        IQueryable<UserPoco> Users { get; }

		/// <summary>
		/// <para>Database table 'users'.</para>
		/// <para>Filter model 'UserFM'.</para>
		/// <para>Catalog model 'UserCM'.</para>
		/// </summary>
		Task<List<UserCM>> Filter(UserFM filter);

    }
}
