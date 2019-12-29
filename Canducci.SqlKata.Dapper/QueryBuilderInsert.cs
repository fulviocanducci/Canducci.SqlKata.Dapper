using Canducci.SqlKata.Dapper.Base;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Canducci.SqlKata.Dapper
{

   public class QueryBuilderInsert : QueryBuilderBase
   {
      public QueryBuilderInsert(IDbConnection connection, Compiler compiler, Action<Query> source)
         : base(connection, compiler, source)
      {
      }

      internal QueryBuilderInsert(IDbConnection connection, Compiler compiler, EntityDescription entityDescription)
         : base(connection,
             compiler,
             x => x.From(entityDescription.EntityTable.Name).AsInsert(entityDescription.GetValues(), entityDescription.EntityKey.AutoIncrement))
      {
         EntityDescription = entityDescription;
      }

      public T Save<T>()
      {
         SqlResult compile = CompileSqlResult();
         Result<long> result = Connection.QueryFirstOrDefault<Result<long>>(compile.Sql, compile.NamedBindings);
         Query.ClearComponent("insert");
         Query.Method = "select";
         Query.Where(EntityDescription.GetPrimaryKeyName(), result.Id);
         compile = CompileSqlResult();
         return Connection.QueryFirstOrDefault<T>(compile.Sql, compile.NamedBindings);
      }

      public async Task<T> SaveAsync<T>()
      {
         SqlResult compile = CompileSqlResult();
         Result<long> result = await Connection.QueryFirstOrDefaultAsync<Result<long>>(compile.Sql, compile.NamedBindings);
         Query.ClearComponent("insert");
         Query.Method = "select";
         Query.Where(EntityDescription.EntityKey.Property, result.Id);
         compile = CompileSqlResult();
         return await Connection.QueryFirstOrDefaultAsync<T>(compile.Sql, compile.NamedBindings);
      }

      public long Save()
      {
         SqlResult compile = CompileSqlResult();
         return Connection.Execute(compile.Sql, compile.NamedBindings);
      }

      public async Task<long> SaveAsync()
      {
         SqlResult compile = CompileSqlResult();
         return await Connection.ExecuteAsync(compile.Sql, compile.NamedBindings);
      }

   }
}
