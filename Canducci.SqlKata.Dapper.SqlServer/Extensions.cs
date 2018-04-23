using SqlKata.Compilers;
using System.Data;
namespace Canducci.SqlKata.Dapper.SqlServer
{
    public static class Extensions
    {
        public static QueryBuilder Query(this IDbConnection connection)
            => new QueryBuilder(connection, new SqlServerCompiler());

        public static QueryBuilder Query(this IDbConnection connection, string table)
            => new QueryBuilder(connection, new SqlServerCompiler(), table);

        public static InsertBuilder Insert(this IDbConnection connection)
           => new InsertBuilder(connection, new SqlServerCompiler());

        public static InsertBuilder Insert(this IDbConnection connection, string table)
            => new InsertBuilder(connection, new SqlServerCompiler(), table);

        public static UpdateBuilder Update(this IDbConnection connection)
           => new UpdateBuilder(connection, new SqlServerCompiler());

        public static UpdateBuilder Update(this IDbConnection connection, string table)
            => new UpdateBuilder(connection, new SqlServerCompiler(), table);

        public static DeleteBuilder Delete(this IDbConnection connection)
           => new DeleteBuilder(connection, new SqlServerCompiler());

        public static DeleteBuilder Delete(this IDbConnection connection, string table)
            => new DeleteBuilder(connection, new SqlServerCompiler(), table);

        public static T Insert<T>(this IDbConnection connection, T model)
        {
            InsertObject<T> insert = new InsertObject<T>(connection, new SqlServerCompiler(), model);
            return insert.Save();
        }

        public static bool Update<T>(this IDbConnection connection, T model)
        {
            UpdateObject<T> update = new UpdateObject<T>(connection, new SqlServerCompiler(), model);
            return update.Save();
        }

        public static bool Delete<T>(this IDbConnection connection, T model)
        {
            DeleteObject<T> update = new DeleteObject<T>(connection, new SqlServerCompiler(), model);
            return update.Save();
        }
    }
}
