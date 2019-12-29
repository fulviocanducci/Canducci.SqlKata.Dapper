using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;

namespace Canducci.SqlKata.Dapper.Extensions
{
   public static class Extensions
   {

      #region insert
      internal static QueryBuilderInsert Insert(this IDbConnection connection, Compiler compiler, string table, IReadOnlyDictionary<string, object> values, bool autoIncrement = true)
      {
         if (table is null)
         {
            throw new ArgumentNullException(nameof(table));
         }
         if (values is null)
         {
            throw new ArgumentNullException(nameof(values));
         }
         if (values.Count == 0)
         {
            throw new Exception(nameof(values));
         }
         return new QueryBuilderInsert(connection, compiler, x => x.From(table).AsInsert(values, autoIncrement));
      }

      internal static QueryBuilderInsert Insert(this IDbConnection connection, Compiler compiler, object entity)
      {
         EntityDescription entityDescription = new EntityDescription(entity);
         return new QueryBuilderInsert(connection, compiler, entityDescription);
      }
      #endregion

      #region update
      internal static QueryBuilderUpdate Update(this IDbConnection connection,
         Compiler compiler,
         string table,
         IReadOnlyDictionary<string, object> values,
         IReadOnlyDictionary<string, object> where)
      {
         if (table is null)
         {
            throw new ArgumentNullException(nameof(table));
         }
         if (values is null)
         {
            throw new ArgumentNullException(nameof(values));
         }
         if (values.Count == 0)
         {
            throw new Exception(nameof(values));
         }
         return new QueryBuilderUpdate(connection, compiler, x => x.From(table).AsUpdate(values).Where(where));
      }
      internal static QueryBuilderUpdate Update(this IDbConnection connection, Compiler compiler, object entity)
      {
         EntityDescription entityDescription = new EntityDescription(entity);
         return new QueryBuilderUpdate(connection, compiler, entityDescription);
      }
      #endregion
   }
}
