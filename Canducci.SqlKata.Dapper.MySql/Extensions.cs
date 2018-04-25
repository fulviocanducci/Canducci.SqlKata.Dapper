using SqlKata.Compilers;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper.MySql
{
    public static class Extensions
    {
        #region builder
        public static QueryBuilder Query(this IDbConnection connection)
            => new QueryBuilder(connection, new MySqlCompiler());

        public static QueryBuilder Query(this IDbConnection connection, string table)
            => new QueryBuilder(connection, new MySqlCompiler(), table);

        public static InsertBuilder Insert(this IDbConnection connection)
           => new InsertBuilder(connection, new MySqlCompiler());

        public static InsertBuilder Insert(this IDbConnection connection, string table)
            => new InsertBuilder(connection, new MySqlCompiler(), table);

        public static UpdateBuilder Update(this IDbConnection connection)
           => new UpdateBuilder(connection, new MySqlCompiler());

        public static UpdateBuilder Update(this IDbConnection connection, string table)
            => new UpdateBuilder(connection, new MySqlCompiler(), table);

        public static DeleteBuilder Delete(this IDbConnection connection)
           => new DeleteBuilder(connection, new MySqlCompiler());

        public static DeleteBuilder Delete(this IDbConnection connection, string table)
            => new DeleteBuilder(connection, new MySqlCompiler(), table);
        #endregion

        #region object
        public static T Insert<T>(this IDbConnection connection, T model)
        {
            InsertObject<T> insert = new InsertObject<T>(connection, new MySqlCompiler(), model);
            return insert.Save();            
        }

        public static async Task<T> InsertAsync<T>(this IDbConnection connection, T model)
        {
            InsertObject<T> insert = new InsertObject<T>(connection, new MySqlCompiler(), model);
            return await insert.SaveAsync();
        }

        public static bool Update<T>(this IDbConnection connection, T model)
        {
            UpdateObject<T> update = new UpdateObject<T>(connection, new MySqlCompiler(), model);            
            return update.Save();
        }

        public static async Task<bool> UpdateAsync<T>(this IDbConnection connection, T model)
        {
            UpdateObject<T> update = new UpdateObject<T>(connection, new MySqlCompiler(), model);
            return await update.SaveAsync();
        }

        public static bool Delete<T>(this IDbConnection connection, T model)
        {
            DeleteObject<T> delete = new DeleteObject<T>(connection, new MySqlCompiler(), model);
            return delete.Save();
        }

        public static async Task<bool> DeleteAsync<T>(this IDbConnection connection, T model)
        {
            DeleteObject<T> delete = new DeleteObject<T>(connection, new MySqlCompiler(), model);
            return await delete.SaveAsync();
        }

        public static T Find<T>(this IDbConnection connection, object id)
        {
            FindObject<T> find = new FindObject<T>(connection, new MySqlCompiler(), id);
            return find.Get();
        }

        public static async Task<T> FindAsync<T>(this IDbConnection connection, object id)
        {
            FindObject<T> find = new FindObject<T>(connection, new MySqlCompiler(), id);
            return await find.GetAsync();
        }

        public static IEnumerable<T> List<T>(this IDbConnection connection)
        {
            ListObject<T> list = new ListObject<T>(connection, new MySqlCompiler());
            return list.List();
        }

        public static async Task<IEnumerable<T>> ListAsync<T>(this IDbConnection connection)
        {
            ListObject<T> list = new ListObject<T>(connection, new MySqlCompiler());
            return await list.ListAsync();
        }
        #endregion
    }
}
