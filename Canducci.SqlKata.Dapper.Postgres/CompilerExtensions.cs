using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper.Postgres
{
    public static class CompilerExtensions
    {
        #region PostGresql
        public static SqlResult CompileWithLastId(this PostgresCompiler compiler, Query query, string primaryKeyName = "id")
        {
            SqlResult result = compiler.Compile(query);
            //return new SqlResult(result.Sql + ";SELECT lastval();", result.RawBindings);
            return new SqlResult(result.Sql + $" RETURNING \"{primaryKeyName}\"", result.RawBindings);
        }
        #endregion
    }
}
