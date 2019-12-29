using Canducci.SqlKata.Dapper.Extensions;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
namespace Canducci.SqlKata.Dapper.SQLite
{
   public static class Extensions
   {
      public static QueryBuilderSelect PrepareQuery(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderSelect(connection, new SqliteCompiler(), source);
      }

      #region Insert
      public static QueryBuilderInsert Insert(this IDbConnection connection, object entity)
      {
         return connection.Insert(new SqliteCompiler(), entity);
      }

      public static QueryBuilderInsert Insert(this IDbConnection connection, string table, IReadOnlyDictionary<string, object> values)
      {
         return connection.Insert(new SqliteCompiler(), table, values);
      }

      public static QueryBuilderInsert Insert(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderInsert(connection, new SqliteCompiler(), source);
      }
      #endregion

      #region Update
      public static QueryBuilderUpdate Update(this IDbConnection connection, object entity)
      {
         return connection.Update(new SqliteCompiler(), entity);
      }
      public static QueryBuilderUpdate Update(this IDbConnection connection, Action<Query> source)
      {
         return new QueryBuilderUpdate(connection, new SqliteCompiler(), source);
      }

      public static QueryBuilderUpdate Update(
         this IDbConnection connection,
         string table,
         IReadOnlyDictionary<string, object> values,
         IReadOnlyDictionary<string, object> where)
      {
         return connection.Update(new SqliteCompiler(), table, values, where);
      }
      #endregion
   }
}
