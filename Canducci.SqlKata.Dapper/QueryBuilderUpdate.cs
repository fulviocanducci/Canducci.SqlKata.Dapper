using Canducci.SqlKata.Dapper.Base;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{
   public class QueryBuilderUpdate : QueryBuilderBase
   {
      public QueryBuilderUpdate(IDbConnection connection, Compiler compiler, Action<Query> source)
         : base(connection, compiler, source)
      {
      }

      internal QueryBuilderUpdate(IDbConnection connection, Compiler compiler, EntityDescription entityDescription)
         : base(connection,
             compiler,
             x => x.From(entityDescription.EntityTable.Name).AsUpdate(entityDescription.GetValues()).Where(entityDescription.GetWhere()))
      {
         EntityDescription = entityDescription;
      }

      public int Change()
      {
         SqlResult compile = CompileSqlResult();
         return Connection.Execute(compile.Sql, compile.NamedBindings);
      }

      public async Task<int> ChangeAsync()
      {
         SqlResult compile = CompileSqlResult();
         return await Connection.ExecuteAsync(compile.Sql, compile.NamedBindings);
      }
   }
}
