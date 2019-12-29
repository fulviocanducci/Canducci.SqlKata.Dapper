using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.Base
{
   public abstract class QueryBuilderBase
   {
      internal IDbConnection Connection { get; private set; }
      internal Compiler Compiler { get; private set; }
      internal Query Query { get; private set; }
      internal EntityDescription EntityDescription { get; set; }
      public QueryBuilderBase(IDbConnection connection, Compiler compiler, Action<Query> source)
      {
         Connection = connection ?? throw new ArgumentNullException(nameof(connection));
         Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
         EntityDescription = default;
         Query = new Query();
         source.Invoke(Query);
      }      

      internal SqlResult CompileSqlResult()
      {
         return Compiler.Compile(Query);
      }
   }
}
