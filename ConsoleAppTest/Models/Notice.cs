using Canducci.SqlKata.Dapper;
namespace Models
{
    [TableFrom("notice")]
    public class Notice
    {
        [ColumnName("id")]
        [PrimaryKey(IsIncrement = false)]
        public string Id { get; set; }

        [ColumnName("title")]
        public string Title { get; set; }

        [ColumnName("text")]
        public string Text { get; set; }
    }
}
