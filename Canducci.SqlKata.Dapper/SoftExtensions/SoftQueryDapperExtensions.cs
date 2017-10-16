using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper.SoftExtensions
{
    public static class SoftQueryDapperExtensions
    {
        public static T FindOne<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .FindOne<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }
        public static async Task<T> FindOneAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .FindOneAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static IEnumerable<T> List<T>(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .List<T>(transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static async Task<IEnumerable<T>> ListAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .ListAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static bool SaveUpdate(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .SaveUpdate(transaction, commandTimeout, commandType);                
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static async Task<bool> SaveUpdateAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .SaveUpdateAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static ResultInsert<T> SaveInsert<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .SaveInsert<T>(transaction, commandTimeout, commandType);                
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        public static async Task<ResultInsert<T>> SaveInsertAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .SaveInsertAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }
    }
}
