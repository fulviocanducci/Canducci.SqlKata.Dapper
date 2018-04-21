using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.MySql
{
    public static class Extensions
    {
        public static QueryBuilder Query(this IDbConnection connection)
            => new QueryBuilder(connection, new MySqlCompiler());

        public static QueryBuilder Query(this IDbConnection connection, string table)
            => new QueryBuilder(connection, new MySqlCompiler(), table);

        public static InsertBuilder Insert(this IDbConnection connection)
           => new InsertBuilder(connection, new MySqlCompiler());

        public static InsertBuilder InsertFrom(this IDbConnection connection, string table)
            => new InsertBuilder(connection, new MySqlCompiler(), table);
    }
}
