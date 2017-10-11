using SqlKata.Compilers;
using System.Data;


namespace Canducci.SqlKata.Dapper.Extensions
{
    public static class QueryBuilderDapperExtensions
    {
        public static QueryBuilderDapper Build(this IDbConnection connection, Compiler compiler)
            => new QueryBuilderDapper(connection, compiler);

        public static QueryBuilderDapper From(this IDbConnection connection, Compiler compiler, string table)
            => new QueryBuilderDapper(connection, compiler, table);
    }
}
