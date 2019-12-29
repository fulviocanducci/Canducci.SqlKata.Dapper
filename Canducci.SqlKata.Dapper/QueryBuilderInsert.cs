using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper
{
   public class QueryBuilderInsert : QueryBuilderBase
   {
      public QueryBuilderInsert(IDbConnection connection, Compiler compiler, Action<Query> source)
         : base(connection, compiler, source)
      {
      }

      public Result<long> Save()
      {         
         SqlResult resultCompile = Compiler.Compile(Query);
         return Connection.QueryFirstOrDefault<Result<long>>(resultCompile.Sql, resultCompile.NamedBindings);
      }

      public Result<T> Save<T>()
      {
         SqlResult resultCompile = Compiler.Compile(Query);
         return Connection.QueryFirstOrDefault<Result<T>>(resultCompile.Sql, resultCompile.NamedBindings);
      }
   }
}
