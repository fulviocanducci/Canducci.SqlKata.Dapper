using SqlKata.Compilers;
using System.Data;

namespace Canducci.SqlKata.Dapper.Postgres
{
    public static class Extensions
    {
        public static QueryBuilderDapper Build(this IDbConnection connection)
            => new QueryBuilderDapper(connection, new PostgresCompiler());

        public static QueryBuilderDapper Build(this IDbConnection connection, string table)
            => new QueryBuilderDapper(connection, new PostgresCompiler(), table);
    }
}
