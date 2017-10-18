using SqlKata;
using SqlKata.Compilers;
using System;
namespace Canducci.SqlKata.Dapper.Extensions.SoftBuilder
{
    public static class CompilerExtensions
    {
        public static SqlResult CompileWithLastId(this MySqlCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            return new SqlResult(result.Sql + ";SELECT LAST_INSERT_ID();", result.RawBindings);            
        }

        public static SqlResult CompileWithLastId<T>(this SqlServerCompiler compiler, Query query, string name = "id")
            where T: struct
        {
            SqlResult result = compiler.Compile(query);
            string sqlComplement = result.Sql;
            if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                sqlComplement += ";SELECT SCOPE_IDENTITY();"; 
            }
            else if (typeof(T) == typeof(Guid))
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new FormatException("Name Column is blank");                
                sqlComplement = sqlComplement.Insert(result.Sql.IndexOf(" VALUE"), $" OUTPUT inserted.{name} ");
            }
            return new SqlResult(sqlComplement, result.RawBindings);
        }

        public static SqlResult CompileWithLastId(this PostgresCompiler compiler, Query query)
        {
            SqlResult result = compiler.Compile(query);
            return new SqlResult(result.Sql + ";SELECT lastval();", result.RawBindings);
        }
    }
}
