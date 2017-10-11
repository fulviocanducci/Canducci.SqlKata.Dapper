using SqlKata.Compilers;
using System.Data;

namespace Canducci.SqlKata.Dapper.MySql
{
    public static class Extensions
    {

        public static QueryBuilderDapper Build(this IDbConnection connection)
            => new QueryBuilderDapper(connection, new MySqlCompiler());

        public static QueryBuilderDapper Build(this IDbConnection connection, string table)
            => new QueryBuilderDapper(connection, new MySqlCompiler(), table);

    }
}
