using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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


        //public override QueryBuilderDapper NewQuery()
        //{
        //    return NewQuery();
        //}

        public int Execute(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();            
            return connection.Execute(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<int> ExecuteAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.ExecuteAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<dynamic> Query(IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, result.Bindings, transaction, buffered, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TReturn>(Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, types, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<T> Query<T>(IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query<T>(result.Sql, result.Bindings, transaction, buffered, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<object> Query(Type type, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.Query(type, result.Sql, result.Bindings, transaction, buffered, commandTimeout, commandType);
        }

        public Task<IEnumerable<dynamic>> QueryAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TReturn>(Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null,
            bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, types, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<object>> QueryAsync(Type type, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync(result.Sql, map, result.Bindings, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public dynamic QueryFirst(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirst(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public object QueryFirst(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirst(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public T QueryFirst<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirst<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<object> QueryFirstAsync(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstAsync(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<T> QueryFirstAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public T QueryFirstOrDefault<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public object QueryFirstOrDefault(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public dynamic QueryFirstOrDefault(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefault(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<object> QueryFirstOrDefaultAsync(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefaultAsync(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryFirstOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public GridReader QueryMultiple(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryMultiple(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<GridReader> QueryMultipleAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QueryMultipleAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public dynamic QuerySingle(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingle(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public object QuerySingle(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingle(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public T QuerySingle<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingle<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<T> QuerySingleAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingleAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<object> QuerySingleAsync(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingleAsync(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public T QuerySingleOrDefault<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingleOrDefault<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<dynamic> QuerySingleOrDefaultAsync(Type type, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingleOrDefaultAsync(type, result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler();
            return connection.QuerySingleOrDefaultAsync<T>(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
        }
    }
}
