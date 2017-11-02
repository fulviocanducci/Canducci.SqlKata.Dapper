using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper.SqlServer
{
    public static class CompilerExtensions
    {
        #region SQLServer
        public static SqlResult CompileWithLastIdToGuid(this SqlServerCompiler compiler, Query query, string primaryKeyName = "id")
        {
            SqlResult result = compiler.Compile(query);
            string sqlComplement = result.Sql;
            sqlComplement = sqlComplement.Insert(result.Sql.IndexOf(" VALUE"), $" OUTPUT INSERTED.{primaryKeyName} ");
            return new SqlResult(sqlComplement, result.RawBindings);
        }

        public static SqlResult CompileWithLastIdToInt(this SqlServerCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            string sqlComplement = result.Sql + ";SELECT CAST(SCOPE_IDENTITY() AS INT);";
            return new SqlResult(sqlComplement, result.RawBindings);
        }

        public static SqlResult CompileWithLastIdToLong(this SqlServerCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            string sqlComplement = result.Sql + ";SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
            return new SqlResult(sqlComplement, result.RawBindings);
        }
    }
}