using SqlKata;
using System;
using System.Threading.Tasks;
using Canducci.SqlKata.Dapper.Extensions.Internals;
using Dapper;
using System.Data;
using System.Collections;

namespace Canducci.SqlKata.Dapper.Extensions.MultipleBuilder
{
    public static class MultipleQueryDapperExtensions
    {
        public static QueryBuilderMultiple AddQuery(this Query query, Func<Query, Query> item)
        {
            return query.AsQueryBuilderMultiple().AddQuery(item);
        }

        public static IEnumerable Results(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return query.AsQueryBuilderMultiple().Results(transaction, commandTimeout, commandType);
        }

        //public static async Task<SqlMapper.GridReader> ResultsAsync(this Query query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        //{
        //    return await query.AsQueryBuilderMultiple().ResultsAsync(transaction, commandTimeout, commandType);
        //}
    }
}
