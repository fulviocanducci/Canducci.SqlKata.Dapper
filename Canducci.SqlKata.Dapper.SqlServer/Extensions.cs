using Canducci.SqlKata.Dapper.Extensions;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
namespace Canducci.SqlKata.Dapper.SqlServer
{
   public static class Extensions
   {
      public static QueryBuilderSelect PrepareQuery(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderSelect(connection, new SqlServerCompiler(), source);
      }

      #region Insert
      public static QueryBuilderInsert Insert(this IDbConnection connection, object entity)
      {
         return connection.Insert(new SqlServerCompiler(), entity);
      }

      public static QueryBuilderInsert Insert(this IDbConnection connection, string table, IReadOnlyDictionary<string, object> values)
      {
         return connection.Insert(new SqlServerCompiler(), table, values);
      }

      public static QueryBuilderInsert Insert(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderInsert(connection, new SqlServerCompiler(), source);
      }
      #endregion

      #region Update
      public static QueryBuilderUpdate Update(this IDbConnection connection, object entity)
      {
         return connection.Update(new SqlServerCompiler(), entity);
      }
      public static QueryBuilderUpdate Update(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderUpdate(connection, new SqlServerCompiler(), source);
      }

      public static QueryBuilderUpdate Update(this IDbConnection connection, string table, IReadOnlyDictionary<string, object> values, IReadOnlyDictionary<string, object> where)
      {
         return connection.Update(new SqlServerCompiler(), table, values, where);
      }
      #endregion
   }
}
