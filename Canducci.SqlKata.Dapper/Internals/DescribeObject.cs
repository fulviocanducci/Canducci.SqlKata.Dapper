using System;
using System.Collections.Generic;
using System.Reflection;
namespace Canducci.SqlKata.Dapper.Internals
{
    internal class DescribeObject<T> : IDisposable
    {
        public T Model { get; }
        public bool IsAutoIncrement
        {
            get
            {
                return (PrimaryKey == null)
                    ? false
                    : PrimaryKey.IsIncrement;
            }
        }
        public PrimaryKeyAttribute PrimaryKey { get; private set; }
        public TableFromAttribute TableFrom { get; private set; }
        public PropertyInfo[] Properties { get; private set; }
        public PropertyInfo Id { get; private set; }
        public string IdName { get; private set; }
        public Type IdType
        {
            get
            {
                return Id == null
                    ? typeof(int)
                    : Id.PropertyType;
            }
        }

        public Dictionary<string, object> Items { get; private set; }

        public DescribeObject(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model null reference");
            }
            if (Items == null)
            {
                Items = new Dictionary<string, object>();
            }
            Model = model;
            Execute();
        }

        internal void Execute()
        {            
            Type type = Model?.GetType();
            TableFrom = type?.GetTypeInfo()?.GetCustomAttribute<TableFromAttribute>();
            if (TableFrom == null)
            {
                TableFrom = new TableFromAttribute(type.Name);
            }
            Properties = type?.GetProperties();
            foreach (PropertyInfo property in Properties)
            {
                ColumnNameAttribute columnName = (ColumnNameAttribute)property.GetCustomAttribute(typeof(ColumnNameAttribute));
                if (property.GetCustomAttribute(typeof(PrimaryKeyAttribute)) == null)
                {
                    Items.Add((columnName == null ? property.Name : columnName.Name), property.GetValue(Model));
                }
                else
                {
                    PrimaryKey = (PrimaryKeyAttribute)property
                        .GetCustomAttribute(typeof(PrimaryKeyAttribute));                    
                    Id = property;
                    IdName = (columnName == null ? property.Name : columnName.Name);
                    if (!IsAutoIncrement)
                    {
                        Items.Add(IdName, property.GetValue(Model));
                    }                    
                }
            }
        }

        public static DescribeObject<T> Create(T model)
        {
            return new DescribeObject<T>(model);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
