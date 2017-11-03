using SqlKata;
using System.Data;
using Canducci.SqlKata.Dapper.Extensions.Internals;
using System.Threading.Tasks;
namespace Canducci.SqlKata.Dapper.Extensions.SoftBuilder
{
    public static class SoftQueryDapperPostgresExtensions
    {
        public static T SaveInsert<T>(this Query query, string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : struct
        {
            return query.AsQueryBuilderSoftDapper().SaveInsertForPostGres<T>(primaryKeyName);
        }

        public static async Task<T> SaveInsertAsync<T>(this Query query, string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : struct
        {
            return await query.AsQueryBuilderSoftDapper().SaveInsertForPostGresAsync<T>(primaryKeyName);
        }
    }
}
