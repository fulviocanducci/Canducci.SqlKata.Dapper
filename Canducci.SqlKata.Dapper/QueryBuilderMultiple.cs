using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using static Dapper.SqlMapper;
using System.Threading.Tasks;
using System.Text;

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
            return Connection.QueryMultiple(result.Sql, result.Bindings, transaction, commandTimeout, commandType);            
        }

        public async Task<GridReader> ResultsAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = CompilerListQueries(Queries);
            Clear();
            return await Connection.QueryMultipleAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
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
                    bindings.AddRange(result.RawBindings);
                }
            }
            return new SqlResult(sqls.ToString(), bindings);
        }

        public void Dispose()
        {
            Connection?.Dispose();            
            GC.SuppressFinalize(this);
        }
    }
}
