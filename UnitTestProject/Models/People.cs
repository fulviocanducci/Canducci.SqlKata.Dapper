using System;
using Canducci.SqlKata.Dapper;
namespace Models
{
    [TableFrom("people")]
    public class People
    {
        [ColumnName("id")]
        [PrimaryKey(IsIncrement = true)]
        public int Id { get; set; }

        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("createdat")]
        public DateTime CreatedAt { get; set; }

        [ColumnName("active")]
        public bool Active { get; set; }
    }
}
