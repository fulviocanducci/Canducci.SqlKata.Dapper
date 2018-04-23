using Canducci.SqlKata.Dapper;
using System;
namespace Models
{
    [TableFrom("credit")]
    public class Credit
    {
        [PrimaryKey()]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("description")]
        public string Description { get; set; }

        [ColumnName("created")]
        public DateTime? Created { get; set; }
    }
}
