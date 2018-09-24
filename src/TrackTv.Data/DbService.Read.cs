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
        public async Task<List<TCatalogModel>> FilterInternal<TPoco, TCatalogModel>(
            IFilterModel<TPoco> filter, 
            CancellationToken cancellationToken = default)
            where TPoco : IPoco<TPoco>, new() 
            where TCatalogModel: ICatalogModel<TPoco>
        {
            var metadata = this.GetMetadata<TPoco>();

            var (columnNames, parameters, operators) = metadata.ParseFM(filter);
         
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

                    sqlBuilder.Append('\n');
                    sqlBuilder.Append(columnName);

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

            var pocos = await this.QueryInternal<TPoco>(sql, allParameters, cancellationToken);
            
            var resultList = new List<TCatalogModel>();

            for (int i = 0; i < pocos.Count; i++)
            {
                resultList.Add((TCatalogModel)metadata.MapToCM(pocos[i]));
            }

            return resultList;
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
                    sqlBuilder.Append(" ~~ (");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.DoesNotStartWith :
                {
                    sqlBuilder.Append(" !~~ (");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.EndsWith :
                {
                    sqlBuilder.Append(" ~~ ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(")");

                    break;
                }
                case QueryOperatorType.DoesNotEndWith :
                {
                    sqlBuilder.Append(" !~~ ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(")");

                    break;
                }
                case QueryOperatorType.Contains :
                {
                    sqlBuilder.Append(" ~~ ('%' || ");
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" || '%')");

                    break;
                }
                case QueryOperatorType.DoesNotContain :
                {
                    sqlBuilder.Append(" !~~ ('%' || ");
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