namespace TrackTv.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Represemts a table in PostgreSQL
    /// </summary>
    public class TableMetadataModel
    {
        public string ClassName { get; set; }

        public List<ColumnMetadataModel> Columns { get; set; }

        public string PluralClassName { get; set; }

        public string PrimaryKeyColumnName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }
    }

    /// <summary>
    /// Represents a column in PostgreSQL
    /// </summary>
    public class ColumnMetadataModel
    {
        public string ClrType { get; set; }

        public string ColumnComment { get; set; }

        public string ColumnName { get; set; }

        public string[] Comments { get; set; }

        public string DataType { get; set; }

        public string ForeignKey { get; set; }

        public string ForeignKeyReferenceColumnName { get; set; }

        public string ForeignKeyReferenceSchemaName { get; set; }

        public string ForeignKeyReferenceTableName { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public string Linq2dbDataType { get; set; }

        public string PrimaryKey { get; set; }

        public string PropertyName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }
    }
}