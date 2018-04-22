using System;
namespace Canducci.SqlKata.Dapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableFromAttribute: Attribute
    {
        public string Name { get; }
        public TableFromAttribute(string name)
        {
            Name = name;
        }
    }
}
