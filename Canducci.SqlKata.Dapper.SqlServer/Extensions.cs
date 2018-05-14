using SqlKata.QueryBuilder.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.SqlServer
{
    public static class Extensions
    {
        public static QueryBuilderDapper Build(this IDbConnection connection)
            => new QueryBuilderDapper(connection, new SqlServerCompiler());

        public static QueryBuilderDapper Build(this IDbConnection connection, string table)
            => new QueryBuilderDapper(connection, new SqlServerCompiler(), table);

        public static QueryBuilderSoftDapper SoftBuild(this IDbConnection connection)
            => new QueryBuilderSoftDapper(connection, new SqlServerCompiler());

        public static QueryBuilderSoftDapper SoftBuild(this IDbConnection connection, string table)
            => new QueryBuilderSoftDapper(connection, new SqlServerCompiler(), table);
    }
}
