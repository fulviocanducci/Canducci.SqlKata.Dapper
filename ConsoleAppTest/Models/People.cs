using Canducci.SqlKata.Dapper;
using System;
namespace Models
{
    [TableFrom("people")]
    public class People
    {
        [PrimaryKey()]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("created")]
        public DateTime? Created { get; set; }

        [ColumnName("active")]
        public bool Active { get; set; } = true;
    }
}
