using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
namespace Canducci.SqlKata.Dapper.MySql
{
    public static class Extensions
    {
      public static QueryBuilderSelect PrepareQuery(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderSelect(connection, new MySqlCompiler(), source);
      }

      public static QueryBuilderInsert Insert(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderInsert(connection, new MySqlCompiler(), source);
      }
   }
}
