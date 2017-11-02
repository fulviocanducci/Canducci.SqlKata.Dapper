using SqlKata;
using SqlKata.Compilers;
namespace Canducci.SqlKata.Dapper.Extensions.Internals
{
    internal static class QueryBuilderCompilerInternals
    {
        #region SqlServer
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
        #endregion

        #region PostGresql
        public static SqlResult CompileWithLastId(this PostgresCompiler compiler, Query query, string primaryKeyName = "id")
        {
            SqlResult result = compiler.Compile(query);
            //return new SqlResult(result.Sql + ";SELECT lastval();", result.RawBindings);
            return new SqlResult(result.Sql + $" RETURNING \"{primaryKeyName}\"", result.RawBindings);
        }
        #endregion

        #region MysQL
        public static SqlResult CompileWithLastId(this MySqlCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            return new SqlResult(result.Sql + ";SELECT LAST_INSERT_ID();", result.RawBindings);
        }
        #endregion
    }
}
