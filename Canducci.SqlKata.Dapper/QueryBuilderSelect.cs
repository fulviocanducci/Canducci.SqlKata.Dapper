using Canducci.SqlKata.Dapper.Base;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{
   public class QueryBuilderSelect : QueryBuilderBase
   {
      public QueryBuilderSelect(IDbConnection connection, Compiler compiler, Action<Query> source)
         :base(connection, compiler, source)
      {
      }

      public IEnumerable<T> List<T>()
      {
         SqlResult compile = CompileSqlResult();
         return Connection.Query<T>(compile.Sql, compile.NamedBindings);
      }

      public async Task<IEnumerable<T>> ListAsync<T>()
      {
         SqlResult compile = CompileSqlResult();
         return await Connection.QueryAsync<T>(compile.Sql, compile.NamedBindings);
      }

      public T FindOne<T>()
      {
         SqlResult compile = CompileSqlResult();
         return Connection.QueryFirstOrDefault<T>(compile.Sql, compile.NamedBindings);
      }

      public async Task<T> FindOneAsync<T>()
      {
         SqlResult compile = CompileSqlResult();
         return await Connection.QueryFirstOrDefaultAsync<T>(compile.Sql, compile.NamedBindings);
      }
   }
}
