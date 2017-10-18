using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper.Extensions.SoftBuilder
{
    public static class SoftQueryDapperExtensions
    {
        public static T FindOne<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .FindOne<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static async Task<T> FindOneAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .FindOneAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static IEnumerable<T> List<T>(this Query query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .List<T>(transaction, buffered, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static async Task<IEnumerable<T>> ListAsync<T>(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .ListAsync<T>(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static bool SaveUpdate(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .SaveUpdate(transaction, commandTimeout, commandType);                
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static async Task<bool> SaveUpdateAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .SaveUpdateAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static bool SaveInsert(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)            
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .SaveInsert(transaction, commandTimeout, commandType);                
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static async Task<bool> SaveInsertAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return await queryBuilderDapper
                    .SaveInsertAsync(transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        public static T SaveInsertGetByIdInserted<T>(this Query query, string name = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
            where T : struct
        {
            if (query is QueryBuilderSoftDapper queryBuilderDapper)
            {
                return queryBuilderDapper
                    .SaveInsertGetByIdInserted<T>(name, transaction, commandTimeout, commandType);
            }
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }
    }
}
