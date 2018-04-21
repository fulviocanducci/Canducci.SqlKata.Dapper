using Dapper;
using SqlKata;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{
    public partial class QueryBuilder
    {
        public T First<T>()
        {
            SqlResult compiler = Compiler();
            return connection.QueryFirst<T>(compiler.Sql, compiler.Bindings);
        }
        public Task<T> FirstAsync<T>()
        {
            SqlResult compiler = Compiler();
            return connection.QueryFirstAsync<T>(compiler.Sql, compiler.Bindings);
        }
    }
}
