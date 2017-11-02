using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper.MySql
{
    public static class CompilerExtensions
    {
        #region MysQL
        public static SqlResult CompileWithLastId(this MySqlCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            return new SqlResult(result.Sql + ";SELECT LAST_INSERT_ID();", result.RawBindings);
        }
        #endregion
    }
}
