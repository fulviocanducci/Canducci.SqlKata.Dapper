using System;

namespace Canducci.SqlKata.Dapper.Attributes
{
   [AttributeUsage(AttributeTargets.Class)]
   public class EntityTable : Attribute
   {
      public EntityTable(string name)
      {
         if (string.IsNullOrEmpty(name))
         {
            throw new ArgumentException("message", nameof(name));
         }

         Name = name;
      }

      public string Name { get; }
   }
}
