using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SqlKata;
namespace Canducci.SqlKata.Dapper
{
    public partial class QueryBuilder
    {
        public IEnumerable<T> List<T>()
        {
            SqlResult compiler = Compile();
            return Connection.Query<T>(compiler.Sql, compiler.Bindings);
        }

        public Task<IEnumerable<T>> ListAsync<T>()
        {
            SqlResult compiler = Compile();
            return Connection.QueryAsync<T>(compiler.Sql, compiler.Bindings);
        }
    }
}
