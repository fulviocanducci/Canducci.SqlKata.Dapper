using Canducci.SqlKata.Dapper.Attributes;
using System;
namespace Models
{
   [EntityKey("Id", "id", true)]
   [EntityTable("credit")]
   public class Credit
   {
      [EntityProperty("id")]
      public int Id { get; set; }

      [EntityProperty("description")]
      public string Description { get; set; }

      [EntityProperty("created")]
      public DateTime? Created { get; set; }
   }
}
