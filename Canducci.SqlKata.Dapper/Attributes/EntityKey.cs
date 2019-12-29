using System;

namespace Canducci.SqlKata.Dapper.Attributes
{
   [AttributeUsage(AttributeTargets.Class)]
   public class EntityKey : Attribute
   {
      public EntityKey(string name, string property = null, bool autoIncrement = false)
      {
         if (string.IsNullOrEmpty(name))
         {
            throw new ArgumentException("message", nameof(name));
         }

         Name = name;
         Property = property ?? name;
         AutoIncrement = autoIncrement;
      }

      public string Name { get; }
      public string Property { get; }
      public bool AutoIncrement { get; }
   }
}
