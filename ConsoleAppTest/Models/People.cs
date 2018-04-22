using Canducci.SqlKata.Dapper;
using System;
namespace Models
{
    [TableFrom("people")]
    public class People
    {
        [AutoIncrement()]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public bool Active { get; set; } = true;
    }
}
