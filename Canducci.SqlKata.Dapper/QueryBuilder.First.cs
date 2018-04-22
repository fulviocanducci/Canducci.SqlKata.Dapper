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
            return connection.QueryFirstOrDefault<T>(compiler.Sql, compiler.Bindings);
        }
        public Task<T> FirstAsync<T>()
        {
            SqlResult compiler = Compiler();
            return connection.QueryFirstOrDefaultAsync<T>(compiler.Sql, compiler.Bindings);
        }
    }
}
