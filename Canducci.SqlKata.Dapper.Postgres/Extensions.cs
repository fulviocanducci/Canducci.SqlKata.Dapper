using SqlKata.QueryBuilder.Compilers;
using System.Data;

namespace Canducci.SqlKata.Dapper.Postgres
{
    public static class Extensions
    {
        public static QueryBuilderDapper Build(this IDbConnection connection)
            => new QueryBuilderDapper(connection, new PostgresCompiler());

        public static QueryBuilderDapper Build(this IDbConnection connection, string table)
            => new QueryBuilderDapper(connection, new PostgresCompiler(), table);

        public static QueryBuilderSoftDapper SoftBuild(this IDbConnection connection)
            => new QueryBuilderSoftDapper(connection, new PostgresCompiler());

        public static QueryBuilderSoftDapper SoftBuild(this IDbConnection connection, string table)
            => new QueryBuilderSoftDapper(connection, new PostgresCompiler(), table);
    }
}
