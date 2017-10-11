using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Canducci.SqlKata.Dapper.Extensions
{
    public static class QueryDapperExtensions
    {       
        public static int Execute(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Execute(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<int> ExecuteAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.ExecuteAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<dynamic> Query(this Query query, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this Query query, Func<TFirst, TSecond, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<TReturn> Query<TReturn>(this Query query, Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(types, map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<T> Query<T>(this Query query, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query<T>(transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<object> Query(this Query query, Type type, string sql, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(type, transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<dynamic>> QueryAsync(this Query query, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this Query query, Func<TFirst, TSecond, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this Query query, Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(types, map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<object>> QueryAsync(this Query query, Type type, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static dynamic QueryFirst(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirst(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static object QueryFirst(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirst(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static T QueryFirst<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirst<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<object> QueryFirstAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstAsync(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<T> QueryFirstAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static T QueryFirstOrDefault<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstOrDefault<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static object QueryFirstOrDefault(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstOrDefault(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static dynamic QueryFirstOrDefault(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstOrDefault(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<object> QueryFirstOrDefaultAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstOrDefaultAsync(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryFirstOrDefaultAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static GridReader QueryMultiple(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryMultiple(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<GridReader> QueryMultipleAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QueryMultipleAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static dynamic QuerySingle(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingle(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static object QuerySingle(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingle(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static T QuerySingle<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingle<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<T> QuerySingleAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingleAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<object> QuerySingleAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingleAsync(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static T QuerySingleOrDefault<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingleOrDefault<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<dynamic> QuerySingleOrDefaultAsync<T>(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingleOrDefaultAsync(type, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static Task<T> QuerySingleOrDefaultAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.QuerySingleOrDefaultAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }
    }
}
