using Dapper;
using SqlKata.QueryBuilder;
using SqlKata.QueryBuilder.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Extensions.Internals;
namespace Canducci.SqlKata.Dapper
{
    public partial class QueryBuilderSoftDapper : QueryBuilder, IDisposable
    {
        #region Construct

        public QueryBuilderSoftDapper(IDbConnection connection, Compiler compiler)
            : base(connection, compiler) { }

        public QueryBuilderSoftDapper(IDbConnection connection, Compiler compiler, string table) 
            : base(connection, compiler, table) { }

        public void Dispose()
        {
            if (QueryBuilderMultiple != null)
            {
                QueryBuilderMultiple.Dispose();
            }
            if (connection != null)
            {
                connection.Dispose();
            }
            connection = null;
            compiler = null;
            GC.SuppressFinalize(this);
        }
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

        #region ForMysqlInserted
        public T SaveInsertForMysql<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = ((MySqlCompiler)compiler).CompileWithLastId(this);
            return connection.QueryFirstOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> SaveInsertForMysqlAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = ((MySqlCompiler)compiler).CompileWithLastId(this);
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        #endregion

        #region ForPostgresInserted
        public T SaveInsertForPostGres<T>(string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = ((PostgresCompiler)compiler).CompileWithLastId(this, primaryKeyName);
            return connection.QueryFirstOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> SaveInsertForPostGresAsync<T>(string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = ((PostgresCompiler)compiler).CompileWithLastId(this, primaryKeyName);
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        #endregion

        #region ForSqlServerInserted

        public T SaveInsertForSqlServer<T>(string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Result<T>((SqlServerCompiler)compiler);
            return connection.QueryFirstOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        public async Task<T> SaveInsertForSqlServerAsync<T>(string primaryKeyName = "id", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Result<T>((SqlServerCompiler)compiler);
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        internal SqlResult Result<T>(SqlServerCompiler compiler, string primaryKeyName = "id")            
        {            
            SqlResult result = null;
            if (typeof(T) == typeof(int)) result = compiler.CompileWithLastIdToInt(this);
            if (typeof(T) == typeof(long)) result = compiler.CompileWithLastIdToLong(this);
            if (typeof(T) == typeof(Guid)) result = compiler.CompileWithLastIdToGuid(this, primaryKeyName);
            return result;
        }
        #endregion

        #endregion SoftQueryDapperExtensions

        #region QueryBuilderMultiple        

        private QueryBuilderMultiple QueryBuilderMultiple { get; set; }

        public QueryBuilderMultiple QueryBuilderMultipleCollection()
        {
            if (QueryBuilderMultiple == null)
            {
                QueryBuilderMultiple = new QueryBuilderMultiple(connection, compiler);
            }
            return QueryBuilderMultiple;
        }        
        #endregion
    }
}
