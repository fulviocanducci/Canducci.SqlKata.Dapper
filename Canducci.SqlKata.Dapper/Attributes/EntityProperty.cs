using System;

namespace Canducci.SqlKata.Dapper.Attributes
{
   [AttributeUsage(AttributeTargets.Property)]
   public class EntityProperty : Attribute
   {
      public EntityProperty(string name)
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
