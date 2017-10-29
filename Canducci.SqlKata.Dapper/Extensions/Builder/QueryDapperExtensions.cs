using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Canducci.SqlKata.Dapper.Extensions.Builder
{
    public static class QueryDapperExtensions
    {       
        internal static QueryBuilderDapper AsQueryBuilderDapper(this Query query)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
                return queryBuilderDapper;
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static int Execute(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Execute(transaction, commandTimeout, commandType);            
        }

        public static Task<int> ExecuteAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().ExecuteAsync(transaction, commandTimeout, commandType);            
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(map, transaction, buffered, splitOn, commandTimeout, commandType);                        
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(map, transaction, buffered, splitOn, commandTimeout, commandType);            
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<dynamic> Query(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(transaction, buffered, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this Query query, Func<TFirst, TSecond, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> Query<TReturn>(this Query query, Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(types, map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<T> Query<T>(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query<T>(transaction, buffered, commandTimeout, commandType);
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<object> Query(this Query query, Type type, string sql, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().Query(type, transaction, buffered, commandTimeout, commandType);
        }

        public static Task<IEnumerable<dynamic>> QueryAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(transaction, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this Query query, Func<TFirst, TSecond, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this Query query, Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(types, map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<object>> QueryAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(type, transaction, commandTimeout, commandType);
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this Query query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync(map, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryAsync<T>(transaction, commandTimeout, commandType);
        }

        public static dynamic QueryFirst(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirst(transaction, commandTimeout, commandType);
        }

        public static object QueryFirst(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirst(transaction, commandTimeout, commandType);            
        }

        public static T QueryFirst<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {        
            return query.AsQueryBuilderDapper().QueryFirst<T>(transaction, commandTimeout, commandType);
        }

        public static Task<object> QueryFirstAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstAsync(type, transaction, commandTimeout, commandType);
        }

        public static Task<T> QueryFirstAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstAsync<T>(transaction, commandTimeout, commandType);
        }

        public static T QueryFirstOrDefault<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstOrDefault<T>(transaction, commandTimeout, commandType);
        }

        public static object QueryFirstOrDefault(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstOrDefault(type, transaction, commandTimeout, commandType);
        }

        public static dynamic QueryFirstOrDefault(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstOrDefault(transaction, commandTimeout, commandType);
        }

        public static Task<object> QueryFirstOrDefaultAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstOrDefaultAsync(type, transaction, commandTimeout, commandType);
        }

        public static Task<T> QueryFirstOrDefaultAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryFirstOrDefaultAsync<T>(transaction, commandTimeout, commandType);
        }

        public static GridReader QueryMultiple(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryMultiple(transaction, commandTimeout, commandType);
        }

        public static Task<GridReader> QueryMultipleAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QueryMultipleAsync(transaction, commandTimeout, commandType);
        }

        public static dynamic QuerySingle(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingle(transaction, commandTimeout, commandType);
        }

        public static object QuerySingle(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingle(type, transaction, commandTimeout, commandType);
        }

        public static T QuerySingle<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingle<T>(transaction, commandTimeout, commandType);            
        }

        public static Task<T> QuerySingleAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingleAsync<T>(transaction, commandTimeout, commandType);
        }

        public static Task<object> QuerySingleAsync(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingleAsync(type, transaction, commandTimeout, commandType);
        }

        public static T QuerySingleOrDefault<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingleOrDefault<T>(transaction, commandTimeout, commandType);
        }

        public static Task<dynamic> QuerySingleOrDefaultAsync<T>(this Query query, Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingleOrDefaultAsync(type, transaction, commandTimeout, commandType);
        }

        public static Task<T> QuerySingleOrDefaultAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderDapper().QuerySingleOrDefaultAsync<T>(transaction, commandTimeout, commandType);
        }
    }
}
