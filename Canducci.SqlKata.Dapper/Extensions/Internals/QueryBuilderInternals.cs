using SqlKata;
using System;
namespace Canducci.SqlKata.Dapper.Extensions.Internals
{
    internal static class QueryBuilderInternals
    {
        internal static QueryBuilderDapper AsQueryBuilderDapper(this Query query)
        {
            if (query is QueryBuilderDapper queryBuilderDapper)
                return queryBuilderDapper;
            throw new NotSupportedException("Only instances QueryBuilderDapper.");
        }

        internal static QueryBuilderSoftDapper AsQueryBuilderSoftDapper(this Query query)
        {
            if (query is QueryBuilderSoftDapper queryBuilderSoftDapper)
                return queryBuilderSoftDapper;
            throw new NotSupportedException("Only instances QueryBuilderSoftDapper.");
        }

        //internal static QueryBuilderMultiple AsQueryBuilderMultiple(this Query query)
        //{
        //    if (query is QueryBuilderMultiple queryBuilderMultiple)
        //        return queryBuilderMultiple;
        //    throw new NotSupportedException("Only instances QueryBuilderMultiple.");
        //}
    }
}
