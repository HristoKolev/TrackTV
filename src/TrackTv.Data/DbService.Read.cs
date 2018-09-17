namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class DbService
    {
        public async Task<List<TCatalogModel>> FilterInternal<TPoco, TCatalogModel>(IFilterModel<TPoco> filter, CancellationToken cancellationToken = default)
            where TPoco : IPoco<TPoco>, new() 
            where TCatalogModel: ICatalogModel<TPoco>
        {
            var metadata = GetMetadata<TPoco>();

            var (columnNames, parameters, operators) = metadata.ParseFM(filter);
         
            var sqlBuilder = new StringBuilder();

            sqlBuilder.Append("select * from \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\"");

            if (columnNames.Count > 0)
            {
                sqlBuilder.Append(" where ");

                for (int i = 0; i < columnNames.Count; i++)
                {
                    string columnName = columnNames[i];
                    var parameter = parameters[i];
                    var oper = operators[i];

                    string paramName = "@p" + i;

                    sqlBuilder.Append('\n');
                    sqlBuilder.Append(columnName);

                    AddCondition(oper, paramName, sqlBuilder);

                    if (i != columnNames.Count - 1)
                    {
                        sqlBuilder.Append(" AND ");
                    }

                    parameter.ParameterName = paramName;
                }
            }

            sqlBuilder.Append(';');

            string sql = sqlBuilder.ToString();

            var pocos = await this.Query<TPoco>(sql, parameters.ToArray());
            
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
                    sqlBuilder.Append(paramName);
                    sqlBuilder.Append(" is null");

                    break;
                }
                case QueryOperatorType.IsNotNull :
                {
                    sqlBuilder.Append(paramName);
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
    }
}