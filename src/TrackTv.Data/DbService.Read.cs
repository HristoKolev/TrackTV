namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Npgsql;

    public partial class DbService<TPocos>
    {
        public Task<List<TCatalogModel>> FilterInternal<TPoco, TCatalogModel>(
            IFilterModel<TPoco> filter, 
            CancellationToken cancellationToken = default)
            where TPoco : IPoco<TPoco>, new() 
            where TCatalogModel: ICatalogModel<TPoco>, new()
        {
            var metadata = DbCodeGenerator.GetMetadata<TPoco>();

            var (columnNames, parameters, operators) = metadata.ParseFm(filter);
         
            var sqlBuilder = new StringBuilder();

            sqlBuilder.Append("select * from \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\"");

            var allParameters = new List<NpgsqlParameter>();

            if (columnNames.Count > 0)
            {
                sqlBuilder.Append(" where ");

                for (int i = 0; i < columnNames.Count; i++)
                {
                    string columnName = columnNames[i];
                    var parameter = parameters[i];
                    var oper = operators[i];

                    sqlBuilder.Append("\n\"");
                    sqlBuilder.Append(columnName);
                    sqlBuilder.Append('"');

                    string paramName = null;

                    if (parameter != null)
                    {
                        paramName = "@p" + i;
                        parameter.ParameterName = paramName;
                        allParameters.Add(parameter);
                    }

                    AddCondition(oper, paramName, sqlBuilder);

                    if (i != columnNames.Count - 1)
                    {
                        sqlBuilder.Append(" AND ");
                    }
                }
            }

            sqlBuilder.Append(';');

            string sql = sqlBuilder.ToString();

            return  this.QueryInternal<TCatalogModel>(sql, allParameters, cancellationToken);
        }

        private static void AddCondition(QueryOperatorType oper, string paramName, StringBuilder sqlBuilder)
        {
            switch (oper)
            {
                case QueryOperatorType.Equal :
                {
                    sqlBuilder.Append(" = ");
                    sqlBuilder.Append(paramName);

                    break;
                }
                case QueryOperatorType.NotEqual :
                {
                    sqlBuilder.Append(" != ");
                    sqlBuilder.Append(paramName);

                    break;
                }
                case QueryOperatorType.LessThan :
                {
                    sqlBuilder.Append(" < ");
                    sqlBuilder.Append(paramName);

                    break;
                }
                case QueryOperatorType.LessThanOrEqual :
                {
                    sqlBuilder.Append(" <= ");
                    sqlBuilder.Append(paramName);

                    break;
                }
                case QueryOperatorType.GreaterThan :
                {
                    sqlBuilder.Append(" > ");
                    sqlBuilder.Append(paramName);

                    break;
                }
                case QueryOperatorType.GreaterThanOrEqual :
                {
                    sqlBuilder.Append(" >= ");
                    sqlBuilder.Append(paramName);

                    break;
                }
                case QueryOperatorType.StartsWith :
                {
                    sqlBuilder.Append(" ilike (");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.DoesNotStartWith :
                {
                    sqlBuilder.Append(" not ilike (");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.EndsWith :
                {
                    sqlBuilder.Append(" ilike ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(")");

                    break;
                }
                case QueryOperatorType.DoesNotEndWith :
                {
                    sqlBuilder.Append(" not ilike ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(")");

                    break;
                }
                case QueryOperatorType.Contains :
                {
                    sqlBuilder.Append(" ilike ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.DoesNotContain :
                {
                    sqlBuilder.Append(" not ilike ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.IsNull :
                {
                    sqlBuilder.Append(" is null");

                    break;
                }
                case QueryOperatorType.IsNotNull :
                {
                    sqlBuilder.Append(" is not null");

                    break;
                }
                case QueryOperatorType.IsIn :
                {
                    sqlBuilder.Append(" = ANY(");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(")");

                    break;
                }
                case QueryOperatorType.IsNotIn :
                {
                    sqlBuilder.Append(" != ANY(");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(")");

                    break;
                }
                default :
                {
                    throw new ArgumentOutOfRangeException(nameof(oper));
                }
            }
        }

        public IQueryable<T> GetTable<T>()
            where T : class, IPoco<T>
        {
            return this.LinqToDbConnection.GetTable<T>();
        }
    }
}