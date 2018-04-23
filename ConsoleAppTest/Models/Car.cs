using Canducci.SqlKata.Dapper;
namespace Models
{
    [TableFrom("car")]
    public class Car
    {
        [ColumnName("id")]
        [PrimaryKey(IsIncrement = true)]
        public long Id { get; set; }

        [ColumnName("description")]
        public string Description { get; set; }
    }
}
