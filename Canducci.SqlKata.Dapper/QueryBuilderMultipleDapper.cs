using Dapper;
using System;
using System.Data;
using SqlKata;
using System.Threading.Tasks;
using System.Collections.Generic;
using SqlKata.Compilers;
using static Dapper.SqlMapper;

namespace Canducci.SqlKata.Dapper
{
    public class QueryBuilderMultipleDapper : IDisposable
    {
        private IList<Query> Queries { get; set; }
        private IDbConnection Connection { get; }
        private Compiler Compiler { get; }

        public QueryBuilderMultipleDapper(IDbConnection connection, Compiler compiler)            
        {
            Clear();
            Connection = connection;
            Compiler = compiler;
        }
        
        public QueryBuilderMultipleDapper AddQuery(Query item)
        {
            Queries.Add(item);
            return this;
        }

        public QueryBuilderMultipleDapper AddQuery(Func<Query, Query> item)
        {            
            Queries.Add(item(new Query()));
            return this;
        }

        public QueryBuilderMultipleDapper AddQuery(params Func<Query, Query>[] item)
        {
            AddQuery(item);
            return this;
        }

        public QueryBuilderMultipleDapper AddQuery(params Query[] queries)
        {
            foreach (Query q in queries)
            {
                Queries.Add(q);
            }
            return this;
        }

        public GridReader Results(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compilers(Queries);
            Clear();
            return Connection.QueryMultiple(result.Sql, result.NamedBindings, transaction, commandTimeout, commandType);            
        }

        public async Task<GridReader> ResultsAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compilers(Queries);
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

        protected SqlResult Compilers(IEnumerable<Query> items)
        {
            return Compiler.Compile(items);
        }

        public void Dispose()
        {
            Connection?.Dispose();            
            GC.SuppressFinalize(this);
        }
    }
}
