using SqlKata;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{
    public static class QueryBuilderExtensions
    {
        
        #region Query_To_QueryBuilder
        internal static QueryBuilder AsQueryBuilder(this Query query)
        {
            if (query is QueryBuilder queryBuilder)
            {
                return queryBuilder;
            }
            throw new InvalidCastException("Only instances QueryBuilder.");
        }
        #endregion

        #region First
        public static T First<T>(this Query query)
        {
            return query.AsQueryBuilder().First<T>();
        }
        public static Task<T> FirstAsync<T>(this Query query)
        {
            return query.AsQueryBuilder().FirstAsync<T>();
        }
        #endregion

        #region List
        public static IEnumerable<T> List<T>(this Query query)
        {
            return query.AsQueryBuilder().List<T>();         
        }
        public static Task<IEnumerable<T>> ListAsync<T>(this Query query)
        {
            return query.AsQueryBuilder().ListAsync<T>();
        }
        #endregion
    }
}
