using Dapper;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SqlKata;

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
            return connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> FindOneAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
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
            return connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> UniqueResultAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }
        
        public IEnumerable<T> List<T>(IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query<T>(result.Sql, result.NamedBindings, transaction, buffered, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> ListAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryAsync<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        public bool SaveUpdate(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Execute(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType) == 1;
        }

        public async Task<bool> SaveUpdateAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType) == 1;
        }

        public bool SaveInsert(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Execute(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType) == 1;
        }

        public async Task<bool> SaveInsertAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.ExecuteAsync(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType) == 1;
        }

        #region ForMysqlInserted
        public T SaveInsertForMysql<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {            
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> SaveInsertForMysqlAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }
        #endregion

        #region ForPostgresInserted
        public T SaveInsertForPostGres<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        public async Task<T> SaveInsertForPostGresAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }
        #endregion

        #region ForSqlServerInserted

        public T SaveInsertForSqlServer<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {            
            SqlResult result = Compiler();            
            string Sql = ReplaceSqlToGuid<T>(result);
            return connection.QueryFirstOrDefault<T>(Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }
        public async Task<T> SaveInsertForSqlServerAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            string Sql = ReplaceSqlToGuid<T>(result);
            return await connection.QueryFirstOrDefaultAsync<T>(Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        internal protected string ReplaceSqlToGuid<T>(SqlResult result)
        {
            if (typeof(T) == typeof(Guid))
            {
                int index = result.Sql.IndexOf(" VALUES");
                if (index > -1)
                {
                    return result.Sql.Insert(index, " OUTPUT INSERTED.Id");
                }
            }
            return result.Sql;
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
