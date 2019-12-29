using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper
{
   public abstract class QueryBuilderBase
   {
      internal IDbConnection Connection { get; private set; }
      internal Compiler Compiler { get; private set; }
      internal Query Query { get; private set; }
      public QueryBuilderBase(IDbConnection connection, Compiler compiler, Action<Query> source)
      {
         Connection = connection ?? throw new ArgumentNullException(nameof(connection));
         Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
         Query = new Query();
         source.Invoke(Query);         
      }
   }
}
