using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
namespace Canducci.SqlKata.Dapper
{
   public class QueryBuilderSelect : QueryBuilderBase
   {
      public QueryBuilderSelect(IDbConnection connection, Compiler compiler, Action<Query> source)
         :base(connection, compiler, source)
      {
      }
      public IEnumerable<T> Get<T>()
      {
         var c = Compiler.Compile(Query);
         return Connection.Query<T>(c.Sql, c.NamedBindings);
      }

      public T GetFirstOrDefault<T>()
      {
         var c = Compiler.Compile(Query);
         return Connection.QueryFirstOrDefault<T>(c.Sql, c.NamedBindings);
      }

      public T GetFindOne<T>()
      {
         return GetFirstOrDefault<T>();
      }
   }
}
