using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections;

namespace Canducci.SqlKata.Dapper
{
    public class QueryBuilderMultiple: QueryBuilder
    {
        private IDictionary<int, KeyValuePair<Type, Query>> Querys { get; }        

        public QueryBuilderMultiple(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
            if (Querys == null)
                Querys = new Dictionary<int, KeyValuePair<Type, Query>>();
        }

        public QueryBuilderMultiple AddQuery<T>(Func<Query, Query> item)
        {
            Query query = new Query();
            Querys.Add(Querys.Count, new KeyValuePair<Type, Query>(typeof(T), item(query)));
            return this;
        }

        public IEnumerable Results(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            SqlResult result = Compiler(Querys);
            SqlMapper.GridReader gridReader = connection.QueryMultiple(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
            foreach (var q in Querys)
            {
                yield return gridReader.Read(q.Value.Key, true);
            }
        }

        //public async Task<dynamic> ResultsAsync(IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        //{            
        //    SqlResult result = Compiler(Querys);
        //    SqlMapper.GridReader gridReader = await connection.QueryMultipleAsync(result.Sql, result.Bindings, transaction, commandTimeout, commandType);
            
        //    foreach(var q in Querys)
        //    {
        //        var t = System.Tuple.Create(gridReader.Read(q.Value.Key, true));
        //        t.
        //    }
        //}
    }
}
