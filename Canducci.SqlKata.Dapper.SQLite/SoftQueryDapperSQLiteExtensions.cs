using SqlKata;
using System.Data;
using Canducci.SqlKata.Dapper.Extensions.SoftBuilder;
using Canducci.SqlKata.Dapper.Extensions.Internals;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper.SQLite
{
    public static class SoftQueryDapperSQLiteExtensions
    {
        public static T SaveInsert<T>(this Query query, string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : struct
        {            
            return query.AsQueryBuilderSoftDapper().SaveInsertForSqlServer<T>(primaryKeyName);
        }

        public static async Task<T> SaveInsertAsync<T>(this Query query, string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : struct
        {            
            return await query.AsQueryBuilderSoftDapper().SaveInsertForSqlServerAsync<T>(primaryKeyName);
        }
    }
}
