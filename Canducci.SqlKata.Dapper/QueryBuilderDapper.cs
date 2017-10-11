using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{
    public class QueryBuilderDapper : Query
    {
        private IDbConnection connection;
        private Compiler compiler;
        public QueryBuilderDapper(IDbConnection connection, Compiler compiler)
        {
            Init(connection, compiler);
        }

        public QueryBuilderDapper(IDbConnection connection, Compiler compiler, string table)
            : base(table)
        {
            Init(connection, compiler); 
        }

        #region Execute
        public int Execute(IDbTransaction transaction, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler(); 
            return connection.Execute(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public async Task<int> ExecuteAsync(IDbTransaction transaction, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.ExecuteAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        #endregion Execute

        #region Query
        public IEnumerable<dynamic> Query(IDbTransaction transaction, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, result.Bindings, transaction, buffered, commandTimeout, commandType);
        }
        public IEnumerable<T> Query<T>(IDbTransaction transaction, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query<T>(result.Sql, result.Bindings, transaction, buffered, commandTimeout, commandType);
        }
        public async Task<IEnumerable<dynamic>> QueryAsync(IDbTransaction transaction, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(IDbTransaction transaction, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return await connection.QueryAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
        #endregion

        #region Compiler
        private SqlResult Compiler()
        {
            return compiler.Compile(this);
        }
        private SqlResult Compiler(Query query)
        {
            return compiler.Compile(query);
        }
        #endregion

        #region Init
        private void Init(IDbConnection connection, Compiler compiler)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        }
        #endregion Init
    }
}
