using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper.Extensions.SoftBuilder
{
    public static class SoftQueryDapperExtensions
    {
        internal static QueryBuilderSoftDapper AsQueryBuilderSoftDapper(this Query query)
        {
            if (query is QueryBuilderSoftDapper queryBuilderSoftDapper)
                return queryBuilderSoftDapper;            
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static T FindOne<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderSoftDapper().FindOne<T>(transaction, commandTimeout, commandType);
        }

        public static async Task<T> FindOneAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await query.AsQueryBuilderSoftDapper().FindOneAsync<T>(transaction, commandTimeout, commandType);
        }

        public static IEnumerable<T> List<T>(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderSoftDapper().List<T>(transaction, buffered, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<T>> ListAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await query.AsQueryBuilderSoftDapper().ListAsync<T>(transaction, commandTimeout, commandType);
        }

        public static bool SaveUpdate(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderSoftDapper().SaveUpdate(transaction, commandTimeout, commandType);                
        }

        public static async Task<bool> SaveUpdateAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await query.AsQueryBuilderSoftDapper().SaveUpdateAsync(transaction, commandTimeout, commandType);
        }

        public static bool SaveInsert(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)            
        {
            return query.AsQueryBuilderSoftDapper().SaveInsert(transaction, commandTimeout, commandType);                
        }

        public static async Task<bool> SaveInsertAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await query.AsQueryBuilderSoftDapper().SaveInsertAsync(transaction, commandTimeout, commandType);
        }

        public static T SaveInsertGetByIdInserted<T>(this Query query, string name = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)  where T : struct
        {
            return query.AsQueryBuilderSoftDapper().SaveInsertGetByIdInserted<T>(name, transaction, commandTimeout, commandType);
        }
    }
}
