﻿namespace TrackTv.Data.Tests.Infrastructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NpgsqlTypes;

    public class GeneratedData<T> : IEnumerable<object[]>
        where T : class, IReadOnlyPoco<T>, new()
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return PocoDataGenerator.GenerateData<T>()
                                    .Select(x => new object[]
                                    {
                                        x
                                    })
                                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class GeneratedBulkData<T> : IEnumerable<object[]>
        where T : class, IReadOnlyPoco<T>, new()
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                PocoDataGenerator.GenerateData<T>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class GeneratedFilterData<TPoco, TFilter> : IEnumerable<object[]>
        where TPoco : class, IReadOnlyPoco<TPoco>, new() where TFilter : new()
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return this.GenerateData()
                       .Select(x => new[] { x })
                       .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private List<object> GenerateData()
        {
            var list = new List<object>();

            foreach (var property in typeof(TFilter).GetProperties().Where(x => x.GetCustomAttribute<FilterOperatorAttribute>() != null))
            {
                var attribute = property.GetCustomAttribute<FilterOperatorAttribute>();

                NpgsqlDbType dbType = attribute.DbType;
                QueryOperatorType operatorType = attribute.QueryOperatorType;

                if (operatorType == QueryOperatorType.IsNull || operatorType == QueryOperatorType.IsNotNull)
                {
                    var instance = new TFilter();
                    property.SetValue(instance, operatorType == QueryOperatorType.IsNull);
                    list.Add(instance);
                }
                else if (operatorType == QueryOperatorType.IsIn || operatorType == QueryOperatorType.IsNotIn)
                {
                    object values = PocoDataGenerator.GetArrayValuesByType(dbType);

                    var inInstance = new TFilter();
                    property.SetValue(inInstance, values);
                    list.Add(inInstance);
                }
                else
                {
                    object[] values = PocoDataGenerator.GetValuesByType(dbType);

                    var instance = new TFilter();
                    property.SetValue(instance, values.First());
                    list.Add(instance);
                }
            }

            return list;
        }
    }

    public class PocoDataGenerator
    {
        private const int RandomSeed = 938274923;

        public static List<T> GenerateData<T>()
            where T : class, IReadOnlyPoco<T>, new()
        {
            var list = new List<T>();

            var metadata = DbCodeGenerator.GetMetadata<T>();

            var setters = DbCodeGenerator.GenerateSetters<T>();

            var valuesArray = metadata.Columns.Select(x => GetValuesByType(x.NpgsDataType)).ToArray();

            for (var i = 0; i < metadata.Columns.Count; i++)
            {
                var column = metadata.Columns[i];

                if (!column.IsPrimaryKey)
                {
                    var values = valuesArray[i];

                    if (column.IsNullable)
                    {
                        values = values.Concat(new object[] { null }).ToArray();
                    }

                    foreach (object value in values)
                    {
                        var instance = new T();

                        setters[column.ColumnName](instance, value);

                        foreach (var otherColumn in metadata.Columns.Where(x => x != column && !x.IsPrimaryKey))
                        {
                            var newValue = GetValuesByType(otherColumn.NpgsDataType).First();
                            setters[otherColumn.ColumnName](instance, newValue);
                        }

                        list.Add(instance);
                    }
                }
            }

            return list;
        }

        public static object GetArrayValuesByType(NpgsqlDbType dbType)
        {
            switch (dbType)
            {
                case NpgsqlDbType.Bigint :
                {
                    return GenerateLongArray();
                }
                case NpgsqlDbType.Double :
                {
                    return GenerateDoubleArray();
                }
                case NpgsqlDbType.Integer :
                {
                    return GenerateIntArray();
                }
                case NpgsqlDbType.Numeric :
                {
                    return GenerateDecimalArray();
                }
                case NpgsqlDbType.Real :
                {
                    return GenerateFloatArray();
                }
                case NpgsqlDbType.Smallint :
                {
                    return GenerateShortArray();
                }
                case NpgsqlDbType.Boolean :
                {
                    return GenerateBooleanArray();
                }
                case NpgsqlDbType.Char :
                {
                    return GenerateCharArray();
                }
                case NpgsqlDbType.Text :
                {
                    return GenerateStringArray();
                }
                case NpgsqlDbType.Varchar :
                {
                    return GenerateStringArray();
                }
                case NpgsqlDbType.Bytea :
                {
                    return GenerateByteArray();
                }
                case NpgsqlDbType.Date :
                {
                    return GenerateDateArray();
                }
                case NpgsqlDbType.Timestamp :
                {
                    return GenerateDateTimeArray();
                }
                case NpgsqlDbType.Uuid :
                {
                    return GenerateGuidArray();
                }
                case NpgsqlDbType.Xml :
                {
                    return GenerateXmlArray();
                }
                case NpgsqlDbType.Json :
                {
                    return GenerateJsonArray();
                }
                case NpgsqlDbType.Jsonb :
                {
                    return GenerateJsonArray();
                }
                case NpgsqlDbType.TimestampTz :
                {
                    return GenerateDateTimeOffsetArray();
                }
                default :
                {
                    throw new ArgumentOutOfRangeException(nameof(dbType), dbType, null);
                }
            }
        }

        public static object[] GetValuesByType(NpgsqlDbType dbType)
        {
            switch (dbType)
            {
                case NpgsqlDbType.Bigint :
                {
                    return GenerateLongArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Double :
                {
                    return GenerateDoubleArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Integer :
                {
                    return GenerateIntArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Numeric :
                {
                    return GenerateDecimalArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Real :
                {
                    return GenerateFloatArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Smallint :
                {
                    return GenerateShortArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Boolean :
                {
                    return GenerateBooleanArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Char :
                {
                    return GenerateCharArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Text :
                {
                    return GenerateStringArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Varchar :
                {
                    return GenerateStringArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Bytea :
                {
                    return GenerateByteArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Date :
                {
                    return GenerateDateArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Timestamp :
                {
                    return GenerateDateTimeArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Uuid :
                {
                    return GenerateGuidArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Xml :
                {
                    return GenerateXmlArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Json :
                {
                    return GenerateJsonArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.Jsonb :
                {
                    return GenerateJsonArray().Cast<object>().ToArray();
                }
                case NpgsqlDbType.TimestampTz :
                {
                    return GenerateDateTimeOffsetArray().Cast<object>().ToArray();
                }
                default :
                {
                    throw new ArgumentOutOfRangeException(nameof(dbType), dbType, null);
                }
            }
        }

        public static object[] GetValuesByType(Type type)
        {
            string typeName = type.Name;

            switch (typeName)
            {
                case "String" :
                {
                    return GenerateStringArray().Select(x => (object)x).ToArray();
                }
                case "DateTime" :
                {
                    return GenerateDateTimeArray().Select(x => (object)x).ToArray();
                }
                case "DateTimeOffset" :
                {
                    return GenerateDateTimeOffsetArray().Select(x => (object)x).ToArray();
                }
                case "decimal" :
                {
                    return GenerateDecimalArray().Select(x => (object)x).ToArray();
                }
                case "double" :
                {
                    return GenerateDoubleArray().Select(x => (object)x).ToArray();
                }
                case "float" :
                {
                    return GenerateFloatArray().Select(x => (object)x).ToArray();
                }
                case "int" :
                {
                    return GenerateIntArray().Select(x => (object)x).ToArray();
                }
                case "long" :
                {
                    return GenerateLongArray().Select(x => (object)x).ToArray();
                }
                case "short" :
                {
                    return GenerateShortArray().Select(x => (object)x).ToArray();
                }
                default :
                {
                    throw new ArgumentOutOfRangeException(nameof(typeName), typeName, null);
                }
            }
        }

        private static bool[] GenerateBooleanArray()
        {
            return new[] { true, false };
        }

        private static byte[][] GenerateByteArray()
        {
            var random = new Random(RandomSeed);

            byte[][] all = new byte[1][];

            for (int i = 0; i < all.Length; i++)
            {
                byte[] buffer = all[i];

                all[i] = new byte[10];

                random.NextBytes(buffer);
            }

            return all;
        }

        private static string[] GenerateCharArray()
        {
            return new[]
            {
                "a",
                "b",
                "c"
            };
        }

        private static DateTime[] GenerateDateArray()
        {
            var random = new Random(RandomSeed);

            return Enumerable.Range(0, 2)
                             .Select(i => new DateTime(random.Next(2000, 2100), random.Next(1, 13), random.Next(1, 27)))
                             .ToArray();
        }

        private static DateTime[] GenerateDateTimeArray()
        {
            var random = new Random(RandomSeed);

            return Enumerable.Range(0, 2)
                             .Select(i => new DateTime(random.Next(2000, 2100), random.Next(1, 13), random.Next(1, 27), random.Next(1, 24),
                                                       random.Next(1, 60), random.Next(1, 60)))
                             .ToArray();
        }

        private static DateTimeOffset[] GenerateDateTimeOffsetArray()
        {
            var random = new Random(RandomSeed);

            return Enumerable.Range(0, 2)
                             .Select(i => new DateTimeOffset(random.Next(2000, 2100), random.Next(1, 13), random.Next(1, 27),
                                                             random.Next(1, 24), random.Next(1, 60), random.Next(1, 60),
                                                             TimeSpan.FromHours(2)))
                             .ToArray();
        }

        private static decimal[] GenerateDecimalArray()
        {
            return new[]
            {
                0m,
                1.234m,
                -1.234m,
            };
        }

        private static double[] GenerateDoubleArray()
        {
            return new[]
            {
                0d,
                1.234d,
                -1.234d,
            };
        }

        private static float[] GenerateFloatArray()
        {
            return new[]
            {
                0f,
                1.234f,
                -1.234f,
            };
        }

        private static string[] GenerateGuidArray()
        {
            return new[]
            {
                "173e5661-e425-431a-a3b0-03e7d65b95aa",
            };
        }

        private static int[] GenerateIntArray()
        {
            return new[]
            {
                -1,
                0,
                1,
            };
        }

        private static string[] GenerateJsonArray()
        {
            var random = new Random(RandomSeed);

            return new[]
            {
                "{\"a\": \"|\", \"b\": |}".Replace("|", random.Next(0, 100).ToString()),
                "{}"
            };
        }

        private static long[] GenerateLongArray()
        {
            return new[]
            {
                -1L,
                0L,
                1L,
            };
        }

        private static short[] GenerateShortArray()
        {
            return new short[]
            {
                -1,
                0,
                1,
            };
        }

        private static string[] GenerateStringArray()
        {
            var random = new Random(RandomSeed);

            string template = "abcdefghijklmnopqrstuvwxyz";
            template = template + template.ToUpper();

            return Enumerable.Range(0, 2)
                             .Select(x => new string(Enumerable.Range(0, 50)
                                                               .Select(i => template[random.Next(0, template.Length)])
                                                               .ToArray()))
                             .ToArray();
        }

        private static string[] GenerateXmlArray()
        {
            var random = new Random(RandomSeed);

            return new[]
            {
                "<tag>|</tag>".Replace("|", random.Next(0, 100).ToString())
            };
        }
    }
}