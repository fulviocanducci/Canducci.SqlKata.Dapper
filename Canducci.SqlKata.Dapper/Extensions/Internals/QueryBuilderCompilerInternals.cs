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
            return new SqlResult() { RawSql = sqlComplement, Bindings = result.Bindings };
        }

        public static SqlResult CompileWithLastIdToInt(this SqlServerCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            string sqlComplement = result.Sql + ";SELECT CAST(SCOPE_IDENTITY() AS INT);";
            return new SqlResult() { RawSql = sqlComplement, Bindings = result.Bindings };
        }

        public static SqlResult CompileWithLastIdToLong(this SqlServerCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            string sqlComplement = result.Sql + ";SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
            return new SqlResult() { RawSql = sqlComplement, Bindings = result.Bindings };
        }
        #endregion

        #region PostGresql
        public static SqlResult CompileWithLastId(this PostgresCompiler compiler, Query query, string primaryKeyName = "id")
        {
            SqlResult result = compiler.Compile(query);
            //return new SqlResult(result.Sql + ";SELECT lastval();", result.RawBindings);
            return new SqlResult() { RawSql = (result.Sql + $" RETURNING \"{primaryKeyName}\""), Bindings = result.Bindings };
        }
        #endregion

        #region MysQL
        public static SqlResult CompileWithLastId(this MySqlCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            return new SqlResult() { RawSql = (result.Sql + ";SELECT LAST_INSERT_ID();"), Bindings = result.Bindings };
        }
        #endregion
    }
}
