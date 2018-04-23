using Dapper;
using SqlKata;
using System.Threading.Tasks;
namespace Canducci.SqlKata.Dapper
{
    public partial class QueryBuilder
    {
        public T First<T>()
        {
            SqlResult compiler = Compile();
            return Connection.QueryFirstOrDefault<T>(compiler.Sql, compiler.Bindings);
        }
        public Task<T> FirstAsync<T>()
        {
            SqlResult compiler = Compile();
            return Connection.QueryFirstOrDefaultAsync<T>(compiler.Sql, compiler.Bindings);
        }
    }
}
