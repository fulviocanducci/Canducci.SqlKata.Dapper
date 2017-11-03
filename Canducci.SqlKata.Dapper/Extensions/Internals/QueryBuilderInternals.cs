﻿using SqlKata;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Canducci.SqlKata.Dapper.MySql")]
[assembly: InternalsVisibleTo("Canducci.SqlKata.Dapper.Postgres")]
[assembly: InternalsVisibleTo("Canducci.SqlKata.Dapper.SqlServer")]
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
    }
}