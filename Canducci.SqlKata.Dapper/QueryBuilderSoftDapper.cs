using System.Collections.Generic;
using System.Data;
using SqlKata.Compilers;
using SqlKata;
using Dapper;
using System.Threading.Tasks;
namespace Canducci.SqlKata.Dapper
{
    public partial class QueryBuilderSoftDapper : QueryBuilder
    {
        #region Construct

        public QueryBuilderSoftDapper(IDbConnection connection, Compiler compiler)
            : base(connection, compiler) { }

        public QueryBuilderSoftDapper(IDbConnection connection, Compiler compiler, string table) 
            : base(connection, compiler, table) { }

        #endregion Construct

        #region SoftQueryDapperExtensions

        public T FindOne<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> FindOneAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public int UniqueResultToInt(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return UniqueResult<int>(transaction, commandTimeout, commandType);
        }

        public async Task<int> UniqueResultToIntAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await UniqueResultAsync<int>(transaction, commandTimeout, commandType);
        }

        public long UniqueResultToLong(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return UniqueResult<long>(transaction, commandTimeout, commandType);
        }

        public async Task<long> UniqueResultToLongAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await UniqueResultAsync<long>(transaction, commandTimeout, commandType);
        }

        public T UniqueResult<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> UniqueResultAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        
        public IEnumerable<T> List<T>(IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query<T>(result.Sql, result.Bindings, transaction, buffered, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> ListAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public bool SaveUpdate(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Execute(result.Sql, result.Bindings, transaction, commandTimeout, commandType) == 1;
        }

        public async Task<bool> SaveUpdateAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.ExecuteAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType) == 1;
        }

        public bool SaveInsert(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Execute(result.Sql, result.Bindings, transaction, commandTimeout, commandType) == 1;
        }

        public async Task<bool> SaveInsertAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.ExecuteAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType) == 1;
        }

        #endregion SoftQueryDapperExtensions
    }
}
