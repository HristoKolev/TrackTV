namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        public static TableMetadataModel<ActorPoco> Metadata => TrackTvMetadata.ActorPocoMetadata;

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

        public static TableMetadataModel<ApiChangeTypePoco> Metadata => TrackTvMetadata.ApiChangeTypePocoMetadata;

        public ApiChangeTypeBM ToBm()
        {
            return new ApiChangeTypeBM
            {
                ApiChangeTypeID = this.ApiChangeTypeID,
                ApiChangeTypeName = this.ApiChangeTypeName,
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

        public static TableMetadataModel<ApiChangePoco> Metadata => TrackTvMetadata.ApiChangePocoMetadata;

        public ApiChangeBM ToBm()
        {
            return new ApiChangeBM
            {
                ApiChangeAttachedSeriesID = this.ApiChangeAttachedSeriesID,
                ApiChangeCreatedDate = this.ApiChangeCreatedDate,
                ApiChangeFailCount = this.ApiChangeFailCount,
                ApiChangeID = this.ApiChangeID,
                ApiChangeLastFailedTime = this.ApiChangeLastFailedTime,
                ApiChangeThetvdbLastUpdated = this.ApiChangeThetvdbLastUpdated,
                ApiChangeThetvdbid = this.ApiChangeThetvdbid,
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

        public static TableMetadataModel<ApiResponsePoco> Metadata => TrackTvMetadata.ApiResponsePocoMetadata;

        public ApiResponseBM ToBm()
        {
            return new ApiResponseBM
            {
                ApiResponseBody = this.ApiResponseBody,
                ApiResponseEpisodeThetvdbid = this.ApiResponseEpisodeThetvdbid,
                ApiResponseID = this.ApiResponseID,
                ApiResponseLastUpdated = this.ApiResponseLastUpdated,
                ApiResponseShowThetvdbid = this.ApiResponseShowThetvdbid,
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

        public static TableMetadataModel<EpisodePoco> Metadata => TrackTvMetadata.EpisodePocoMetadata;

        public EpisodeBM ToBm()
        {
            return new EpisodeBM
            {
                EpisodeDescription = this.EpisodeDescription,
                EpisodeID = this.EpisodeID,
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

        public static TableMetadataModel<GenrePoco> Metadata => TrackTvMetadata.GenrePocoMetadata;

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

        public static TableMetadataModel<NetworkPoco> Metadata => TrackTvMetadata.NetworkPocoMetadata;

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

        public static TableMetadataModel<ProfilePoco> Metadata => TrackTvMetadata.ProfilePocoMetadata;

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

        public static TableMetadataModel<RolePoco> Metadata => TrackTvMetadata.RolePocoMetadata;

        public RoleBM ToBm()
        {
            return new RoleBM
            {
                ActorID = this.ActorID,
                RoleID = this.RoleID,
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

        public static TableMetadataModel<SettingPoco> Metadata => TrackTvMetadata.SettingPocoMetadata;

        public SettingBM ToBm()
        {
            return new SettingBM
            {
                SettingID = this.SettingID,
                SettingName = this.SettingName,
                SettingValue = this.SettingValue,
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

        public static TableMetadataModel<ShowPoco> Metadata => TrackTvMetadata.ShowPocoMetadata;

        public ShowBM ToBm()
        {
            return new ShowBM
            {
                AirDay = this.AirDay,
                AirTime = this.AirTime,
                FirstAired = this.FirstAired,
                Imdbid = this.Imdbid,
                LastUpdated = this.LastUpdated,
                NetworkID = this.NetworkID,
                ShowBanner = this.ShowBanner,
                ShowDescription = this.ShowDescription,
                ShowID = this.ShowID,
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

        public static TableMetadataModel<ShowGenrePoco> Metadata => TrackTvMetadata.ShowGenrePocoMetadata;

        public ShowGenreBM ToBm()
        {
            return new ShowGenreBM
            {
                GenreID = this.GenreID,
                ShowID = this.ShowID,
                ShowsGenresID = this.ShowsGenresID,
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

        public static TableMetadataModel<SubscriptionPoco> Metadata => TrackTvMetadata.SubscriptionPocoMetadata;

        public SubscriptionBM ToBm()
        {
            return new SubscriptionBM
            {
                ProfileID = this.ProfileID,
                ShowID = this.ShowID,
                SubscriptionID = this.SubscriptionID,
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

        public static TableMetadataModel<UserPoco> Metadata => TrackTvMetadata.UserPocoMetadata;

        public UserBM ToBm()
        {
            return new UserBM
            {
                IsAdmin = this.IsAdmin,
                Password = this.Password,
                ProfileID = this.ProfileID,
                UserID = this.UserID,
                Username = this.Username,
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

    }

    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeCM : ICatalogModel<ApiChangePoco>
    {
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

    }

    /// <summary>
    /// <para>Table name: 'episodes'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class EpisodeCM : ICatalogModel<EpisodePoco>
    {
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
        /// <para>Column name: 'setting_name'.</para>
        /// <para>Table name: 'settings'.</para>
        /// <para>This column is not nullable.</para>
        /// <para>PostgreSQL data type: 'character varying'.</para>
        /// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
        /// <para>CLR type: 'string'.</para>
        /// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
        public string SettingName { get; set; }

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

    }

    /// <summary>
    /// <para>Table name: 'shows'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowCM : ICatalogModel<ShowPoco>
    {
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

    }

    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SubscriptionCM : ICatalogModel<SubscriptionPoco>
    {
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

    }

    /// <summary>
    /// <para>Table name: 'users'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class UserCM : ICatalogModel<UserPoco>
    {
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
        /// <para>Column name: 'username'.</para>
        /// <para>Table name: 'users'.</para>
        /// <para>This column is not nullable.</para>
        /// <para>PostgreSQL data type: 'character varying'.</para>
        /// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
        /// <para>CLR type: 'string'.</para>
        /// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
        public string Username { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'actors'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ActorFM : IFilterModel<ActorPoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int[] ActorID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int[] ActorID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string ActorImage_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public bool? ActorImage_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public bool? ActorImage_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string[] ActorImage_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ActorImage", NpgsqlDbType.Varchar, "actor_image")]
        public string[] ActorImage_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string ActorName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public bool? ActorName_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public bool? ActorName_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string[] ActorName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ActorName", NpgsqlDbType.Varchar, "actor_name")]
        public string[] ActorName_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public bool? LastUpdated_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public bool? LastUpdated_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime[] LastUpdated_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime[] LastUpdated_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int[] Thetvdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int[] Thetvdbid_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'api_change_types'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeTypeFM : IFilterModel<ApiChangeTypePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int? ApiChangeTypeID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int? ApiChangeTypeID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int? ApiChangeTypeID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int? ApiChangeTypeID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int? ApiChangeTypeID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int? ApiChangeTypeID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int[] ApiChangeTypeID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeTypeID", NpgsqlDbType.Integer, "api_change_type_id")]
        public int[] ApiChangeTypeID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string ApiChangeTypeName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string[] ApiChangeTypeName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeTypeName", NpgsqlDbType.Varchar, "api_change_type_name")]
        public string[] ApiChangeTypeName_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'api_changes'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiChangeFM : IFilterModel<ApiChangePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ApiChangeAttachedSeriesID", NpgsqlDbType.Integer, "api_change_attached_series_id")]
        public int? ApiChangeAttachedSeriesID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeAttachedSeriesID", NpgsqlDbType.Integer, "api_change_attached_series_id")]
        public int? ApiChangeAttachedSeriesID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ApiChangeAttachedSeriesID", NpgsqlDbType.Integer, "api_change_attached_series_id")]
        public bool? ApiChangeAttachedSeriesID_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ApiChangeAttachedSeriesID", NpgsqlDbType.Integer, "api_change_attached_series_id")]
        public bool? ApiChangeAttachedSeriesID_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeAttachedSeriesID", NpgsqlDbType.Integer, "api_change_attached_series_id")]
        public int[] ApiChangeAttachedSeriesID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeAttachedSeriesID", NpgsqlDbType.Integer, "api_change_attached_series_id")]
        public int[] ApiChangeAttachedSeriesID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime? ApiChangeCreatedDate { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime? ApiChangeCreatedDate_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime? ApiChangeCreatedDate_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime? ApiChangeCreatedDate_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime? ApiChangeCreatedDate_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime? ApiChangeCreatedDate_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime[] ApiChangeCreatedDate_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeCreatedDate", NpgsqlDbType.Timestamp, "api_change_created_date")]
        public DateTime[] ApiChangeCreatedDate_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int? ApiChangeFailCount { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int? ApiChangeFailCount_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int? ApiChangeFailCount_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int? ApiChangeFailCount_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int? ApiChangeFailCount_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int? ApiChangeFailCount_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int[] ApiChangeFailCount_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeFailCount", NpgsqlDbType.Integer, "api_change_fail_count")]
        public int[] ApiChangeFailCount_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int? ApiChangeID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int? ApiChangeID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int? ApiChangeID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int? ApiChangeID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int? ApiChangeID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int? ApiChangeID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int[] ApiChangeID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeID", NpgsqlDbType.Integer, "api_change_id")]
        public int[] ApiChangeID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeLastFailedTime", NpgsqlDbType.Timestamp, "api_change_last_failed_time")]
        public DateTime? ApiChangeLastFailedTime { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeLastFailedTime", NpgsqlDbType.Timestamp, "api_change_last_failed_time")]
        public DateTime? ApiChangeLastFailedTime_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ApiChangeLastFailedTime", NpgsqlDbType.Timestamp, "api_change_last_failed_time")]
        public bool? ApiChangeLastFailedTime_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ApiChangeLastFailedTime", NpgsqlDbType.Timestamp, "api_change_last_failed_time")]
        public bool? ApiChangeLastFailedTime_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeLastFailedTime", NpgsqlDbType.Timestamp, "api_change_last_failed_time")]
        public DateTime[] ApiChangeLastFailedTime_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeLastFailedTime", NpgsqlDbType.Timestamp, "api_change_last_failed_time")]
        public DateTime[] ApiChangeLastFailedTime_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime? ApiChangeThetvdbLastUpdated { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime? ApiChangeThetvdbLastUpdated_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime? ApiChangeThetvdbLastUpdated_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime? ApiChangeThetvdbLastUpdated_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime? ApiChangeThetvdbLastUpdated_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime? ApiChangeThetvdbLastUpdated_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime[] ApiChangeThetvdbLastUpdated_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeThetvdbLastUpdated", NpgsqlDbType.Timestamp, "api_change_thetvdb_last_updated")]
        public DateTime[] ApiChangeThetvdbLastUpdated_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int? ApiChangeThetvdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int? ApiChangeThetvdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int? ApiChangeThetvdbid_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int? ApiChangeThetvdbid_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int? ApiChangeThetvdbid_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int? ApiChangeThetvdbid_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int[] ApiChangeThetvdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeThetvdbid", NpgsqlDbType.Integer, "api_change_thetvdbid")]
        public int[] ApiChangeThetvdbid_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int? ApiChangeType { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int? ApiChangeType_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int? ApiChangeType_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int? ApiChangeType_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int? ApiChangeType_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int? ApiChangeType_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int[] ApiChangeType_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiChangeType", NpgsqlDbType.Integer, "api_change_type")]
        public int[] ApiChangeType_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'api_responses'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ApiResponseFM : IFilterModel<ApiResponsePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string ApiResponseBody_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string[] ApiResponseBody_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiResponseBody", NpgsqlDbType.Jsonb, "api_response_body")]
        public string[] ApiResponseBody_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiResponseEpisodeThetvdbid", NpgsqlDbType.Integer, "api_response_episode_thetvdbid")]
        public int? ApiResponseEpisodeThetvdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiResponseEpisodeThetvdbid", NpgsqlDbType.Integer, "api_response_episode_thetvdbid")]
        public int? ApiResponseEpisodeThetvdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ApiResponseEpisodeThetvdbid", NpgsqlDbType.Integer, "api_response_episode_thetvdbid")]
        public bool? ApiResponseEpisodeThetvdbid_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ApiResponseEpisodeThetvdbid", NpgsqlDbType.Integer, "api_response_episode_thetvdbid")]
        public bool? ApiResponseEpisodeThetvdbid_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiResponseEpisodeThetvdbid", NpgsqlDbType.Integer, "api_response_episode_thetvdbid")]
        public int[] ApiResponseEpisodeThetvdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiResponseEpisodeThetvdbid", NpgsqlDbType.Integer, "api_response_episode_thetvdbid")]
        public int[] ApiResponseEpisodeThetvdbid_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int? ApiResponseID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int? ApiResponseID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int? ApiResponseID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int? ApiResponseID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int? ApiResponseID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int? ApiResponseID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int[] ApiResponseID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiResponseID", NpgsqlDbType.Integer, "api_response_id")]
        public int[] ApiResponseID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime? ApiResponseLastUpdated { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime? ApiResponseLastUpdated_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime? ApiResponseLastUpdated_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime? ApiResponseLastUpdated_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime? ApiResponseLastUpdated_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime? ApiResponseLastUpdated_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime[] ApiResponseLastUpdated_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiResponseLastUpdated", NpgsqlDbType.Timestamp, "api_response_last_updated")]
        public DateTime[] ApiResponseLastUpdated_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ApiResponseShowThetvdbid", NpgsqlDbType.Integer, "api_response_show_thetvdbid")]
        public int? ApiResponseShowThetvdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ApiResponseShowThetvdbid", NpgsqlDbType.Integer, "api_response_show_thetvdbid")]
        public int? ApiResponseShowThetvdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ApiResponseShowThetvdbid", NpgsqlDbType.Integer, "api_response_show_thetvdbid")]
        public bool? ApiResponseShowThetvdbid_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ApiResponseShowThetvdbid", NpgsqlDbType.Integer, "api_response_show_thetvdbid")]
        public bool? ApiResponseShowThetvdbid_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ApiResponseShowThetvdbid", NpgsqlDbType.Integer, "api_response_show_thetvdbid")]
        public int[] ApiResponseShowThetvdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ApiResponseShowThetvdbid", NpgsqlDbType.Integer, "api_response_show_thetvdbid")]
        public int[] ApiResponseShowThetvdbid_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'episodes'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class EpisodeFM : IFilterModel<EpisodePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string EpisodeDescription_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public bool? EpisodeDescription_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public bool? EpisodeDescription_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string[] EpisodeDescription_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "EpisodeDescription", NpgsqlDbType.Text, "episode_description")]
        public string[] EpisodeDescription_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int? EpisodeID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int? EpisodeID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int? EpisodeID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int? EpisodeID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int? EpisodeID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int? EpisodeID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int[] EpisodeID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "EpisodeID", NpgsqlDbType.Integer, "episode_id")]
        public int[] EpisodeID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int? EpisodeNumber { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int? EpisodeNumber_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int? EpisodeNumber_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int? EpisodeNumber_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int? EpisodeNumber_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int? EpisodeNumber_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int[] EpisodeNumber_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "EpisodeNumber", NpgsqlDbType.Integer, "episode_number")]
        public int[] EpisodeNumber_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string EpisodeTitle_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public bool? EpisodeTitle_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public bool? EpisodeTitle_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string[] EpisodeTitle_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "EpisodeTitle", NpgsqlDbType.Varchar, "episode_title")]
        public string[] EpisodeTitle_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime? FirstAired { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime? FirstAired_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public bool? FirstAired_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public bool? FirstAired_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime[] FirstAired_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime[] FirstAired_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public bool? Imdbid_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public bool? Imdbid_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string[] Imdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string[] Imdbid_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime[] LastUpdated_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime[] LastUpdated_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int? SeasonNumber { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int? SeasonNumber_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int? SeasonNumber_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int? SeasonNumber_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int? SeasonNumber_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int? SeasonNumber_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int[] SeasonNumber_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "SeasonNumber", NpgsqlDbType.Integer, "season_number")]
        public int[] SeasonNumber_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int[] Thetvdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int[] Thetvdbid_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'genres'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class GenreFM : IFilterModel<GenrePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int[] GenreID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int[] GenreID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string GenreName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string[] GenreName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "GenreName", NpgsqlDbType.Varchar, "genre_name")]
        public string[] GenreName_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'networks'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class NetworkFM : IFilterModel<NetworkPoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int[] NetworkID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int[] NetworkID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string NetworkName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string[] NetworkName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "NetworkName", NpgsqlDbType.Varchar, "network_name")]
        public string[] NetworkName_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'profiles'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ProfileFM : IFilterModel<ProfilePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int[] ProfileID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int[] ProfileID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string ProfileName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string[] ProfileName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ProfileName", NpgsqlDbType.Varchar, "profile_name")]
        public string[] ProfileName_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'roles'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class RoleFM : IFilterModel<RolePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int? ActorID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int[] ActorID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ActorID", NpgsqlDbType.Integer, "actor_id")]
        public int[] ActorID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int? RoleID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int? RoleID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int? RoleID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int? RoleID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int? RoleID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int? RoleID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int[] RoleID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "RoleID", NpgsqlDbType.Integer, "role_id")]
        public int[] RoleID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string RoleName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public bool? RoleName_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public bool? RoleName_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string[] RoleName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "RoleName", NpgsqlDbType.Varchar, "role_name")]
        public string[] RoleName_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'settings'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SettingFM : IFilterModel<SettingPoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int? SettingID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int? SettingID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int? SettingID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int? SettingID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int? SettingID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int? SettingID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int[] SettingID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "SettingID", NpgsqlDbType.Integer, "setting_id")]
        public int[] SettingID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string SettingName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string[] SettingName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "SettingName", NpgsqlDbType.Varchar, "setting_name")]
        public string[] SettingName_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string SettingValue_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string[] SettingValue_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "SettingValue", NpgsqlDbType.Varchar, "setting_value")]
        public string[] SettingValue_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'shows'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowFM : IFilterModel<ShowPoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "AirDay", NpgsqlDbType.Integer, "air_day")]
        public int? AirDay { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "AirDay", NpgsqlDbType.Integer, "air_day")]
        public int? AirDay_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "AirDay", NpgsqlDbType.Integer, "air_day")]
        public bool? AirDay_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "AirDay", NpgsqlDbType.Integer, "air_day")]
        public bool? AirDay_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "AirDay", NpgsqlDbType.Integer, "air_day")]
        public int[] AirDay_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "AirDay", NpgsqlDbType.Integer, "air_day")]
        public int[] AirDay_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "AirTime", NpgsqlDbType.Timestamp, "air_time")]
        public DateTime? AirTime { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "AirTime", NpgsqlDbType.Timestamp, "air_time")]
        public DateTime? AirTime_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "AirTime", NpgsqlDbType.Timestamp, "air_time")]
        public bool? AirTime_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "AirTime", NpgsqlDbType.Timestamp, "air_time")]
        public bool? AirTime_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "AirTime", NpgsqlDbType.Timestamp, "air_time")]
        public DateTime[] AirTime_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "AirTime", NpgsqlDbType.Timestamp, "air_time")]
        public DateTime[] AirTime_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime? FirstAired { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime? FirstAired_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public bool? FirstAired_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public bool? FirstAired_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime[] FirstAired_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "FirstAired", NpgsqlDbType.Timestamp, "first_aired")]
        public DateTime[] FirstAired_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string Imdbid_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public bool? Imdbid_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public bool? Imdbid_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string[] Imdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Imdbid", NpgsqlDbType.Varchar, "imdbid")]
        public string[] Imdbid_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime? LastUpdated_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime[] LastUpdated_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "LastUpdated", NpgsqlDbType.Timestamp, "last_updated")]
        public DateTime[] LastUpdated_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int? NetworkID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int[] NetworkID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "NetworkID", NpgsqlDbType.Integer, "network_id")]
        public int[] NetworkID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string ShowBanner_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public bool? ShowBanner_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public bool? ShowBanner_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string[] ShowBanner_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowBanner", NpgsqlDbType.Varchar, "show_banner")]
        public string[] ShowBanner_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string ShowDescription_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsNull, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public bool? ShowDescription_IsNull { get; set; }

        [FilterOperator(QueryOperatorType.IsNotNull, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public bool? ShowDescription_IsNotNull { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string[] ShowDescription_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowDescription", NpgsqlDbType.Text, "show_description")]
        public string[] ShowDescription_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string ShowName_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string[] ShowName_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowName", NpgsqlDbType.Varchar, "show_name")]
        public string[] ShowName_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int? ShowStatus { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int? ShowStatus_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int? ShowStatus_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int? ShowStatus_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int? ShowStatus_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int? ShowStatus_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int[] ShowStatus_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowStatus", NpgsqlDbType.Integer, "show_status")]
        public int[] ShowStatus_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int? Thetvdbid_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int[] Thetvdbid_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Thetvdbid", NpgsqlDbType.Integer, "thetvdbid")]
        public int[] Thetvdbid_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'shows_genres'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class ShowGenreFM : IFilterModel<ShowGenrePoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int? GenreID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int[] GenreID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "GenreID", NpgsqlDbType.Integer, "genre_id")]
        public int[] GenreID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int? ShowsGenresID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int? ShowsGenresID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int? ShowsGenresID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int? ShowsGenresID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int? ShowsGenresID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int? ShowsGenresID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int[] ShowsGenresID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowsGenresID", NpgsqlDbType.Integer, "shows_genres_id")]
        public int[] ShowsGenresID_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'subscriptions'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class SubscriptionFM : IFilterModel<SubscriptionPoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int[] ProfileID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int[] ProfileID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int? ShowID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ShowID", NpgsqlDbType.Integer, "show_id")]
        public int[] ShowID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int? SubscriptionID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int? SubscriptionID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int? SubscriptionID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int? SubscriptionID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int? SubscriptionID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int? SubscriptionID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int[] SubscriptionID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "SubscriptionID", NpgsqlDbType.Integer, "subscription_id")]
        public int[] SubscriptionID_IsNotIn { get; set; }

    }

    /// <summary>
    /// <para>Table name: 'users'.</para>
    /// <para>Table schema: 'public'.</para>
    /// </summary>
    public class UserFM : IFilterModel<UserPoco>
    {
        [FilterOperator(QueryOperatorType.Equal, "IsAdmin", NpgsqlDbType.Boolean, "is_admin")]
        public bool? IsAdmin { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "IsAdmin", NpgsqlDbType.Boolean, "is_admin")]
        public bool? IsAdmin_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "IsAdmin", NpgsqlDbType.Boolean, "is_admin")]
        public bool[] IsAdmin_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "IsAdmin", NpgsqlDbType.Boolean, "is_admin")]
        public bool[] IsAdmin_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "Password", NpgsqlDbType.Varchar, "password")]
        public string Password_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Password", NpgsqlDbType.Varchar, "password")]
        public string[] Password_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Password", NpgsqlDbType.Varchar, "password")]
        public string[] Password_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int? ProfileID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int[] ProfileID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "ProfileID", NpgsqlDbType.Integer, "profile_id")]
        public int[] ProfileID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int? UserID { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int? UserID_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.LessThan, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int? UserID_LessThan { get; set; }

        [FilterOperator(QueryOperatorType.LessThanOrEqual, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int? UserID_LessThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThan, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int? UserID_GreaterThan { get; set; }

        [FilterOperator(QueryOperatorType.GreaterThanOrEqual, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int? UserID_GreaterThanOrEqual { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int[] UserID_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "UserID", NpgsqlDbType.Integer, "user_id")]
        public int[] UserID_IsNotIn { get; set; }

        [FilterOperator(QueryOperatorType.Equal, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username { get; set; }

        [FilterOperator(QueryOperatorType.NotEqual, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_NotEqual { get; set; }

        [FilterOperator(QueryOperatorType.StartsWith, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_StartsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotStartWith, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_DoesNotStartWith { get; set; }

        [FilterOperator(QueryOperatorType.EndsWith, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_EndsWith { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotEndWith, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_DoesNotEndWith { get; set; }

        [FilterOperator(QueryOperatorType.Contains, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_Contains { get; set; }

        [FilterOperator(QueryOperatorType.DoesNotContain, "Username", NpgsqlDbType.Varchar, "username")]
        public string Username_DoesNotContain { get; set; }

        [FilterOperator(QueryOperatorType.IsIn, "Username", NpgsqlDbType.Varchar, "username")]
        public string[] Username_IsIn { get; set; }

        [FilterOperator(QueryOperatorType.IsNotIn, "Username", NpgsqlDbType.Varchar, "username")]
        public string[] Username_IsNotIn { get; set; }

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

        public ApiChangeTypePoco ToPoco()
        {
            return new ApiChangeTypePoco
            {
                ApiChangeTypeID = this.ApiChangeTypeID,
                ApiChangeTypeName = this.ApiChangeTypeName,
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
                ApiChangeAttachedSeriesID = this.ApiChangeAttachedSeriesID,
                ApiChangeCreatedDate = this.ApiChangeCreatedDate,
                ApiChangeFailCount = this.ApiChangeFailCount,
                ApiChangeID = this.ApiChangeID,
                ApiChangeLastFailedTime = this.ApiChangeLastFailedTime,
                ApiChangeThetvdbLastUpdated = this.ApiChangeThetvdbLastUpdated,
                ApiChangeThetvdbid = this.ApiChangeThetvdbid,
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

        public ApiResponsePoco ToPoco()
        {
            return new ApiResponsePoco
            {
                ApiResponseBody = this.ApiResponseBody,
                ApiResponseEpisodeThetvdbid = this.ApiResponseEpisodeThetvdbid,
                ApiResponseID = this.ApiResponseID,
                ApiResponseLastUpdated = this.ApiResponseLastUpdated,
                ApiResponseShowThetvdbid = this.ApiResponseShowThetvdbid,
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
                EpisodeDescription = this.EpisodeDescription,
                EpisodeID = this.EpisodeID,
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
                ActorID = this.ActorID,
                RoleID = this.RoleID,
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
        /// <para>Column name: 'setting_name'.</para>
        /// <para>Table name: 'settings'.</para>
        /// <para>This column is not nullable.</para>
        /// <para>PostgreSQL data type: 'character varying'.</para>
        /// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
        /// <para>CLR type: 'string'.</para>
        /// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
        public string SettingName { get; set; }

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

        public SettingPoco ToPoco()
        {
            return new SettingPoco
            {
                SettingID = this.SettingID,
                SettingName = this.SettingName,
                SettingValue = this.SettingValue,
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
                AirDay = this.AirDay,
                AirTime = this.AirTime,
                FirstAired = this.FirstAired,
                Imdbid = this.Imdbid,
                LastUpdated = this.LastUpdated,
                NetworkID = this.NetworkID,
                ShowBanner = this.ShowBanner,
                ShowDescription = this.ShowDescription,
                ShowID = this.ShowID,
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

        public ShowGenrePoco ToPoco()
        {
            return new ShowGenrePoco
            {
                GenreID = this.GenreID,
                ShowID = this.ShowID,
                ShowsGenresID = this.ShowsGenresID,
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

        public SubscriptionPoco ToPoco()
        {
            return new SubscriptionPoco
            {
                ProfileID = this.ProfileID,
                ShowID = this.ShowID,
                SubscriptionID = this.SubscriptionID,
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
        /// <para>Column name: 'username'.</para>
        /// <para>Table name: 'users'.</para>
        /// <para>This column is not nullable.</para>
        /// <para>PostgreSQL data type: 'character varying'.</para>
        /// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
        /// <para>CLR type: 'string'.</para>
        /// <para>linq2db data type: 'DataType.NVarChar'.</para>
        /// </summary>
        public string Username { get; set; }

        public UserPoco ToPoco()
        {
            return new UserPoco
            {
                IsAdmin = this.IsAdmin,
                Password = this.Password,
                ProfileID = this.ProfileID,
                UserID = this.UserID,
                Username = this.Username,
            };
        }
    }

    public class TrackTvPocos : IDbPocos<TrackTvPocos>
    {
        /// <summary>
        /// <para>Database table 'actors'.</para>
        /// </summary>
        public IQueryable<ActorPoco> Actors => this.DbService.GetTable<ActorPoco>();

        /// <summary>
        /// <para>Database table 'actors'.</para>
        /// <para>Filter model 'ActorFM'.</para>
        /// <para>Catalog model 'ActorCM'.</para>
        /// </summary>
        public Task<List<ActorCM>> Filter(ActorFM filter) => this.DbService.FilterInternal<ActorPoco, ActorCM>(filter);

        /// <summary>
        /// <para>Database table 'api_change_types'.</para>
        /// </summary>
        public IQueryable<ApiChangeTypePoco> ApiChangeTypes => this.DbService.GetTable<ApiChangeTypePoco>();

        /// <summary>
        /// <para>Database table 'api_change_types'.</para>
        /// <para>Filter model 'ApiChangeTypeFM'.</para>
        /// <para>Catalog model 'ApiChangeTypeCM'.</para>
        /// </summary>
        public Task<List<ApiChangeTypeCM>> Filter(ApiChangeTypeFM filter) => this.DbService.FilterInternal<ApiChangeTypePoco, ApiChangeTypeCM>(filter);

        /// <summary>
        /// <para>Database table 'api_changes'.</para>
        /// </summary>
        public IQueryable<ApiChangePoco> ApiChanges => this.DbService.GetTable<ApiChangePoco>();

        /// <summary>
        /// <para>Database table 'api_changes'.</para>
        /// <para>Filter model 'ApiChangeFM'.</para>
        /// <para>Catalog model 'ApiChangeCM'.</para>
        /// </summary>
        public Task<List<ApiChangeCM>> Filter(ApiChangeFM filter) => this.DbService.FilterInternal<ApiChangePoco, ApiChangeCM>(filter);

        /// <summary>
        /// <para>Database table 'api_responses'.</para>
        /// </summary>
        public IQueryable<ApiResponsePoco> ApiResponses => this.DbService.GetTable<ApiResponsePoco>();

        /// <summary>
        /// <para>Database table 'api_responses'.</para>
        /// <para>Filter model 'ApiResponseFM'.</para>
        /// <para>Catalog model 'ApiResponseCM'.</para>
        /// </summary>
        public Task<List<ApiResponseCM>> Filter(ApiResponseFM filter) => this.DbService.FilterInternal<ApiResponsePoco, ApiResponseCM>(filter);

        /// <summary>
        /// <para>Database table 'episodes'.</para>
        /// </summary>
        public IQueryable<EpisodePoco> Episodes => this.DbService.GetTable<EpisodePoco>();

        /// <summary>
        /// <para>Database table 'episodes'.</para>
        /// <para>Filter model 'EpisodeFM'.</para>
        /// <para>Catalog model 'EpisodeCM'.</para>
        /// </summary>
        public Task<List<EpisodeCM>> Filter(EpisodeFM filter) => this.DbService.FilterInternal<EpisodePoco, EpisodeCM>(filter);

        /// <summary>
        /// <para>Database table 'genres'.</para>
        /// </summary>
        public IQueryable<GenrePoco> Genres => this.DbService.GetTable<GenrePoco>();

        /// <summary>
        /// <para>Database table 'genres'.</para>
        /// <para>Filter model 'GenreFM'.</para>
        /// <para>Catalog model 'GenreCM'.</para>
        /// </summary>
        public Task<List<GenreCM>> Filter(GenreFM filter) => this.DbService.FilterInternal<GenrePoco, GenreCM>(filter);

        /// <summary>
        /// <para>Database table 'networks'.</para>
        /// </summary>
        public IQueryable<NetworkPoco> Networks => this.DbService.GetTable<NetworkPoco>();

        /// <summary>
        /// <para>Database table 'networks'.</para>
        /// <para>Filter model 'NetworkFM'.</para>
        /// <para>Catalog model 'NetworkCM'.</para>
        /// </summary>
        public Task<List<NetworkCM>> Filter(NetworkFM filter) => this.DbService.FilterInternal<NetworkPoco, NetworkCM>(filter);

        /// <summary>
        /// <para>Database table 'profiles'.</para>
        /// </summary>
        public IQueryable<ProfilePoco> Profiles => this.DbService.GetTable<ProfilePoco>();

        /// <summary>
        /// <para>Database table 'profiles'.</para>
        /// <para>Filter model 'ProfileFM'.</para>
        /// <para>Catalog model 'ProfileCM'.</para>
        /// </summary>
        public Task<List<ProfileCM>> Filter(ProfileFM filter) => this.DbService.FilterInternal<ProfilePoco, ProfileCM>(filter);

        /// <summary>
        /// <para>Database table 'roles'.</para>
        /// </summary>
        public IQueryable<RolePoco> Roles => this.DbService.GetTable<RolePoco>();

        /// <summary>
        /// <para>Database table 'roles'.</para>
        /// <para>Filter model 'RoleFM'.</para>
        /// <para>Catalog model 'RoleCM'.</para>
        /// </summary>
        public Task<List<RoleCM>> Filter(RoleFM filter) => this.DbService.FilterInternal<RolePoco, RoleCM>(filter);

        /// <summary>
        /// <para>Database table 'settings'.</para>
        /// </summary>
        public IQueryable<SettingPoco> Settings => this.DbService.GetTable<SettingPoco>();

        /// <summary>
        /// <para>Database table 'settings'.</para>
        /// <para>Filter model 'SettingFM'.</para>
        /// <para>Catalog model 'SettingCM'.</para>
        /// </summary>
        public Task<List<SettingCM>> Filter(SettingFM filter) => this.DbService.FilterInternal<SettingPoco, SettingCM>(filter);

        /// <summary>
        /// <para>Database table 'shows'.</para>
        /// </summary>
        public IQueryable<ShowPoco> Shows => this.DbService.GetTable<ShowPoco>();

        /// <summary>
        /// <para>Database table 'shows'.</para>
        /// <para>Filter model 'ShowFM'.</para>
        /// <para>Catalog model 'ShowCM'.</para>
        /// </summary>
        public Task<List<ShowCM>> Filter(ShowFM filter) => this.DbService.FilterInternal<ShowPoco, ShowCM>(filter);

        /// <summary>
        /// <para>Database table 'shows_genres'.</para>
        /// </summary>
        public IQueryable<ShowGenrePoco> ShowsGenres => this.DbService.GetTable<ShowGenrePoco>();

        /// <summary>
        /// <para>Database table 'shows_genres'.</para>
        /// <para>Filter model 'ShowGenreFM'.</para>
        /// <para>Catalog model 'ShowGenreCM'.</para>
        /// </summary>
        public Task<List<ShowGenreCM>> Filter(ShowGenreFM filter) => this.DbService.FilterInternal<ShowGenrePoco, ShowGenreCM>(filter);

        /// <summary>
        /// <para>Database table 'subscriptions'.</para>
        /// </summary>
        public IQueryable<SubscriptionPoco> Subscriptions => this.DbService.GetTable<SubscriptionPoco>();

        /// <summary>
        /// <para>Database table 'subscriptions'.</para>
        /// <para>Filter model 'SubscriptionFM'.</para>
        /// <para>Catalog model 'SubscriptionCM'.</para>
        /// </summary>
        public Task<List<SubscriptionCM>> Filter(SubscriptionFM filter) => this.DbService.FilterInternal<SubscriptionPoco, SubscriptionCM>(filter);

        /// <summary>
        /// <para>Database table 'users'.</para>
        /// </summary>
        public IQueryable<UserPoco> Users => this.DbService.GetTable<UserPoco>();

        /// <summary>
        /// <para>Database table 'users'.</para>
        /// <para>Filter model 'UserFM'.</para>
        /// <para>Catalog model 'UserCM'.</para>
        /// </summary>
        public Task<List<UserCM>> Filter(UserFM filter) => this.DbService.FilterInternal<UserPoco, UserCM>(filter);


        public IDbService<TrackTvPocos> DbService { private get; set; }
    }

    public static class TrackTvPocosExtensions
    {
        /// <summary>
        /// <para>Database table 'actors'.</para>
        /// </summary>
        public static IQueryable<ActorCM> SelectCm(this IQueryable<ActorPoco> collection) => collection.SelectCm<ActorPoco, ActorCM>();

        /// <summary>
        /// <para>Database table 'api_change_types'.</para>
        /// </summary>
        public static IQueryable<ApiChangeTypeCM> SelectCm(this IQueryable<ApiChangeTypePoco> collection) => collection.SelectCm<ApiChangeTypePoco, ApiChangeTypeCM>();

        /// <summary>
        /// <para>Database table 'api_changes'.</para>
        /// </summary>
        public static IQueryable<ApiChangeCM> SelectCm(this IQueryable<ApiChangePoco> collection) => collection.SelectCm<ApiChangePoco, ApiChangeCM>();

        /// <summary>
        /// <para>Database table 'api_responses'.</para>
        /// </summary>
        public static IQueryable<ApiResponseCM> SelectCm(this IQueryable<ApiResponsePoco> collection) => collection.SelectCm<ApiResponsePoco, ApiResponseCM>();

        /// <summary>
        /// <para>Database table 'episodes'.</para>
        /// </summary>
        public static IQueryable<EpisodeCM> SelectCm(this IQueryable<EpisodePoco> collection) => collection.SelectCm<EpisodePoco, EpisodeCM>();

        /// <summary>
        /// <para>Database table 'genres'.</para>
        /// </summary>
        public static IQueryable<GenreCM> SelectCm(this IQueryable<GenrePoco> collection) => collection.SelectCm<GenrePoco, GenreCM>();

        /// <summary>
        /// <para>Database table 'networks'.</para>
        /// </summary>
        public static IQueryable<NetworkCM> SelectCm(this IQueryable<NetworkPoco> collection) => collection.SelectCm<NetworkPoco, NetworkCM>();

        /// <summary>
        /// <para>Database table 'profiles'.</para>
        /// </summary>
        public static IQueryable<ProfileCM> SelectCm(this IQueryable<ProfilePoco> collection) => collection.SelectCm<ProfilePoco, ProfileCM>();

        /// <summary>
        /// <para>Database table 'roles'.</para>
        /// </summary>
        public static IQueryable<RoleCM> SelectCm(this IQueryable<RolePoco> collection) => collection.SelectCm<RolePoco, RoleCM>();

        /// <summary>
        /// <para>Database table 'settings'.</para>
        /// </summary>
        public static IQueryable<SettingCM> SelectCm(this IQueryable<SettingPoco> collection) => collection.SelectCm<SettingPoco, SettingCM>();

        /// <summary>
        /// <para>Database table 'shows'.</para>
        /// </summary>
        public static IQueryable<ShowCM> SelectCm(this IQueryable<ShowPoco> collection) => collection.SelectCm<ShowPoco, ShowCM>();

        /// <summary>
        /// <para>Database table 'shows_genres'.</para>
        /// </summary>
        public static IQueryable<ShowGenreCM> SelectCm(this IQueryable<ShowGenrePoco> collection) => collection.SelectCm<ShowGenrePoco, ShowGenreCM>();

        /// <summary>
        /// <para>Database table 'subscriptions'.</para>
        /// </summary>
        public static IQueryable<SubscriptionCM> SelectCm(this IQueryable<SubscriptionPoco> collection) => collection.SelectCm<SubscriptionPoco, SubscriptionCM>();

        /// <summary>
        /// <para>Database table 'users'.</para>
        /// </summary>
        public static IQueryable<UserCM> SelectCm(this IQueryable<UserPoco> collection) => collection.SelectCm<UserPoco, UserCM>();

    }

    public class TrackTvMetadata : IDbMetadata
    {
        internal static TableMetadataModel<ActorPoco> ActorPocoMetadata;

        internal static TableMetadataModel<ApiChangeTypePoco> ApiChangeTypePocoMetadata;

        internal static TableMetadataModel<ApiChangePoco> ApiChangePocoMetadata;

        internal static TableMetadataModel<ApiResponsePoco> ApiResponsePocoMetadata;

        internal static TableMetadataModel<EpisodePoco> EpisodePocoMetadata;

        internal static TableMetadataModel<GenrePoco> GenrePocoMetadata;

        internal static TableMetadataModel<NetworkPoco> NetworkPocoMetadata;

        internal static TableMetadataModel<ProfilePoco> ProfilePocoMetadata;

        internal static TableMetadataModel<RolePoco> RolePocoMetadata;

        internal static TableMetadataModel<SettingPoco> SettingPocoMetadata;

        internal static TableMetadataModel<ShowPoco> ShowPocoMetadata;

        internal static TableMetadataModel<ShowGenrePoco> ShowGenrePocoMetadata;

        internal static TableMetadataModel<SubscriptionPoco> SubscriptionPocoMetadata;

        internal static TableMetadataModel<UserPoco> UserPocoMetadata;

        private static readonly object InitLock = new object();

        private static bool Initialized;

        // ReSharper disable once FunctionComplexityOverflow
        // ReSharper disable once CyclomaticComplexity
        private static void InitializeInternal()
        {
            ActorPocoMetadata = new TableMetadataModel<ActorPoco>
            {
                ClassName = "Actor",
                PluralClassName = "Actors",
                TableName = "actors",
                TableSchema = "public",
                PrimaryKeyColumnName = "actor_id",
                PrimaryKeyPropertyName = "ActorID",
                GetPrimaryKey = (instance) => instance.ActorID,
                SetPrimaryKey = (instance, val) => instance.ActorID = val,
                IsNew = (instance) => instance.ActorID == default,
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

            ActorPocoMetadata.Clone = DbCodeGenerator.GetClone<ActorPoco>();
            ActorPocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ActorPocoMetadata);
            ActorPocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ActorPocoMetadata);
            ActorPocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ActorPocoMetadata);
            ActorPocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ActorPocoMetadata);
            ActorPocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ActorPocoMetadata, typeof(ActorFM));

            ApiChangeTypePocoMetadata = new TableMetadataModel<ApiChangeTypePoco>
            {
                ClassName = "ApiChangeType",
                PluralClassName = "ApiChangeTypes",
                TableName = "api_change_types",
                TableSchema = "public",
                PrimaryKeyColumnName = "api_change_type_id",
                PrimaryKeyPropertyName = "ApiChangeTypeID",
                GetPrimaryKey = (instance) => instance.ApiChangeTypeID,
                SetPrimaryKey = (instance, val) => instance.ApiChangeTypeID = val,
                IsNew = (instance) => instance.ApiChangeTypeID == default,
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
                }
            };

            ApiChangeTypePocoMetadata.Clone = DbCodeGenerator.GetClone<ApiChangeTypePoco>();
            ApiChangeTypePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ApiChangeTypePocoMetadata);
            ApiChangeTypePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ApiChangeTypePocoMetadata);
            ApiChangeTypePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ApiChangeTypePocoMetadata);
            ApiChangeTypePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ApiChangeTypePocoMetadata);
            ApiChangeTypePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ApiChangeTypePocoMetadata, typeof(ApiChangeTypeFM));

            ApiChangePocoMetadata = new TableMetadataModel<ApiChangePoco>
            {
                ClassName = "ApiChange",
                PluralClassName = "ApiChanges",
                TableName = "api_changes",
                TableSchema = "public",
                PrimaryKeyColumnName = "api_change_id",
                PrimaryKeyPropertyName = "ApiChangeID",
                GetPrimaryKey = (instance) => instance.ApiChangeID,
                SetPrimaryKey = (instance, val) => instance.ApiChangeID = val,
                IsNew = (instance) => instance.ApiChangeID == default,
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

            ApiChangePocoMetadata.Clone = DbCodeGenerator.GetClone<ApiChangePoco>();
            ApiChangePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ApiChangePocoMetadata);
            ApiChangePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ApiChangePocoMetadata);
            ApiChangePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ApiChangePocoMetadata);
            ApiChangePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ApiChangePocoMetadata);
            ApiChangePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ApiChangePocoMetadata, typeof(ApiChangeFM));

            ApiResponsePocoMetadata = new TableMetadataModel<ApiResponsePoco>
            {
                ClassName = "ApiResponse",
                PluralClassName = "ApiResponses",
                TableName = "api_responses",
                TableSchema = "public",
                PrimaryKeyColumnName = "api_response_id",
                PrimaryKeyPropertyName = "ApiResponseID",
                GetPrimaryKey = (instance) => instance.ApiResponseID,
                SetPrimaryKey = (instance, val) => instance.ApiResponseID = val,
                IsNew = (instance) => instance.ApiResponseID == default,
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
                }
            };

            ApiResponsePocoMetadata.Clone = DbCodeGenerator.GetClone<ApiResponsePoco>();
            ApiResponsePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ApiResponsePocoMetadata);
            ApiResponsePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ApiResponsePocoMetadata);
            ApiResponsePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ApiResponsePocoMetadata);
            ApiResponsePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ApiResponsePocoMetadata);
            ApiResponsePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ApiResponsePocoMetadata, typeof(ApiResponseFM));

            EpisodePocoMetadata = new TableMetadataModel<EpisodePoco>
            {
                ClassName = "Episode",
                PluralClassName = "Episodes",
                TableName = "episodes",
                TableSchema = "public",
                PrimaryKeyColumnName = "episode_id",
                PrimaryKeyPropertyName = "EpisodeID",
                GetPrimaryKey = (instance) => instance.EpisodeID,
                SetPrimaryKey = (instance, val) => instance.EpisodeID = val,
                IsNew = (instance) => instance.EpisodeID == default,
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

            EpisodePocoMetadata.Clone = DbCodeGenerator.GetClone<EpisodePoco>();
            EpisodePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(EpisodePocoMetadata);
            EpisodePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(EpisodePocoMetadata);
            EpisodePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(EpisodePocoMetadata);
            EpisodePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(EpisodePocoMetadata);
            EpisodePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(EpisodePocoMetadata, typeof(EpisodeFM));

            GenrePocoMetadata = new TableMetadataModel<GenrePoco>
            {
                ClassName = "Genre",
                PluralClassName = "Genres",
                TableName = "genres",
                TableSchema = "public",
                PrimaryKeyColumnName = "genre_id",
                PrimaryKeyPropertyName = "GenreID",
                GetPrimaryKey = (instance) => instance.GenreID,
                SetPrimaryKey = (instance, val) => instance.GenreID = val,
                IsNew = (instance) => instance.GenreID == default,
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

            GenrePocoMetadata.Clone = DbCodeGenerator.GetClone<GenrePoco>();
            GenrePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(GenrePocoMetadata);
            GenrePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(GenrePocoMetadata);
            GenrePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(GenrePocoMetadata);
            GenrePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(GenrePocoMetadata);
            GenrePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(GenrePocoMetadata, typeof(GenreFM));

            NetworkPocoMetadata = new TableMetadataModel<NetworkPoco>
            {
                ClassName = "Network",
                PluralClassName = "Networks",
                TableName = "networks",
                TableSchema = "public",
                PrimaryKeyColumnName = "network_id",
                PrimaryKeyPropertyName = "NetworkID",
                GetPrimaryKey = (instance) => instance.NetworkID,
                SetPrimaryKey = (instance, val) => instance.NetworkID = val,
                IsNew = (instance) => instance.NetworkID == default,
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

            NetworkPocoMetadata.Clone = DbCodeGenerator.GetClone<NetworkPoco>();
            NetworkPocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(NetworkPocoMetadata);
            NetworkPocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(NetworkPocoMetadata);
            NetworkPocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(NetworkPocoMetadata);
            NetworkPocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(NetworkPocoMetadata);
            NetworkPocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(NetworkPocoMetadata, typeof(NetworkFM));

            ProfilePocoMetadata = new TableMetadataModel<ProfilePoco>
            {
                ClassName = "Profile",
                PluralClassName = "Profiles",
                TableName = "profiles",
                TableSchema = "public",
                PrimaryKeyColumnName = "profile_id",
                PrimaryKeyPropertyName = "ProfileID",
                GetPrimaryKey = (instance) => instance.ProfileID,
                SetPrimaryKey = (instance, val) => instance.ProfileID = val,
                IsNew = (instance) => instance.ProfileID == default,
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

            ProfilePocoMetadata.Clone = DbCodeGenerator.GetClone<ProfilePoco>();
            ProfilePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ProfilePocoMetadata);
            ProfilePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ProfilePocoMetadata);
            ProfilePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ProfilePocoMetadata);
            ProfilePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ProfilePocoMetadata);
            ProfilePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ProfilePocoMetadata, typeof(ProfileFM));

            RolePocoMetadata = new TableMetadataModel<RolePoco>
            {
                ClassName = "Role",
                PluralClassName = "Roles",
                TableName = "roles",
                TableSchema = "public",
                PrimaryKeyColumnName = "role_id",
                PrimaryKeyPropertyName = "RoleID",
                GetPrimaryKey = (instance) => instance.RoleID,
                SetPrimaryKey = (instance, val) => instance.RoleID = val,
                IsNew = (instance) => instance.RoleID == default,
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

            RolePocoMetadata.Clone = DbCodeGenerator.GetClone<RolePoco>();
            RolePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(RolePocoMetadata);
            RolePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(RolePocoMetadata);
            RolePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(RolePocoMetadata);
            RolePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(RolePocoMetadata);
            RolePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(RolePocoMetadata, typeof(RoleFM));

            SettingPocoMetadata = new TableMetadataModel<SettingPoco>
            {
                ClassName = "Setting",
                PluralClassName = "Settings",
                TableName = "settings",
                TableSchema = "public",
                PrimaryKeyColumnName = "setting_id",
                PrimaryKeyPropertyName = "SettingID",
                GetPrimaryKey = (instance) => instance.SettingID,
                SetPrimaryKey = (instance, val) => instance.SettingID = val,
                IsNew = (instance) => instance.SettingID == default,
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
                }
            };

            SettingPocoMetadata.Clone = DbCodeGenerator.GetClone<SettingPoco>();
            SettingPocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(SettingPocoMetadata);
            SettingPocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(SettingPocoMetadata);
            SettingPocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(SettingPocoMetadata);
            SettingPocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(SettingPocoMetadata);
            SettingPocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(SettingPocoMetadata, typeof(SettingFM));

            ShowPocoMetadata = new TableMetadataModel<ShowPoco>
            {
                ClassName = "Show",
                PluralClassName = "Shows",
                TableName = "shows",
                TableSchema = "public",
                PrimaryKeyColumnName = "show_id",
                PrimaryKeyPropertyName = "ShowID",
                GetPrimaryKey = (instance) => instance.ShowID,
                SetPrimaryKey = (instance, val) => instance.ShowID = val,
                IsNew = (instance) => instance.ShowID == default,
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

            ShowPocoMetadata.Clone = DbCodeGenerator.GetClone<ShowPoco>();
            ShowPocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ShowPocoMetadata);
            ShowPocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ShowPocoMetadata);
            ShowPocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ShowPocoMetadata);
            ShowPocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ShowPocoMetadata);
            ShowPocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ShowPocoMetadata, typeof(ShowFM));

            ShowGenrePocoMetadata = new TableMetadataModel<ShowGenrePoco>
            {
                ClassName = "ShowGenre",
                PluralClassName = "ShowsGenres",
                TableName = "shows_genres",
                TableSchema = "public",
                PrimaryKeyColumnName = "shows_genres_id",
                PrimaryKeyPropertyName = "ShowsGenresID",
                GetPrimaryKey = (instance) => instance.ShowsGenresID,
                SetPrimaryKey = (instance, val) => instance.ShowsGenresID = val,
                IsNew = (instance) => instance.ShowsGenresID == default,
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
                }
            };

            ShowGenrePocoMetadata.Clone = DbCodeGenerator.GetClone<ShowGenrePoco>();
            ShowGenrePocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(ShowGenrePocoMetadata);
            ShowGenrePocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(ShowGenrePocoMetadata);
            ShowGenrePocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(ShowGenrePocoMetadata);
            ShowGenrePocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(ShowGenrePocoMetadata);
            ShowGenrePocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(ShowGenrePocoMetadata, typeof(ShowGenreFM));

            SubscriptionPocoMetadata = new TableMetadataModel<SubscriptionPoco>
            {
                ClassName = "Subscription",
                PluralClassName = "Subscriptions",
                TableName = "subscriptions",
                TableSchema = "public",
                PrimaryKeyColumnName = "subscription_id",
                PrimaryKeyPropertyName = "SubscriptionID",
                GetPrimaryKey = (instance) => instance.SubscriptionID,
                SetPrimaryKey = (instance, val) => instance.SubscriptionID = val,
                IsNew = (instance) => instance.SubscriptionID == default,
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
                }
            };

            SubscriptionPocoMetadata.Clone = DbCodeGenerator.GetClone<SubscriptionPoco>();
            SubscriptionPocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(SubscriptionPocoMetadata);
            SubscriptionPocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(SubscriptionPocoMetadata);
            SubscriptionPocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(SubscriptionPocoMetadata);
            SubscriptionPocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(SubscriptionPocoMetadata);
            SubscriptionPocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(SubscriptionPocoMetadata, typeof(SubscriptionFM));

            UserPocoMetadata = new TableMetadataModel<UserPoco>
            {
                ClassName = "User",
                PluralClassName = "Users",
                TableName = "users",
                TableSchema = "public",
                PrimaryKeyColumnName = "user_id",
                PrimaryKeyPropertyName = "UserID",
                GetPrimaryKey = (instance) => instance.UserID,
                SetPrimaryKey = (instance, val) => instance.UserID = val,
                IsNew = (instance) => instance.UserID == default,
                Columns = new List<ColumnMetadataModel>
                {
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
                }
            };

            UserPocoMetadata.Clone = DbCodeGenerator.GetClone<UserPoco>();
            UserPocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(UserPocoMetadata);
            UserPocoMetadata.WriteToImporter = DbCodeGenerator.GetWriteToImporter(UserPocoMetadata);
            UserPocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(UserPocoMetadata);
            UserPocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(UserPocoMetadata);
            UserPocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(UserPocoMetadata, typeof(UserFM));

        }

        public static void Initialize()
        {
            if(Initialized)
            {
                return;
            }

            lock(InitLock)
            {
                if(Initialized)
                {
                    return;
                }

                InitializeInternal();

                Initialized = true;
            }
        }

        static TrackTvMetadata()
        {
            Initialize();
        }
    }
}
