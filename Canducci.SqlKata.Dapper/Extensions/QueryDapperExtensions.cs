using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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
            throw new NotSupportedException("Only instances QueryBuilderDapper");
        }

        public static async Task<int> ExecuteAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper.ExecuteAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper");
        }

        public static IEnumerable<dynamic> Query(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query(transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper");
        }

        public static IEnumerable<T> Query<T>(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper.Query<T>(transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper");
        }

        public static async Task<IEnumerable<dynamic>> QueryAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper.QueryAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper");
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper.QueryAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper");
        }
    }
}
