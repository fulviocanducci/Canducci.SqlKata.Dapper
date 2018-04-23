using System;
namespace Canducci.SqlKata.Dapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute: Attribute
    {
        public bool IsIncrement { get; set; } = true;
    }
}
