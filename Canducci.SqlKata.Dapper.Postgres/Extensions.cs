using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.Postgres
{
    public static class Extensions
    {
        public static QueryBuilder Query(this IDbConnection connection)
            => new QueryBuilder(connection, new PostgresCompiler());

        public static QueryBuilder Query(this IDbConnection connection, string table)
            => new QueryBuilder(connection, new PostgresCompiler(), table);

        public static InsertBuilder Insert(this IDbConnection connection)
           => new InsertBuilder(connection, new PostgresCompiler());

        public static InsertBuilder Insert(this IDbConnection connection, string table)
            => new InsertBuilder(connection, new PostgresCompiler(), table);

        public static UpdateBuilder Update(this IDbConnection connection)
           => new UpdateBuilder(connection, new PostgresCompiler());

        public static UpdateBuilder Update(this IDbConnection connection, string table)
            => new UpdateBuilder(connection, new PostgresCompiler(), table);

        public static DeleteBuilder Delete(this IDbConnection connection)
           => new DeleteBuilder(connection, new PostgresCompiler());

        public static DeleteBuilder Delete(this IDbConnection connection, string table)
            => new DeleteBuilder(connection, new PostgresCompiler(), table);

        public static T Insert<T>(this IDbConnection connection, T model)
        {
            InsertObject<T> insert = new InsertObject<T>(connection, new PostgresCompiler(), model);
            return insert.Save();
        }

        public static bool Update<T>(this IDbConnection connection, T model)
        {
            UpdateObject<T> update = new UpdateObject<T>(connection, new PostgresCompiler(), model);
            return update.Save();
        }
    }
}
