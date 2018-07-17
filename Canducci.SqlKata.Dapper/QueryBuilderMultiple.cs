using Dapper;
using System;
using System.Data;
using System.Text;
using SqlKata;
using System.Threading.Tasks;
using System.Collections.Generic;
using SqlKata.Compilers;
using static Dapper.SqlMapper;
namespace Canducci.SqlKata.Dapper
{
    public class QueryBuilderMultiple: IDisposable
    {
        private IList<Query> Queries { get; set; }
        private IDbConnection Connection { get; }
        private Compiler Compiler { get; }

        public QueryBuilderMultiple(IDbConnection connection, Compiler compiler)            
        {
            Clear();
            Connection = connection;
            Compiler = compiler;
        }

        public QueryBuilderMultiple AddQuery(Func<Query, Query> item)
        {            
            Queries.Add(item(new Query()));
            return this;
        }

        public GridReader Results(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = CompilerListQueries(Queries);
            Clear();
            return Connection.QueryMultiple(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);            
        }

        public async Task<GridReader> ResultsAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = CompilerListQueries(Queries);
            Clear();
            return await Connection.QueryMultipleAsync(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);
        }

        public void Clear()
        {
            if (Queries == null)
                Queries = new List<Query>();
            else
                Queries.Clear();
        }

        protected SqlResult CompilerListQueries(IList<Query> items)
        {
            StringBuilder sqls = new StringBuilder();
            List<object> bindings = new List<object>();
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var result = Compiler.Compile(items[i]);
                    if (sqls.Length > 0) sqls.Append(";");
                    sqls.Append(result.RawSql);
                    bindings.AddRange(result.Bindings);
                }
            }
            return new SqlResult() { RawSql = sqls.ToString(), Bindings = bindings };
        }

        public void Dispose()
        {
            Connection?.Dispose();            
            GC.SuppressFinalize(this);
        }
    }
}
