using Canducci.SqlKata.Dapper.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Canducci.SqlKata.Dapper
{
   internal sealed class EntityDescription
   {
      public object Entity { get; }
      public TypeInfo TypeInfo { get; }
      public EntityKey EntityKey { get; }
      public EntityTable EntityTable { get; }
      public Dictionary<string, object> Values { get; } = new Dictionary<string, object>();
      public EntityDescription(object entity)
      {
         Entity = entity;
         SetEntityProperty();
         TypeInfo = Entity.GetType().GetTypeInfo();
         EntityKey = GetEntityKey();
         EntityTable = GetEntityTable();
      }

      internal string GetPrimaryKeyName()
      {
         return EntityKey.Property;
      }

      internal object GetPrimaryKeyValue()
      {
         return Values.Where(x => x.Key == GetPrimaryKeyName())?.FirstOrDefault().Value;
      }

      internal Dictionary<string, object> GetValues()
      {
         return Values.Where(x => x.Key != GetPrimaryKeyName()).ToDictionary(x => x.Key, x => x.Value);
      }

      internal Dictionary<string, object> GetValuesAll()
      {
         return Values;
      }

      internal Dictionary<string, object> GetWhere()
      {
         return Values.Where(x => x.Key == GetPrimaryKeyName()).ToDictionary(x => x.Key, x => x.Value);
      }

      internal void SetEntityProperty()
      {
         foreach (PropertyInfo propertyInfo in TypeInfo.GetProperties())
         {
            string name = ((EntityProperty)propertyInfo
               ?.GetCustomAttributes(typeof(EntityProperty))
               ?.FirstOrDefault()).Name
               ?? propertyInfo.Name;
            object value = propertyInfo.GetValue(Entity);
            Values.Add(name, value);
         }
      }

      internal EntityTable GetEntityTable()
      {
         return (EntityTable)TypeInfo
            ?.GetCustomAttributes(typeof(EntityTable))
            ?.FirstOrDefault() ?? new EntityTable(TypeInfo.Name);
      }

      internal EntityKey GetEntityKey()
      {
         return (EntityKey)TypeInfo?.GetCustomAttributes(typeof(EntityKey))?.FirstOrDefault()
            ?? 
            new EntityKey(Values.Where(x => x.Key.ToLower() == "id").Any() ? "id" : "Id");
      }
   }
}
